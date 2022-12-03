using UnityEngine;
using System.Collections.Generic;
using HarmonyLib;
using ModdingUtils.MonoBehaviours;
using UnboundLib;
using ExtraGameCards.AssetsEmbedded;
using ExtraGameCards.Utils;
using Photon.Pun;
using System.Collections;

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

        public Vector2 originPos;
        public Vector2 shootingPos;
        public Vector2 targetPos;
        public Vector2 direction;
        public Quaternion rotation;

        private Animator animator;




        void Start()
        {
            gameObject.transform.localScale = new Vector3(1.2f, 1.2f, transform.localScale.z);
            UnityEngine.Debug.Log($"Blaster is at : {transform.position}");

            animator = GetComponent<Animator>();
            StartCoroutine(shootLaser());
        }

        public IEnumerator Move(float time, Vector3 beginPos, Vector3 endPos)
        {
            UnityEngine.Debug.Log($"Moving toward Shoot Position : {endPos}");
            for (float t = 0; t < 1; t += TimeHandler.deltaTime / time)
            {
                transform.position = Vector3.Lerp(beginPos, endPos, t);
                yield return null;
            }
            UnityEngine.Debug.Log($"Blaster is now at : {transform.position}");
        }

        IEnumerator shootLaser()
        {
            UnityEngine.Debug.Log($"SHOOTING : {shootingPos}, TARGET : {targetPos}, ROTATION : {rotation}");
            yield return new WaitForEndOfFrame();
            UnityEngine.Debug.Log("Begin");
            yield return Move(1.5f, originPos, shootingPos);
            UnityEngine.Debug.Log("Move() Finished");
            animator.SetTrigger("isBlasting");
            AudioSource blasterNoise = gameObject.GetOrAddComponent<AudioSource>();
            blasterNoise.PlayOneShot(Assets.GasterBlasterNoise, 0.9f);
            yield return BlastEffect(player, gun, gunAmmo, data, health, gravity, block, statModifiers);
            UnityEngine.Debug.Log("BlastEffect() Finished");
            animator.SetTrigger("isBlasting");
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
            newGun.destroyBulletAfter = 100f;
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
