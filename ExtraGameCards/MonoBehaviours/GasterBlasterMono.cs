using EGC.AssetsEmbedded;
using Photon.Pun;
using UnboundLib;
using UnityEngine;

namespace EGC.MonoBehaviours
{
    public class GasterBlasterMono : MonoBehaviour, IPunInstantiateMagicCallback
    {
        private Camera camera;

        public Player player;
        public Gun gun;
        public CharacterData data;
        public HealthHandler health;
        public Gravity gravity;
        public Block block;
        public GunAmmo gunAmmo;
        public CharacterStatModifiers statModifiers;

        private Vector2 targetPos;
        private Vector2 originPos;
        private Vector3 direction;
        private Quaternion rotation;

        private GameObject blasterSprite;
        private AudioSource? gasterBlasterNoise;

        void IPunInstantiateMagicCallback.OnPhotonInstantiate(PhotonMessageInfo info)
        {
            object[] data = info.photonView.InstantiationData;
            originPos = (Vector2)data[0];
            rotation = (Quaternion)data[1];
        }

        public void Start()
        {
            camera = GameObject.Find("MainCamera").GetComponent<Camera>();
            gasterBlasterNoise = gameObject.GetOrAddComponent<AudioSource>();

            ProjectileHit hit = gameObject.GetComponent<ProjectileHit>();
            hit.AddHitAction(OnHit);
        }

        private void OnHit()
        {
            targetPos = gameObject.transform.position;
            Player[] players = PlayerManager.instance.players.ToArray();
            foreach(Player p in players)
            {
                if (p == player) { continue; }
                if (Vector2.Distance(targetPos, p.transform.position) <= 6 && Extensions.CharacterStatModifiersExtension.GetAdditionalData(statModifiers).numberOfGaster < 5)
                {
                    Extensions.CharacterStatModifiersExtension.GetAdditionalData(statModifiers).numberOfGaster++;
                    UnityEngine.Debug.Log($"New GasterBlaster : {Extensions.CharacterStatModifiersExtension.GetAdditionalData(statModifiers).numberOfGaster} total");
                    var tries = 0;
                    while (!(tries>100))
                    {
                        originPos = targetPos + Random.insideUnitCircle * 15;
                        Vector3 viewport = camera.WorldToViewportPoint(originPos);
                        bool inCameraFrustum = Is01(viewport.x) && Is01(viewport.y);
                        if (inCameraFrustum) {break;}
                        tries++;
                    }

                    direction = targetPos - originPos;
                    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90;
                    rotation = Quaternion.AngleAxis(angle, Vector3.forward);

                    blasterSprite = PhotonNetwork.Instantiate(Assets.GasterBlasterSprite.name, originPos, rotation, data: new object[] { originPos, rotation });

                    GasterBlasterInstantMono gasterBlasterInstantMono = blasterSprite.AddComponent<GasterBlasterInstantMono>();
                    gasterBlasterInstantMono.player = player;
                    gasterBlasterInstantMono.health = health;
                    gasterBlasterInstantMono.block = block;
                    gasterBlasterInstantMono.statModifiers = statModifiers;
                    gasterBlasterInstantMono.data = data;
                    gasterBlasterInstantMono.gravity = gravity;
                    gasterBlasterInstantMono.gun = gun;
                    gasterBlasterInstantMono.gunAmmo = gunAmmo;

                    gasterBlasterInstantMono.originPos = originPos;
                    gasterBlasterInstantMono.direction = direction;
                    gasterBlasterInstantMono.targetPos = targetPos;
                    gasterBlasterInstantMono.rotation = rotation;

                    break;
                }
            }
        }
        private bool Is01(float a){return a > 0 && a < 1;}
    }
}
