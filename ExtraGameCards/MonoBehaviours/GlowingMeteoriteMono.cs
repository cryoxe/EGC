using System;
using System.Collections.Generic;
using EGC.Extensions;
using EGC.Utils;
using ModdingUtils.MonoBehaviours;
using UnboundLib;
using UnityEngine;

namespace EGC.MonoBehaviours
{
    internal class GlowingMeteoriteMono : ReversibleEffect
    {
        private float duration = 10f;

        public Player player;
        public Gun gun;
        public CharacterData data;
        public HealthHandler health;
        public Gravity gravity;
        public Block block;
        public GunAmmo gunAmmo;
        public CharacterStatModifiers statModifiers;

        private void OnBlock(BlockTrigger.BlockTriggerType trigger)
        {
            if (duration <= 0)
            {
                MeteorEffect(player, gun, gunAmmo, data, health, gravity, block, statModifiers);
                duration = 10f;
            }

            player = gameObject.GetComponent<Player>();
        }

        public override void OnStart()
        {
            block.BlockAction += (Action<BlockTrigger.BlockTriggerType>)Delegate.Combine(block.BlockAction,
                new Action<BlockTrigger.BlockTriggerType>(OnBlock));
            SetLivesToEffect(int.MaxValue);
            duration = 0f;
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
            duration = 10f;
            block.BlockAction = (Action<BlockTrigger.BlockTriggerType>)Delegate.Remove(block.BlockAction,
                new Action<BlockTrigger.BlockTriggerType>(OnBlock));
        }


        public List<MonoBehaviour> MeteorEffect(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data,
            HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            Gun newGun = gameObject.AddComponent<Meteor>();

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
            newGun.objectsToSpawn = new[] { PreventRecursion.StopRecursionObjectToSpawn };

            effect.SetGun(newGun);

            ColorFlash thisColorFlash = this.player.gameObject.GetOrAddComponent<ColorFlash>();
            thisColorFlash.SetNumberOfFlashes(10);
            thisColorFlash.SetDuration(0.15f);
            thisColorFlash.SetDelayBetweenFlashes(0.15f);
            thisColorFlash.SetColor(Color.cyan);

            return new List<MonoBehaviour> { effect };
        }

        public class Meteor : Gun
        {
        }
    }
}