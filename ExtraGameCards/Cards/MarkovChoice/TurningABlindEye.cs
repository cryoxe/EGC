using EGC.AssetsEmbedded;
using UnboundLib.Cards;
using UnityEngine;

namespace EGC.Cards.MarkovChoice
{
    internal class TurningABlindEye : CustomCard
    {
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats,
            CharacterStatModifiers statModifiers, Block block)
        {
            cardInfo.categories = new[]
            {
                ExtraGameCards.Markov
            };

            statModifiers.health = 1.45f;
            gun.damage = 0.8f;
            statModifiers.movementSpeed = 0.8f;
            block.cdAdd = -0.25f;
        }

        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data,
            HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats) { }


        protected override string GetTitle() => "Turning a blind eye";

        protected override string GetDescription() => "You refuse to believe what you just read.";

        protected override GameObject GetCardArt() => Assets.MarkovBlindEyeArt;

        protected override CardInfo.Rarity GetRarity() => CardInfo.Rarity.Common;

        protected override CardInfoStat[] GetStats()
        {
            return new[]
            {
                new CardInfoStat
                {
                    positive = false,
                    stat = "Damage",
                    amount = "-20%",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },

                new CardInfoStat
                {
                    positive = false,
                    stat = "Speed",
                    amount = "-20%",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
                new CardInfoStat
                {
                    positive = true,
                    stat = "Health",
                    amount = "+45%",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
                new CardInfoStat
                {
                    positive = true,
                    stat = "Block Cooldown",
                    amount = "+0.25s",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
            };
        }

        protected override CardThemeColor.CardThemeColorType GetTheme()
        {
            return CardThemeColor.CardThemeColorType.TechWhite;
        }

        public override string GetModName()
        {
            return ExtraGameCards.ModInitials;
        }
    }
}