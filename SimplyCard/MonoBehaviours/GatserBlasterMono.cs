﻿using UnityEngine;
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

        private GameObject blasterSprite;
        private Animator blastAnimator;

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
        
            AudioSource blasterNoise = gameObject.GetOrAddComponent<AudioSource>();
            blasterNoise.PlayOneShot(Assets.GasterBlasterNoise, 0.9f);
            BlastEffect(player, gun, gunAmmo, data, health, gravity, block, statModifiers);

            

        }

        public List<MonoBehaviour> BlastEffect(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            Gun newGun = this.gameObject.AddComponent<Blast>();

            SpawnBulletsEffect effect = player.gameObject.AddComponent<SpawnBulletsEffect>();

            UnityEngine.Debug.Log("INSTANTIATE GASTERBLASTER");
            Transform transform = gameObject.transform.parent.gameObject.transform.parent.gameObject.transform.parent.gameObject.transform.parent;
            blasterSprite = Instantiate(Assets.GasterBlasterSprite, transform, false);
            Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up);
            blasterSprite.transform.position = originPos;
            blasterSprite.transform.rotation = rotation;
            blastAnimator = blasterSprite.GetComponent<Animator>();
            blastAnimator.SetTrigger("IsBlasting");

            effect.SetDirection(direction);
            effect.SetPosition(originPos);
            effect.SetNumBullets(40);
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
            newGun.destroyBulletAfter = 30f;
            newGun.numberOfProjectiles = 1;
            newGun.ignoreWalls = true;
            newGun.damageAfterDistanceMultiplier = 1f;
            newGun.objectsToSpawn = new ObjectsToSpawn[] { PreventRecursion.stopRecursionObjectToSpawn };

            Traverse.Create(newGun).Field("spreadOfLastBullet").SetValue(0f);
            effect.SetGun(newGun);

            Unbound.Instance.ExecuteAfterSeconds(2f, delegate
            {
                blastAnimator.SetTrigger("IsBlasting");
                Destroy(blasterSprite);

            });

            return new List<MonoBehaviour> { effect };
        }
        public class Blast : Gun
        { }
    }
}