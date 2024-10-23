using EGC.AssetsEmbedded;
using ModdingUtils.Extensions;
using RarityLib.Utils;
using UnboundLib.Cards;
using UnityEngine;

namespace EGC.Cards.Lunar
{
    internal class StoneFluxPauldron : CustomCard
    {
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            //UnityEngine.Debug.Log($"[{ExtraCards.ModInitials}][Card] {GetTitle()} has been setup.");
            cardInfo.allowMultiple = false;

            cardInfo.categories = new CardCategory[]
            {
                EGC.ExtraGameCards.Lunar
            };
            statModifiers.health = 2f;
            block.additionalBlocks = 1;
            statModifiers.movementSpeed = 0.7f;
            gun.attackSpeed = 1.3f;
        }
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            //UnityEngine.Debug.Log($"[{ExtraCards.ModInitials}][Card] {GetTitle()} has been added to player {player.playerID}.");

        }
        public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            //UnityEngine.Debug.Log($"[{ExtraCards.ModInitials}][Card] {GetTitle()} has been removed from player {player.playerID}.");
        }

        protected override string GetTitle()
        {
            return "Stone Flux Pauldron";
        }
        protected override string GetDescription()
        {
            return "You are more resilient... <color=#c61a09>but reduce your speed.</color>";
        }
        protected override GameObject GetCardArt()
        {
            return Assets.StoneFluxPauldronArt;
        }
        protected override CardInfo.Rarity GetRarity()
        {
            return RarityUtils.GetRarity("Lunar");
        }
        protected override CardInfoStat[] GetStats()
        {
            return new CardInfoStat[]
            {
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Health",
                    amount = "+100%",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Addtional Block",
                    amount = "+1",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
                new CardInfoStat()
                {
                    positive = false,
                    stat = "Speed",
                    amount = "-30%",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
                new CardInfoStat()
                {
                    positive = false,
                    stat = "Attack Speed",
                    amount = "-30%",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
            };
        }
        protected override CardThemeColor.CardThemeColorType GetTheme()
        {
            return CardThemeColor.CardThemeColorType.ColdBlue;
        }
        public override string GetModName()
        {
            return EGC.ExtraGameCards.ModInitials;
        }
    }
}