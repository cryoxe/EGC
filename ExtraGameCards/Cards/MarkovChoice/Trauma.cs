using EGC.AssetsEmbedded;
using UnboundLib.Cards;
using UnityEngine;

namespace EGC.Cards.MarkovChoice
{
    internal class Trauma : CustomCard
    {
        public static CardInfo TraumaCard = null!;

        public override bool GetEnabled() => false;

        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats,
            CharacterStatModifiers statModifiers, Block block)
        {
            cardInfo.categories = new[]
            {
                ExtraGameCards.Markov
            };
            gun.attackSpeed = 0.6f;
        }

        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data,
            HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            characterStats.lifeSteal = (characterStats.lifeSteal != 0f)
                ? (characterStats.lifeSteal * 0.5f)
                : (characterStats.lifeSteal - 0.5f);
        }

        protected override string GetTitle() => "Trauma";

        protected override string GetDescription() => "You throw this cursed book away.";

        protected override GameObject GetCardArt() => Assets.MarkovEyeTraumaArt;

        protected override CardInfo.Rarity GetRarity() => CardInfo.Rarity.Common;

        protected override CardInfoStat[] GetStats()
        {
            return new[]
            {
                new CardInfoStat
                {
                    positive = false,
                    stat = "Life Steal",
                    amount = "-50%",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },

                new CardInfoStat
                {
                    positive = true,
                    stat = "Attack Speed",
                    amount = "+40%",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
            };
        }

        protected override CardThemeColor.CardThemeColorType GetTheme()
        {
            return CardThemeColor.CardThemeColorType.DestructiveRed;
        }

        public override string GetModName()
        {
            return ExtraGameCards.ModInitials;
        }
    }
}