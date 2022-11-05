using UnityEngine;
using ModdingUtils.MonoBehaviours;
using UnboundLib.Cards;
using UnboundLib.GameModes;
using UnboundLib;
using ModdingUtils.Extensions;
using static ModdingUtils.Utils.Cards;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using SimplyCard.Cards;
using ExtraGameCards.Utils;
using HarmonyLib;

namespace ExtraGameCards.MonoBehaviours
{
    internal class GlowingMeteoriteMono : ReversibleEffect
    {
        private  float duration = 12f;

        private Player player;
        private Gun gun;
        private CharacterData data;
        private HealthHandler health;
        private Gravity gravity;
        private Block block;
        private GunAmmo gunAmmo;
        private CharacterStatModifiers statModifiers;

     
        void Awake()
        {
            this.player = gameObject.GetComponent<Player>();
            this.gun = this.player.GetComponent<Holding>().holdable.GetComponent<Gun>();
            this.data = this.player.GetComponent<CharacterData>();
            this.health = this.player.GetComponent<HealthHandler>();
            this.gravity = this.player.GetComponent<Gravity>();
            this.block = this.player.GetComponent<Block>();
            this.gunAmmo = this.gun.GetComponentInChildren<GunAmmo>();
            this.statModifiers = this.player.GetComponent<CharacterStatModifiers>();
        }
        private void OnBlock(BlockTrigger.BlockTriggerType trigger)
        {
            if (duration <= 0)
            {
                MeteorEffect(player, gun, gunAmmo, data, health, gravity, block, statModifiers);
                duration = 12f;
            }

            
            this.player = base.gameObject.GetComponent<Player>();
        }

        public override void OnStart()
        {
            block.BlockAction = (Action<BlockTrigger.BlockTriggerType>)Delegate.Combine(block.BlockAction, new Action<BlockTrigger.BlockTriggerType>(OnBlock));
            SetLivesToEffect(int.MaxValue);
        }
        public override void OnUpdate()
        {
            if (!(duration <= 0))
            {
                duration -= TimeHandler.deltaTime;
                //UnityEngine.Debug.Log(duration);
            }
        }
        public override void OnOnDestroy()
        {
            duration = 12f;
            block.BlockAction = (Action<BlockTrigger.BlockTriggerType>)Delegate.Remove(block.BlockAction, new Action<BlockTrigger.BlockTriggerType>(OnBlock));
        }


        public List<MonoBehaviour> MeteorEffect(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            Gun newGun = this.gameObject.AddComponent<Meteor>();

            SpawnBulletsEffect effect = player.gameObject.AddComponent<SpawnBulletsEffect>();
            effect.SetDirection(new Vector3(0f, -1f, 0f));
            effect.SetPosition(new Vector3(0f, 100f, 0f));
            effect.SetNumBullets(40);
            effect.SetTimeBetweenShots(0.03f);

            SpawnBulletsEffect.CopyGunStats(gun, newGun);

            newGun.damage = 1.5f;
            newGun.damageAfterDistanceMultiplier = 1.2f;
            newGun.reflects = 0;
            newGun.explodeNearEnemyRange = 6f;
            newGun.explodeNearEnemyDamage = 2f;
            newGun.bulletDamageMultiplier = 1f;
            newGun.projectileSpeed = 1.1f;
            newGun.projectielSimulatonSpeed = 1f;
            newGun.projectileSize = 2f;
            newGun.projectileColor = Color.cyan;
            newGun.spread = 0.75f;
            newGun.destroyBulletAfter = 100f;
            newGun.numberOfProjectiles = 1;
            newGun.ignoreWalls = false;
            newGun.damageAfterDistanceMultiplier = 1f;
            newGun.objectsToSpawn = new ObjectsToSpawn[] { PreventRecursion.stopRecursionObjectToSpawn };

            effect.SetGun(newGun);

            ColorFlash thisColorFlash = this.player.gameObject.GetOrAddComponent<ColorFlash>();
            thisColorFlash.SetNumberOfFlashes(10);
            thisColorFlash.SetDuration(0.15f);
            thisColorFlash.SetDelayBetweenFlashes(0.15f);
            thisColorFlash.SetColor(Color.cyan);

            return new List<MonoBehaviour> { effect };
        }
        public class Meteor : Gun
        { }
    }
}

