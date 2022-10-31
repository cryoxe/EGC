using ExtraGameCards.AssetsEmbedded;
using ExtraGameCards.MonoBehaviours;
using ModdingUtils.Extensions;
using ModsPlus;
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
                    stat = "Blast",
                    amount = "10%",
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
        protected override void Added(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            // Card added to the player!
        }
        protected override void Removed(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            // Card removed from the player!
        }
    }
    public class BlastEffect : CardEffect
    {
        public override void OnShoot(GameObject projectile)
        {
            System.Random random = new System.Random();
            if (random.Next(0, 100) < 50)
            {
                GatserBlasterMono sensor = projectile.AddComponent<GatserBlasterMono>();
                sensor.health = health;
                sensor.gravity = gravity;
                sensor.block = block;
                sensor.data = data;
                sensor.player = player;
                sensor.gun = gun;
                sensor.gunAmmo = gunAmmo;
            }
        }

        public override 
    }
}