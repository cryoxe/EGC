using ExtraGameCards.AssetsEmbedded;
using ExtraGameCards.MonoBehaviours;
using ModdingUtils.Extensions;
using ModsPlus;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ExtraGameCards.Cards
{
    class BooMushroom : SimpleCard
    {
        public static CardInfo booMushroomCard;
        public override CardDetails Details => new CardDetails
        {
            Title = "Boo Mushroom",
            Description = "You're a ghost now !",
            ModName = EGC.ModInitials,
            Art = Assets.BooMushArt,
            Rarity = CardInfo.Rarity.Uncommon,
            Theme = CardThemeColor.CardThemeColorType.TechWhite,
            Stats = new CardInfoStat[]
            {
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Bullets",
                    amount = "+ Ghost",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Damage",
                    amount = "Unblockable",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Attack Speed",
                    amount = "+10%",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
                new CardInfoStat()
                {
                    positive = false,
                    stat = "Damage",
                    amount = "-30%",
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
            cardInfo.allowMultiple = false;

            cardInfo.categories = new CardCategory[]
            {
                EGC.MarioPowerUps
            };

            //UnityEngine.Debug.Log($"[{ExtraCards.ModInitials}][Card] {GetTitle()} has been setup.");
            gun.projectileColor = new Color32(227, 226, 217, 210);
            statModifiers.health = 0.7f;
            gun.damage = 0.7f;
            gun.unblockable = true;
            gun.ignoreWalls = true;
        }

        protected override void Added(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {

            gun.attackSpeed -= 0.1f;
        }
    }
}