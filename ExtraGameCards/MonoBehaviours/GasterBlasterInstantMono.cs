using System.Collections;
using System.Collections.Generic;
using EGC.AssetsEmbedded;
using EGC.Extensions;
using EGC.Utils;
using HarmonyLib;
using UnboundLib;
using UnityEngine;

namespace EGC.MonoBehaviours
{
    public class GasterBlasterInstantMono : MonoBehaviour
    {
        public Player player;
        public Gun gun;
        public CharacterData data;
        public HealthHandler health;
        public Gravity gravity;
        public Block block;
        public GunAmmo gunAmmo;
        public CharacterStatModifiers statModifiers;
        public GasterBlasterMono gasterBlasterSound;

        public Vector2 originPos;
        public Vector2 targetPos;
        public Vector2 direction;
        public Quaternion rotation;

        private Animator animator;
        private RemoveAfterSeconds removeAfterSeconds;


        private void Start()
        {
            removeAfterSeconds = gameObject.AddComponent<RemoveAfterSeconds>();
            removeAfterSeconds.seconds = 4;

            gameObject.transform.localScale = new Vector3(1.8f, 1.8f, transform.localScale.z);

            Color objectColor = gameObject.GetComponent<Renderer>().material.color;
            gameObject.GetComponent<Renderer>().material.color =
                new Color(objectColor.r, objectColor.g, objectColor.b, 0);
            gameObject.GetComponent<Renderer>().sortingLayerName = "MostFront";
            gameObject.GetComponent<Renderer>().sortingOrder = 1048575;

            animator = GetComponent<Animator>();

            StartCoroutine(shootLaser());
        }

        private void Update()
        {
            if (!(bool)data.playerVel.GetFieldValue("simulated"))
            {
                UnityEngine.Debug.Log("Player not simulated !");
                CharacterStatModifiersExtension.GetAdditionalData(statModifiers).numberOfGaster--;
                UnityEngine.Debug.Log(
                    $"Minus One Gaster Blaster : {CharacterStatModifiersExtension.GetAdditionalData(statModifiers).numberOfGaster} total");
                Destroy(gameObject);
            }
        }

        private IEnumerator shootLaser()
        {
            yield return FadeIn();
            animator.SetTrigger("isBlasting");
            AudioController.Play(Assets.GasterBlasterNoise, transform);
            yield return BlastEffect(player, gun, gunAmmo, data, health, gravity, block, statModifiers);
            yield return new WaitForSeconds(1.2f);
            animator.SetTrigger("isBlasting");
            yield return new WaitForSeconds(0.5f);
            yield return FadeOut();

            CharacterStatModifiersExtension.GetAdditionalData(statModifiers).numberOfGaster--;
            UnityEngine.Debug.Log(
                $"Minus One Gaster Blaster : {CharacterStatModifiersExtension.GetAdditionalData(statModifiers).numberOfGaster} total");
            Destroy(gameObject);
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

        public List<MonoBehaviour> BlastEffect(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data,
            HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            Gun newGun = player.gameObject.AddComponent<Blast>();

            SpawnBulletsEffect effect = player.gameObject.AddComponent<SpawnBulletsEffect>();

            effect.SetDirection(direction);
            effect.SetPosition(originPos);
            effect.SetNumBullets(20);
            effect.SetTimeBetweenShots(0.004f);

            SpawnBulletsEffect.CopyGunStats(gun, newGun);
            newGun.damage = 8f;
            newGun.damageAfterDistanceMultiplier = 1f;
            newGun.reflects = 0;
            newGun.bulletDamageMultiplier = 1f;
            newGun.projectileSpeed = 2f;
            newGun.projectielSimulatonSpeed = 1f;
            newGun.projectileSize = 10f;
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

        public class Blast : Gun
        {
        }
    }
}