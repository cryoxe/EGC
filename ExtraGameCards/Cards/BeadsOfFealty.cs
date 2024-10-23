using EGC.AssetsEmbedded;
using ModdingUtils.Extensions;
using UnboundLib;
using UnboundLib.Cards;
using UnityEngine;

namespace EGC.Cards
{
    internal class BeadsOfFealty : CustomCard
    {
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats,
            CharacterStatModifiers statModifiers, Block block)
        {
            cardInfo.allowMultiple = false;

            statModifiers.regen = 5f;
            statModifiers.health = 1.1f;
        }

        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data,
            HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            Unbound.Instance.ExecuteAfterFrames(25,
                () => { player.data.stats.GetAdditionalData().blacklistedCategories.Remove(ExtraGameCards.Lunar); });
        }

        public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data,
            HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            Unbound.Instance.ExecuteAfterFrames(25,
                () => { player.data.stats.GetAdditionalData().blacklistedCategories.Add(ExtraGameCards.Lunar); });
        }

        protected override string GetTitle()
        {
            return "Beads of Fealty";
        }

        protected override string GetDescription()
        {
            return "Seems to do nothing... <color=#c61a09>but...</color>";
        }

        protected override GameObject GetCardArt()
        {
            return Assets.BeadsArt;
        }

        protected override CardInfo.Rarity GetRarity()
        {
            return CardInfo.Rarity.Uncommon;
        }

        protected override CardInfoStat[] GetStats()
        {
            return new[]
            {
                new CardInfoStat
                {
                    positive = true,
                    stat = "Health",
                    amount = "+10%",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
                new CardInfoStat
                {
                    positive = true,
                    stat = "Regen",
                    amount = "+3",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                }
            };
        }

        protected override CardThemeColor.CardThemeColorType GetTheme()
        {
            return CardThemeColor.CardThemeColorType.ColdBlue;
        }

        public override string GetModName()
        {
            return ExtraGameCards.ModInitials;
        }
    }
}