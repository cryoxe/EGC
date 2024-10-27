using EGC.AssetsEmbedded;
using EGC.Cards;
using ModsPlus;
using Photon.Pun;
using UnboundLib;
using UnboundLib.Networking;
using UnityEngine;

namespace EGC.MonoBehaviours.GasterBlaster
{
    public class GasterBlasterSpawner : RayHitEffect
    {
        public int BlasterOwnerPlayerID { get; set; } = -1;

        private const float MaxTriggerDistance = 6f;

        private const float MinSpawnDistance = 5f;
        private const float MaxSpawnDistance = 15f;

        private const int InBoundTries = 75;

        private bool isHitDoneOnce;

        public override HasToReturn DoHitEffect(HitInfo hit)
        {
            if (BlasterOwnerPlayerID == -1 || isHitDoneOnce)
                return HasToReturn.canContinue;

            var initialPosition = transform.position;
            Transform target = PlayerManager.instance.GetClosestPlayer(initialPosition).transform;

            if (target == null)
                return HasToReturn.canContinue;

            if (target.GetComponent<Player>() == PlayerManager.instance.GetPlayerWithID(BlasterOwnerPlayerID))
                return HasToReturn.canContinue;

            if (Vector2.Distance(target.position, initialPosition) >= MaxTriggerDistance)
                return HasToReturn.canContinue;

            Vector2 blasterOrbitPosition = CalculateRandomPosition(target.position);
            Quaternion blasterOrientation = CalculateRotation(target.position, blasterOrbitPosition);

            Vector2 direction = (Vector2)target.position - blasterOrbitPosition;

            SpawnGaster(blasterOrbitPosition, blasterOrientation, direction, BlasterOwnerPlayerID);

            isHitDoneOnce = true;
            return HasToReturn.canContinue;
        }

        private static void SpawnGaster(Vector2 position, Quaternion rotation, Vector2 direction, int playerID)
        {
            if (!PhotonNetwork.IsMasterClient && !PhotonNetwork.OfflineMode) return;

            GameObject gasterBlaster = PhotonNetwork.Instantiate(
                Assets.GasterBlasterSprite.name, position, rotation);

            GasterBlasterMono gasterBlasterMono = gasterBlaster.GetComponent<GasterBlasterMono>();
            gasterBlasterMono.BlasterOwnerPlayerID = playerID;
            gasterBlasterMono.Direction = direction;

            PhotonView blasterPhotonView = gasterBlaster.GetComponent<PhotonView>();
            NetworkingManager.RPC(
                typeof(GasterBlasterSpawner), nameof(RPC_UpdateGasterProperties),
                blasterPhotonView.ViewID, playerID, direction);
        }

        [UnboundRPC]
        private static void RPC_UpdateGasterProperties(int viewID, int playerID, Vector2 direction)
        {
            PhotonView photonView = PhotonView.Find(viewID);

            if (photonView == null)
            {
                UnityEngine.Debug.LogError("PhotonView for blaster props not found");
                return;
            }

            GasterBlasterMono gasterBlasterMono = photonView.GetComponent<GasterBlasterMono>();

            if (gasterBlasterMono == null) return;

            gasterBlasterMono.BlasterOwnerPlayerID = playerID;
            gasterBlasterMono.Direction = direction;
        }

        private static Quaternion CalculateRotation(Vector2 target, Vector2 position)
        {
            Vector2 direction = target - position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90;
            return Quaternion.AngleAxis(angle, Vector3.forward);
        }

        private static Vector2 CalculateRandomPosition(Vector2 target)
        {
            Vector2 position = Vector2.zero;
            Camera camera = GameObject.Find("MainCamera").GetComponent<Camera>();

            var tries = 0;
            while (!(tries > InBoundTries))
            {
                Vector2 randomDirection = Random.insideUnitCircle.normalized;
                position = target + randomDirection * Random.Range(MinSpawnDistance, MaxSpawnDistance);

                Vector3 viewport = camera.WorldToViewportPoint(position);
                bool inCameraFrustum = Is01(viewport.x) && Is01(viewport.y);
                if (inCameraFrustum) break;

                tries++;
            }

            return position;
        }

        private static bool Is01(float a) => a > 0 && a < 1;
    }
}