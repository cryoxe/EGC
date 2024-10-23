using System.Collections;
using System.Collections.Generic;
using EGC.AssetsEmbedded;
using EGC.Extensions;
using EGC.Utils;
using HarmonyLib;
using ModsPlus;
using Photon.Pun;
using RarityLib.Utils;
using UnboundLib;
using UnboundLib.Networking;
using UnityEngine;

namespace EGC.Cards
{
    internal class GasterBlaster : CustomEffectCard<BlastEffect>
    {
        public override CardDetails Details => new CardDetails
        {
            Title = "Gaster Blaster",
            Description = "your opponents are gonna have a bad time",
            ModName = ExtraGameCards.ModInitials,
            Art = Assets.GasterBlasterArt,
            Rarity = RarityUtils.GetRarity("Legendary"),
            Theme = CardThemeColor.CardThemeColorType.TechWhite,
            OwnerOnly = true,
            Stats = new[]
            {
                new CardInfoStat
                {
                    positive = true,
                    stat = "Attack Speed",
                    amount = "+20%",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
                new CardInfoStat
                {
                    positive = true,
                    stat = "Ammo",
                    amount = "+2",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
                new CardInfoStat
                {
                    positive = false,
                    stat = "Damage",
                    amount = "-20%",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
                new CardInfoStat
                {
                    positive = false,
                    stat = "Health",
                    amount = "-30%",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
            }
        };

        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats,
            CharacterStatModifiers statModifiers, Block block)
        {
            //UnityEngine.Debug.Log($"[{ExtraCards.ModInitials}][Card] {GetTitle()} has been setup.");
            cardInfo.allowMultiple = false;

            statModifiers.health = 0.7f;
            gun.attackSpeed = 0.8f;
            gun.damage = 0.8f;
            gun.ammo = 2;
        }
    }

    public class BlastEffect : CardEffect
    {
        public override void OnShoot(GameObject projectile)
        {
            var pH = projectile.GetComponent<ProjectileHit>();
            var gun = player.GetComponent<Holding>().holdable.GetComponent<Gun>();
            if (pH.ownWeapon != gun.gameObject) return;

            var spawnedAttack = projectile.GetComponent<SpawnedAttack>();
            if (!spawnedAttack) return;

            UnityEngine.Debug.Log("Was shoot by player");
            GasterBlasterSpawner gasterBlasterSpawner = projectile.AddComponent<GasterBlasterSpawner>();
            gasterBlasterSpawner.playerID = player.playerID;
        }
    }

    public class GasterBlasterSpawner : RayHitEffect
    {
        private bool done;
        private Quaternion rotation;
        private Vector2 originPosition;
        public int playerID;


        public override HasToReturn DoHitEffect(HitInfo hit)
        {
            if (done || GasterBlasterBehaviour.maxGasterCount <= GasterBlasterBehaviour.gasterCount ||
                (!PhotonNetwork.IsMasterClient && !PhotonNetwork.OfflineMode))
            {
                return HasToReturn.canContinue;
            }

            var initialPosition = transform.position;
            Transform target = PlayerManager.instance.GetClosestPlayer(initialPosition).transform;

            if (target == null)
            {
                return HasToReturn.canContinue;
            }

            if (target.GetComponent<Player>() == PlayerManager.instance.GetPlayerWithID(playerID))
            {
                return HasToReturn.canContinue;
            }

            if (Vector2.Distance(target.position, initialPosition) >= 6)
            {
                return HasToReturn.canContinue;
            }

            Vector2 shootPosition = CalculatePosition(target.position);
            Quaternion rotation = CalculateRotation(target.position, shootPosition);
            Vector2 direction = (Vector2)target.position - shootPosition;

            UnityEngine.Debug.Log("Good range + good number !");

            NetworkingManager.RPC(typeof(GasterBlasterSpawner), nameof(RPC_SpawnGaster), shootPosition, rotation,
                direction, playerID);

            done = true;
            return HasToReturn.canContinue;
        }

        [UnboundRPC]
        public static void RPC_SpawnGaster(Vector2 position, Quaternion rotation, Vector2 direction, int playerID)
        {
            UnityEngine.Debug.Log("Spawning GasterBlaster");
            if (!PhotonNetwork.IsMasterClient && !PhotonNetwork.OfflineMode) return;
            GasterBlasterBehaviour gasterBlasterBehaviour = PhotonNetwork
                .Instantiate(Assets.GasterBlasterSprite.name, position, rotation,
                    data: new object[] { position, rotation }).AddComponent<GasterBlasterBehaviour>();
            gasterBlasterBehaviour.playerID = playerID;
            gasterBlasterBehaviour.direction = direction;
        }

        private Quaternion CalculateRotation(Vector2 target, Vector2 position)
        {
            Vector2 direction = target - position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90;
            rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            return rotation;
        }

        private Vector2 CalculatePosition(Vector2 target)
        {
            Vector2 position = Vector2.zero;
            Camera camera = GameObject.Find("MainCamera").GetComponent<Camera>();
            var tries = 0;
            while (!(tries > 50))
            {
                position = target + Random.insideUnitCircle * 15;
                Vector3 viewport = camera.WorldToViewportPoint(position);
                bool inCameraFrustum = Is01(viewport.x) && Is01(viewport.y);
                if (inCameraFrustum)
                {
                    break;
                }

                tries++;
            }

            return position;
        }

        private bool Is01(float a)
        {
            return a > 0 && a < 1;
        }
    }

    public class GasterBlasterBehaviour : MonoBehaviour
    {
        internal static int gasterCount = 0;
        internal static readonly int maxGasterCount = 5;

        public int playerID;
        public Vector2 direction;

        private Player player;
        private Gun gun;
        private PhotonView view;
        private Animator animator;
        private Renderer renderer;

        private RemoveAfterSeconds removeAfterSeconds;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            view = GetComponent<PhotonView>();
            renderer = gameObject.GetComponent<Renderer>();

            removeAfterSeconds = gameObject.AddComponent<RemoveAfterSeconds>();

            removeAfterSeconds.seconds = 5;
            renderer.material.color = new Color(renderer.material.color.r, renderer.material.color.g,
                renderer.material.color.b, 0);
            renderer.sortingLayerName = "MostFront";
            renderer.sortingOrder = 1048575;

            gasterCount++;
        }

        private void Start()
        {
            UnityEngine.Debug.Log("Starting GasterBlaster");
            gameObject.transform.localScale = new Vector3(1.8f, 1.8f, transform.localScale.z);

            player = PlayerManager.instance.GetPlayerWithID(playerID);
            gun = player.GetComponent<Holding>().holdable.GetComponent<Gun>();

            this.ExecuteAfterSeconds(0.1f,
                () => { view.RPC(nameof(RPC_ShootLaser), RpcTarget.All, transform.position, direction); });
        }

        [PunRPC]
        private void RPC_ShootLaser(Vector3 position, Vector2 direction)
        {
            UnityEngine.Debug.Log("Starting RPC shoot laser");
            StartCoroutine(ShootLaser(position, direction));
        }

        private IEnumerator ShootLaser(Vector3 position, Vector2 direction)
        {
            yield return FadeIn();

            animator.SetTrigger("isBlasting");
            AudioController.Play(Assets.GasterBlasterNoise, transform);

            yield return BlastEffect(player, gun, position, direction);

            yield return new WaitForSeconds(1.2f);
            animator.SetTrigger("isBlasting");

            yield return new WaitForSeconds(0.5f);
            yield return FadeOut();

            if (PhotonNetwork.IsMasterClient || PhotonNetwork.OfflineMode)
            {
                Unbound.Instance.ExecuteAfterFrames(1, () => PhotonNetwork.Destroy(gameObject));
            }
        }

        private IEnumerator FadeOut()
        {
            while (GetComponent<Renderer>().material.color.a > 0)
            {
                Color objectColor = GetComponent<Renderer>().material.color;
                float fadeAmount = objectColor.a - (5 * TimeHandler.deltaTime);
                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                GetComponent<Renderer>().material.color = objectColor;
                yield return null;
            }
        }

        private IEnumerator FadeIn()
        {
            while (GetComponent<Renderer>().material.color.a < 1)
            {
                Color objectColor = GetComponent<Renderer>().material.color;
                float fadeAmount = objectColor.a + (5 * TimeHandler.deltaTime);
                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                GetComponent<Renderer>().material.color = objectColor;
                yield return null;
            }
        }

        public List<MonoBehaviour> BlastEffect(Player player, Gun gun, Vector3 position, Vector2 direction)
        {
            UnityEngine.Debug.Log("Starting BlastEffect");
            Gun newGun = player.gameObject.AddComponent<Blast>();

            SpawnBulletsEffect effect = player.gameObject.AddComponent<SpawnBulletsEffect>();

            var damage = player.data.weaponHandler.gun.damage / 4f;
            var projectileSize = 1f / damage;

            effect.SetDirection(direction);
            effect.SetPosition(position);
            effect.SetNumBullets(25);
            effect.SetTimeBetweenShots(0.004f);

            SpawnBulletsEffect.CopyGunStats(gun, newGun);
            newGun.damage = damage;
            newGun.damageAfterDistanceMultiplier = 1f;
            newGun.reflects = 0;
            newGun.bulletDamageMultiplier = 1f;
            newGun.projectileSpeed = 2f;
            newGun.projectielSimulatonSpeed = 1.1f;
            newGun.projectileSize = projectileSize;
            newGun.projectileColor = Color.white;
            newGun.spread = 0f;
            newGun.gravity = 0f;
            newGun.destroyBulletAfter = 5f;
            newGun.numberOfProjectiles = 1;
            newGun.ignoreWalls = true;
            newGun.damageAfterDistanceMultiplier = 1f;
            newGun.objectsToSpawn = new[] { PreventRecursion.stopRecursionObjectToSpawn };

            Traverse.Create(newGun).Field("spreadOfLastBullet").SetValue(0f);
            effect.SetGun(newGun);

            return new List<MonoBehaviour> { effect };
        }

        private void OnDestroy()
        {
            gasterCount--;
        }

        public class Blast : Gun
        {
        }
    }
}