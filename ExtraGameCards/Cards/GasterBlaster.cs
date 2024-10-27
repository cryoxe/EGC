using EGC.AssetsEmbedded;
using EGC.MonoBehaviours.GasterBlaster;
using ModsPlus;
using RarityLib.Utils;
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
            OwnerOnly = false,
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
                }
            }
        };

        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats,
            CharacterStatModifiers statModifiers, Block block)
        {
            cardInfo.allowMultiple = false;

            statModifiers.health = 0.7f;
            gun.attackSpeed = 0.8f;
            gun.damage = 0.8f;
            gun.ammo = 2;

        }
    }

    public class BlastEffect : CardEffect
    {
        private const int MaxBlasters = 4;
        public int BlasterNumber { get; set; } = 0;


        public override void OnShoot(GameObject projectile)
        {
            if (BlasterNumber >= MaxBlasters) return;

            ProjectileHit projectileHit = projectile.GetComponent<ProjectileHit>();
            Gun gunHeld = player.GetComponent<Holding>().holdable.GetComponent<Gun>();
            if (projectileHit.ownWeapon != gunHeld.gameObject) return;

            var spawnedAttack = projectile.GetComponent<SpawnedAttack>();
            if (!spawnedAttack)
                return;

            GasterBlasterSpawner gasterBlasterSpawner = projectile.AddComponent<GasterBlasterSpawner>();
            gasterBlasterSpawner.BlasterOwnerPlayerID = player.playerID;
        }

        public static BlastEffect? GetBlastEffect(int playerID)
        {
            var blastEffect = GameObject.Find("Gaster Blaster effect").GetComponent<BlastEffect>();
            if (blastEffect == null)
            {
                UnityEngine.Debug.LogWarning("No blast effect found");
            }

            return blastEffect;
        }
    }

    // public class GasterBlasterSpawner : RayHitEffect
    // {
    //     public int BlasterOwnerPlayerID { get; set; } = -1;
    //
    //     private const float MaxDistance = 6;
    //
    //     private bool done;
    //
    //     public override HasToReturn DoHitEffect(HitInfo hit)
    //     {
    //         if (BlasterOwnerPlayerID == -1 || done)
    //         {
    //             return HasToReturn.canContinue;
    //         }
    //
    //         var initialPosition = transform.position;
    //         Transform target = PlayerManager.instance.GetClosestPlayer(initialPosition).transform;
    //
    //         if (target == null)
    //         {
    //             UnityEngine.Debug.Log("No target found");
    //             return HasToReturn.canContinue;
    //         }
    //
    //         if (target.GetComponent<Player>() == PlayerManager.instance.GetPlayerWithID(BlasterOwnerPlayerID))
    //         {
    //             UnityEngine.Debug.Log("Player is the same");
    //             return HasToReturn.canContinue;
    //         }
    //
    //         if (Vector2.Distance(target.position, initialPosition) >= MaxDistance)
    //         {
    //             UnityEngine.Debug.Log("Distance too far");
    //             return HasToReturn.canContinue;
    //         }
    //
    //         Vector2 blasterOrbitPosition = CalculateRandomPosition(target.position);
    //         Quaternion blasterOrientation = CalculateRotation(target.position, blasterOrbitPosition);
    //
    //         Vector2 direction = (Vector2)target.position - blasterOrbitPosition;
    //
    //         // NetworkingManager.RPC(
    //         //     typeof(GasterBlasterSpawner), nameof(RPC_SpawnGaster),
    //         //     blasterOrbitPosition, blasterOrientation, direction, GasterOwnerPlayerID);
    //
    //         RPC_SpawnGaster(blasterOrbitPosition, blasterOrientation, direction, BlasterOwnerPlayerID);
    //
    //         done = true;
    //         return HasToReturn.canContinue;
    //     }
    //
    //     // [UnboundRPC]
    //     private static void RPC_SpawnGaster(Vector2 position, Quaternion rotation, Vector2 direction, int playerID)
    //     {
    //         if (!PhotonNetwork.IsMasterClient && !PhotonNetwork.OfflineMode) return;
    //
    //         UnityEngine.Debug.Log("Spawning GasterBlaster in the network");
    //         GameObject gasterBlaster = PhotonNetwork.Instantiate(
    //             Assets.GasterBlasterSprite.name, position, rotation);
    //
    //         GasterBlasterBehaviour gasterBlasterBehaviour = gasterBlaster.GetComponent<GasterBlasterBehaviour>();
    //         // gasterBlasterBehaviour.isOwner = true;
    //         gasterBlasterBehaviour.BlasterOwnerPlayerID = playerID;
    //         gasterBlasterBehaviour.Direction = direction;
    //
    //         PhotonView blasterPhotonView = gasterBlaster.GetComponent<PhotonView>();
    //
    //         NetworkingManager.RPC(
    //             typeof(GasterBlasterSpawner), nameof(RPC_UpdateGasterProperties),
    //             blasterPhotonView.ViewID, playerID, direction);
    //     }
    //
    //     [UnboundRPC]
    //     private static void RPC_UpdateGasterProperties(int viewID, int playerID, Vector2 direction)
    //     {
    //         PhotonView photonView = PhotonView.Find(viewID);
    //
    //         if (photonView == null)
    //         {
    //             UnityEngine.Debug.LogError("PhotonView for blaster props not found");
    //             return;
    //         }
    //         GasterBlasterBehaviour gasterBlasterBehaviour = photonView.GetComponent<GasterBlasterBehaviour>();
    //
    //         if (gasterBlasterBehaviour == null) return;
    //
    //         gasterBlasterBehaviour.BlasterOwnerPlayerID = playerID;
    //         gasterBlasterBehaviour.Direction = direction;
    //
    //         UnityEngine.Debug.Log("Updated GasterBlaster properties for clients");
    //     }
    //
    //     private static Quaternion CalculateRotation(Vector2 target, Vector2 position)
    //     {
    //         Vector2 direction = target - position;
    //         float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90;
    //         return Quaternion.AngleAxis(angle, Vector3.forward);
    //     }
    //
    //     private static Vector2 CalculateRandomPosition(Vector2 target)
    //     {
    //         Vector2 position = Vector2.zero;
    //         Camera camera = GameObject.Find("MainCamera").GetComponent<Camera>();
    //
    //         var tries = 0;
    //         while (!(tries > 50))
    //         {
    //             position = target + Random.insideUnitCircle * 15;
    //             Vector3 viewport = camera.WorldToViewportPoint(position);
    //             bool inCameraFrustum = Is01(viewport.x) && Is01(viewport.y);
    //             if (inCameraFrustum)
    //             {
    //                 break;
    //             }
    //
    //             tries++;
    //         }
    //
    //         return position;
    //     }
    //
    //     private static bool Is01(float a) => a > 0 && a < 1;
    // }
    //
    // public class GasterBlasterBehaviour : MonoBehaviour
    // {
    //     private const int Duration = 5;
    //     private const string SortingLayerName = "MostFront";
    //     private const int SortingOrder = 105000;
    //     private const float SizeMultiplier = 1.8f;
    //     private static readonly int IsBlasting = Animator.StringToHash("isBlasting");
    //
    //     // public bool isOwner = false;
    //     public int BlasterOwnerPlayerID { get; set; }
    //     public Vector2 Direction { get; set; }
    //
    //     private Animator animator = null!;
    //     private Renderer renderer = null!;
    //
    //     private RemoveAfterSeconds removeAfterSeconds = null!;
    //
    //     private void Awake()
    //     {
    //         animator = GetComponent<Animator>();
    //
    //         removeAfterSeconds = gameObject.AddComponent<RemoveAfterSeconds>();
    //         removeAfterSeconds.seconds = Duration;
    //
    //         renderer = gameObject.GetComponent<Renderer>();
    //         renderer.material.color =
    //             new Color(renderer.material.color.r, renderer.material.color.g, renderer.material.color.b, 0);
    //
    //         renderer.sortingLayerName = SortingLayerName;
    //         renderer.sortingOrder = SortingOrder;
    //     }
    //
    //     private void Start()
    //     {
    //         UnityEngine.Debug.Log("Blaster viewID: " + PhotonView.Get(this).ViewID);
    //         UnityEngine.Debug.Log("Blaster owner: " + BlasterOwnerPlayerID);
    //         // UnityEngine.Debug.Log(isOwner ? "Starting GasterBlaster for owner" : "Starting GasterBlaster for visual");
    //         gameObject.transform.localScale = new Vector3(SizeMultiplier, SizeMultiplier, transform.localScale.z);
    //
    //         // PhotonView view = GetComponent<PhotonView>();
    //
    //         // if (!PhotonNetwork.IsMasterClient && !PhotonNetwork.OfflineMode) return;
    //         // this.ExecuteAfterSeconds(0.1f,
    //         //     () =>
    //         //     {
    //         //         view.RPC(nameof(RPC_ShootLaser), RpcTarget.All, transform.position, Direction);
    //         //     });
    //
    //         // NetworkingManager.RPC(
    //         //     typeof(GasterBlasterBehaviour), nameof(RPC_ShootLaser),
    //         //     transform.position, Direction);
    //
    //         StartCoroutine(ShootLaser(transform.position));
    //     }
    //
    //     // [UnboundRPC]
    //     // private void RPC_ShootLaser(Vector3 position, Vector2 direction)
    //     // {
    //     //     UnityEngine.Debug.Log("Starting RPC shoot laser");
    //     //     StartCoroutine(ShootLaser(position, direction));
    //     // }
    //
    //     private IEnumerator ShootLaser(Vector3 position)
    //     {
    //         yield return FadeIn();
    //
    //         animator.SetTrigger(IsBlasting);
    //         AudioController.Play(Assets.GasterBlasterNoise, transform);
    //
    //         Player player = PlayerManager.instance.GetPlayerWithID(BlasterOwnerPlayerID);
    //         Gun gun = player.GetComponent<Holding>().holdable.GetComponent<Gun>();
    //         yield return BlastEffect(player, gun, position, Direction);
    //
    //         yield return new WaitForSeconds(1.2f);
    //         animator.SetTrigger(IsBlasting);
    //
    //         yield return new WaitForSeconds(0.5f);
    //         yield return FadeOut();
    //
    //         if (PhotonNetwork.IsMasterClient)
    //         {
    //             Unbound.Instance.ExecuteAfterFrames(1, () => PhotonNetwork.Destroy(gameObject));
    //         }
    //     }
    //
    //     private static List<MonoBehaviour> BlastEffect(Player player, Gun gun, Vector3 position, Vector2 direction)
    //     {
    //         UnityEngine.Debug.Log("Starting BlastEffect");
    //         Gun newGun = player.gameObject.AddComponent<Blast>();
    //
    //         SpawnBulletsEffect effect = player.gameObject.AddComponent<SpawnBulletsEffect>();
    //
    //         var damage = player.data.weaponHandler.gun.damage / 4f;
    //         var projectileSize = 1f / damage;
    //
    //         effect.SetDirection(direction);
    //         effect.SetPosition(position);
    //         effect.SetNumBullets(20);
    //         effect.SetTimeBetweenShots(0.004f);
    //
    //         SpawnBulletsEffect.CopyGunStats(gun, newGun);
    //         newGun.damage = damage;
    //         newGun.damageAfterDistanceMultiplier = 1f;
    //         newGun.reflects = 0;
    //         newGun.bulletDamageMultiplier = 1f;
    //         newGun.projectileSpeed = 2f;
    //         newGun.projectielSimulatonSpeed = 1f;
    //         newGun.projectileSize = projectileSize;
    //         newGun.projectileColor = Color.white;
    //         newGun.spread = 0f;
    //         newGun.gravity = 0f;
    //         newGun.destroyBulletAfter = 5f;
    //         newGun.numberOfProjectiles = 1;
    //         newGun.ignoreWalls = true;
    //         newGun.objectsToSpawn = new[] { PreventRecursion.StopRecursionObjectToSpawn };
    //
    //         Traverse.Create(newGun).Field("spreadOfLastBullet").SetValue(0f);
    //         effect.SetGun(newGun);
    //
    //         return new List<MonoBehaviour> { effect };
    //     }
    //
    //     public class Blast : Gun
    //     {
    //     }
    //
    //     private IEnumerator FadeOut()
    //     {
    //         while (renderer.material.color.a > 0)
    //         {
    //             Color objectColor = renderer.material.color;
    //             float fadeAmount = objectColor.a - 5 * TimeHandler.deltaTime;
    //             objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
    //             renderer.material.color = objectColor;
    //             yield return null;
    //         }
    //     }
    //
    //     private IEnumerator FadeIn()
    //     {
    //         while (renderer.material.color.a < 1)
    //         {
    //             Color objectColor = renderer.material.color;
    //             float fadeAmount = objectColor.a + 5 * TimeHandler.deltaTime;
    //             objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
    //             renderer.material.color = objectColor;
    //             yield return null;
    //         }
    //     }
    // }
}