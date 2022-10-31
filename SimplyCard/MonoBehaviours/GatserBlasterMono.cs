using UnityEngine;
using System.Collections.Generic;
using HarmonyLib;
using ModdingUtils.MonoBehaviours;
using UnboundLib;
using ExtraGameCards.AssetsEmbedded;
using ExtraGameCards.Utils;

namespace ExtraGameCards.MonoBehaviours
{
    public class GatserBlasterMono : MonoBehaviour
    {
        private float duration = 5f;

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
        private Vector2 direction;

        void Awake()
        {
            ProjectileHit hit = gameObject.GetComponent<ProjectileHit>();
            hit.AddHitAction(OnHit);

            //foreach( Component component in gameObject.GetComponents<Component>())
            //{
            //    UnityEngine.Debug.Log(component);
            //}
        }

        void OnHit()
        {
            UnityEngine.Debug.Log("We hit something");
            targetPos = gameObject.transform.position;
            System.Random random = new System.Random();
            float rPosY = random.Next(-20, ((int)targetPos.y) +21);
            float rPosX = random.Next(-20, ((int)targetPos.x) + 21);
            originPos = new Vector2(rPosX, rPosY);
            direction = targetPos - originPos;
            direction.Normalize();
            BlastEffect(player, gun, gunAmmo, data, health, gravity, block, statModifiers);

        }

        public List<MonoBehaviour> BlastEffect(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            Gun newGun = this.gameObject.AddComponent<Blast>();

            SpawnBulletsEffect effect = player.gameObject.AddComponent<SpawnBulletsEffect>();

            UnityEngine.Debug.Log("TARGET : " + targetPos + "\nRANDOM POSITION : " + originPos + "\nDIRECTION : " + direction);

            effect.SetDirection(direction);
            effect.SetPosition(originPos);
            effect.SetNumBullets(40);
            effect.SetTimeBetweenShots(0.01f);

            SpawnBulletsEffect.CopyGunStats(gun, newGun);

            newGun.damage = 2f;
            newGun.damageAfterDistanceMultiplier = 1f;
            newGun.reflects = 0;
            newGun.bulletDamageMultiplier = 1f;
            newGun.projectileSpeed = 1.2f;
            newGun.projectielSimulatonSpeed = 1f;
            newGun.projectileSize = 10f;
            newGun.projectileColor = Color.white;
            newGun.spread = 0f;
            newGun.gravity = 0f;
            newGun.destroyBulletAfter = 30f;
            newGun.numberOfProjectiles = 1;
            newGun.ignoreWalls = true;
            newGun.damageAfterDistanceMultiplier = 1f;
            newGun.objectsToSpawn = new ObjectsToSpawn[] { PreventRecursion.stopRecursionObjectToSpawn };

            Traverse.Create(newGun).Field("spreadOfLastBullet").SetValue(0f);

            AudioSource blasterNoise = gameObject.GetOrAddComponent<AudioSource>();
            blasterNoise.PlayOneShot(Assets.GasterBlasterNoise, 0.9f);
            effect.SetGun(newGun);

            return new List<MonoBehaviour> { effect };
        }
        public class Blast : Gun
        { }
    }
}
