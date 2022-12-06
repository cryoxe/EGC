using UnityEngine;
using System.Collections.Generic;
using HarmonyLib;
using ModdingUtils.MonoBehaviours;
using UnboundLib;
using ExtraGameCards.AssetsEmbedded;
using ExtraGameCards.Utils;
using Photon.Pun;
using System.Collections;
using UnboundLib.GameModes;
using Sonigon;

namespace ExtraGameCards.MonoBehaviours
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




        void Start()
        {

            gameObject.transform.localScale = new Vector3(1.8f, 1.8f, transform.localScale.z);

            Color objectColor = gameObject.GetComponent<Renderer>().material.color;
            gameObject.GetComponent<Renderer>().material.color = new Color(objectColor.r, objectColor.g, objectColor.b, 0);
            gameObject.GetComponent<Renderer>().sortingLayerName = "MostFront";
            gameObject.GetComponent<Renderer>().sortingOrder = 1048575;

            animator = GetComponent<Animator>();

            StartCoroutine(shootLaser());
        }

        IEnumerator shootLaser()
        {
            yield return FadeIn();
            animator.SetTrigger("isBlasting");
            AudioController.Play(Assets.GasterBlasterNoise, transform);
            yield return BlastEffect(player, gun, gunAmmo, data, health, gravity, block, statModifiers);
            yield return new WaitForSeconds(1.2f);
            animator.SetTrigger("isBlasting");
            yield return new WaitForSeconds(0.5f);
            yield return FadeOut(true);
        }

        private IEnumerator FadeOut(bool destroyAfter = false)
        {
            while (this.GetComponent<Renderer>().material.color.a > 0)
            {
                Color objectColor = this.GetComponent<Renderer>().material.color;
                float fadeAmount = objectColor.a - (5 * TimeHandler.deltaTime);
                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                this.GetComponent<Renderer>().material.color = objectColor;
                yield return null;
            }
            if (destroyAfter) { Destroy(gameObject); }
        }
        private IEnumerator FadeIn()
        {
            while (this.GetComponent<Renderer>().material.color.a < 1)
            {
                Color objectColor = this.GetComponent<Renderer>().material.color;
                float fadeAmount = objectColor.a + (5 * TimeHandler.deltaTime);
                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                this.GetComponent<Renderer>().material.color = objectColor;
                yield return null;
            }
        }

        public List<MonoBehaviour> BlastEffect(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
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
            newGun.objectsToSpawn = new ObjectsToSpawn[] { PreventRecursion.stopRecursionObjectToSpawn };

            Traverse.Create(newGun).Field("spreadOfLastBullet").SetValue(0f);
            effect.SetGun(newGun);

            return new List<MonoBehaviour> { effect };
        }
        public class Blast : Gun
        { }
    }
}
