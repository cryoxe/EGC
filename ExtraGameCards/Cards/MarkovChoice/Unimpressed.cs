using EGC.AssetsEmbedded;
using UnboundLib.Cards;
using UnityEngine;

namespace EGC.Cards.MarkovChoice
{
    internal class Unimpressed : CustomCard
    {
        public static CardInfo UnimpressedCard = null!;

        public override bool GetEnabled() => false;

        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats,
            CharacterStatModifiers statModifiers, Block block)
        {
            cardInfo.categories = new[]
            {
                ExtraGameCards.Markov
            };
            gun.projectileSpeed = 0.8f;
            gun.reloadTimeAdd = 0.25f;
            gun.ammoReg = 0.4f;
        }

        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data,
            HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
        }

        protected override string GetTitle() => "Unimpressed";

        protected override string GetDescription() => "Meh... not your style of story";

        protected override GameObject GetCardArt() => Assets.MarkovEyeUnimpressedArt;

        protected override CardInfo.Rarity GetRarity() => CardInfo.Rarity.Common;

        protected override CardInfoStat[] GetStats()
        {
            return new[]
            {
                new CardInfoStat
                {
                    positive = false,
                    stat = "Bullet Speed",
                    amount = "-20%",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },

                new CardInfoStat
                {
                    positive = false,
                    stat = "Reload Speed",
                    amount = "+0.25s",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },

                new CardInfoStat
                {
                    positive = true,
                    stat = "Bullet Regen",
                    amount = "+40%",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                }
            };
        }

        protected override CardThemeColor.CardThemeColorType GetTheme()
        {
            return CardThemeColor.CardThemeColorType.DefensiveBlue;
        }

        public override string GetModName()
        {
            return ExtraGameCards.ModInitials;
        }
    }
}