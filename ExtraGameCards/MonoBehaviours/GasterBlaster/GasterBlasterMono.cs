using System.Collections;
using System.Collections.Generic;
using EGC.AssetsEmbedded;
using EGC.Extensions.SpawnBullet;
using EGC.Utils;
using HarmonyLib;
using ModsPlus;
using Photon.Pun;
using UnboundLib;
using UnityEngine;

namespace EGC.MonoBehaviours.GasterBlaster
{
public class GasterBlasterMono : MonoBehaviour
    {
        private const int Duration = 5;
        private const string SortingLayerName = "MostFront";
        private const int SortingOrder = 105000;
        private const float SizeMultiplier = 1.8f;
        private static readonly int IsBlasting = Animator.StringToHash("isBlasting");

        public int BlasterOwnerPlayerID { get; set; }
        public Vector2 Direction { get; set; }

        private Animator animator = null!;
        private Renderer renderer = null!;

        private AudioSource audioSource = null!;
        private RemoveAfterSeconds removeAfterSeconds = null!;

        private void Awake()
        {
            Cards.BlastEffect.GetBlastEffect(BlasterOwnerPlayerID)!.BlasterNumber++;

            audioSource = gameObject.GetComponent<AudioSource>();

            animator = GetComponent<Animator>();

            removeAfterSeconds = gameObject.AddComponent<RemoveAfterSeconds>();
            removeAfterSeconds.seconds = Duration;

            renderer = gameObject.GetComponent<Renderer>();
            renderer.material.color =
                new Color(renderer.material.color.r, renderer.material.color.g, renderer.material.color.b, 0);

            renderer.sortingLayerName = SortingLayerName;
            renderer.sortingOrder = SortingOrder;

            gameObject.transform.localScale = new Vector3(SizeMultiplier, SizeMultiplier, transform.localScale.z);
        }

        private void Start() => StartCoroutine(ShootLaser(transform.position));

        private IEnumerator ShootLaser(Vector3 position)
        {
            yield return FadeIn();

            animator.SetTrigger(IsBlasting);
            // AudioController.Play(Assets.GasterBlasterNoise, transform);
            if (audioSource)
            {
                audioSource.PlayOneShot(Assets.GasterBlasterNoise,
                    0.7f * Optionshandler.vol_Master * Optionshandler.vol_Sfx);
            }


            Player player = PlayerManager.instance.GetPlayerWithID(BlasterOwnerPlayerID);
            Gun gun = player.GetComponent<Holding>().holdable.GetComponent<Gun>();
            yield return BlastEffect(player, gun, position, Direction);

            yield return new WaitForSeconds(1.2f);
            animator.SetTrigger(IsBlasting);

            yield return new WaitForSeconds(0.5f);
            yield return FadeOut();

            if (PhotonNetwork.IsMasterClient)
            {
                Unbound.Instance.ExecuteAfterFrames(1, () => PhotonNetwork.Destroy(gameObject));
            }
        }

        private static List<MonoBehaviour> BlastEffect(Player player, Gun gun, Vector3 position, Vector2 direction)
        {
            Gun newGun = player.gameObject.AddComponent<Blast>();

            SpawnBulletsEffect effect = player.gameObject.AddComponent<SpawnBulletsEffect>();

            var damage = player.data.weaponHandler.gun.damage / 4f;
            var projectileSize = 1f / damage;

            effect.SetDirection(direction);
            effect.SetPosition(position);
            effect.SetNumBullets(20);
            effect.SetTimeBetweenShots(0.004f);

            SpawnBulletsEffect.CopyGunStats(gun, newGun);
            newGun.damage = damage;
            newGun.damageAfterDistanceMultiplier = 1f;
            newGun.reflects = 0;
            newGun.bulletDamageMultiplier = 1f;
            newGun.projectileSpeed = 2f;
            newGun.projectielSimulatonSpeed = 1f;
            newGun.projectileSize = projectileSize;
            newGun.projectileColor = Color.white;
            newGun.spread = 0f;
            newGun.gravity = 0f;
            newGun.destroyBulletAfter = 5f;
            newGun.numberOfProjectiles = 1;
            newGun.ignoreWalls = true;
            newGun.objectsToSpawn = new[] { PreventRecursion.StopRecursionObjectToSpawn };

            Traverse.Create(newGun).Field("spreadOfLastBullet").SetValue(0f);
            effect.SetGun(newGun);

            return new List<MonoBehaviour> { effect };
        }

        public class Blast : Gun
        {
        }

        private IEnumerator FadeOut()
        {
            while (renderer.material.color.a > 0)
            {
                Color objectColor = renderer.material.color;
                float fadeAmount = objectColor.a - 5 * TimeHandler.deltaTime;
                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                renderer.material.color = objectColor;
                yield return null;
            }
        }

        private IEnumerator FadeIn()
        {
            while (renderer.material.color.a < 1)
            {
                Color objectColor = renderer.material.color;
                float fadeAmount = objectColor.a + 5 * TimeHandler.deltaTime;
                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                renderer.material.color = objectColor;
                yield return null;
            }
        }

        private void OnDestroy()
        {
            Cards.BlastEffect.GetBlastEffect(BlasterOwnerPlayerID)!.BlasterNumber--;
        }
    }
}