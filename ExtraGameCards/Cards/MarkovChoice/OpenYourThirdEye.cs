using EGC.AssetsEmbedded;
using UnboundLib.Cards;
using UnityEngine;

namespace EGC.Cards.MarkovChoice
{
    internal class OpenYourThirdEye : CustomCard
    {
        public static CardInfo OpenYourThirdEyeCard = null!;

        public override bool GetEnabled() => false;

        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats,
            CharacterStatModifiers statModifiers, Block block)
        {
            cardInfo.categories = new[]
            {
                ExtraGameCards.Markov
            };

            statModifiers.health = 0.6f;
            gun.damage = 1.3f;
            gun.ammo = 1;
        }

        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data,
            HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats) { }

        protected override string GetTitle() => "Open Your Third Eye";

        protected override string GetDescription() => "And finally become complete.";

        protected override GameObject GetCardArt() => Assets.MarkovEyeThirdArt;

        protected override CardInfo.Rarity GetRarity() => CardInfo.Rarity.Common;

        protected override CardInfoStat[] GetStats()
        {
            return new[]
            {
                new CardInfoStat
                {
                    positive = false,
                    stat = "Health",
                    amount = "-40%",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },

                new CardInfoStat
                {
                    positive = true,
                    stat = "Damage",
                    amount = "+30%",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },

                new CardInfoStat
                {
                    positive = true,
                    stat = "Ammo",
                    amount = "+1",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                }
            };
        }

        protected override CardThemeColor.CardThemeColorType GetTheme()
        {
            return CardThemeColor.CardThemeColorType.EvilPurple;
        }

        public override string GetModName()
        {
            return ExtraGameCards.ModInitials;
        }
    }
}