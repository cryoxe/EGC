using EGC.AssetsEmbedded;
using UnboundLib.Cards;
using UnityEngine;

namespace EGC.Cards.MarkovChoice
{
    internal class Madness : CustomCard
    {
        public static CardInfo MadnessCard = null!;

        public override bool GetEnabled() => false;

        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats,
            CharacterStatModifiers statModifiers, Block block)
        {
            cardInfo.categories = new[]
            {
                ExtraGameCards.Markov
            };
            gun.attackSpeed = 1.4f;
            gun.reflects = 1;
            gun.spread = 0.2f;
        }

        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity,
            Block block, CharacterStatModifiers characterStats) { }

        protected override string GetTitle() => "Madness";

        protected override string GetDescription() => "You have lost your mind.";

        protected override GameObject GetCardArt() => Assets.MarkovEyeMadnessArt;

        protected override CardInfo.Rarity GetRarity() => CardInfo.Rarity.Common;

        protected override CardInfoStat[] GetStats()
        {
            return new[]
            {
                new CardInfoStat
                {
                    positive = false,
                    stat = "Spread",
                    amount = "Way More",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
                new CardInfoStat
                {
                    positive = true,
                    stat = "Attack Speed",
                    amount = "+40%",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
                new CardInfoStat
                {
                    positive = true,
                    stat = "Bounce",
                    amount = "+1",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
            };
        }

        protected override CardThemeColor.CardThemeColorType GetTheme()
        {
            return CardThemeColor.CardThemeColorType.MagicPink;
        }

        public override string GetModName()
        {
            return ExtraGameCards.ModInitials;
        }
    }
}