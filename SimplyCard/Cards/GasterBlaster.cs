using ExtraGameCards.AssetsEmbedded;
using ExtraGameCards.MonoBehaviours;
using ModdingUtils.Extensions;
using ModsPlus;
using Photon.Pun;
using System.Collections;
using UnboundLib;
using UnityEngine;

namespace ExtraGameCards.Cards
{
    class GasterBlaster : CustomEffectCard<BlastEffect>
    {
        public override CardDetails Details => new CardDetails
        {
            Title = "Gaster Blaster",
            Description = "your opponents are gonna have a bad time",
            ModName = EGC.ModInitials,
            Art = Assets.GasterBlasterArt,
            Rarity = CardInfo.Rarity.Rare,
            Theme = CardThemeColor.CardThemeColorType.TechWhite,
            Stats = new CardInfoStat[]
            {
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Blast Chance",
                    amount = "50%",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Attack Speed",
                    amount = "+20%",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
                new CardInfoStat()
                {
                    positive = false,
                    stat = "Health",
                    amount = "-30%",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
            }
        };

        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            //UnityEngine.Debug.Log($"[{ExtraCards.ModInitials}][Card] {GetTitle()} has been setup.");
            cardInfo.GetAdditionalData().canBeReassigned = false;
            cardInfo.allowMultiple = false;

            statModifiers.health = 0.7f;
            gun.attackSpeed = 0.8f;
        }
    }
    public class BlastEffect : CardEffect
    {

        public override void OnShoot(GameObject projectile)
        {
            ProjectileHit pH = projectile.GetComponent<ProjectileHit>();
            Gun gun = player.GetComponent<Holding>().holdable.GetComponent<Gun>();

            if (pH.ownWeapon == gun.gameObject)
            {
                UnityEngine.Debug.Log("Was shoot by player");
                GasterBlasterMono sensor = projectile.AddComponent<GasterBlasterMono>();
                sensor.statModifiers = characterStats;
                sensor.health = health;
                sensor.gravity = gravity;
                sensor.block = block;
                sensor.data = data;
                sensor.player = player;
                sensor.gun = gun;
                sensor.gunAmmo = gunAmmo;
            }

        }
    }

}