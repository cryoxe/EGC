using EGC.AssetsEmbedded;
using EGC.MonoBehaviours;
using RarityLib.Utils;
using UnboundLib;
using UnboundLib.Cards;
using UnityEngine;

namespace EGC.Cards.Lunar
{
    internal class GestureOfTheDrowned : CustomCard
    {
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats,
            CharacterStatModifiers statModifiers, Block block)
        {
            //UnityEngine.Debug.Log($"[{ExtraCards.ModInitials}][Card] {GetTitle()} has been setup.");
            cardInfo.allowMultiple = false;

            cardInfo.categories = new[]
            {
                ExtraGameCards.Lunar
            };

            block.cdMultiplier = 0.45f;
            statModifiers.health = 1.3f;
        }

        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data,
            HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            //UnityEngine.Debug.Log($"[{ExtraCards.ModInitials}][Card] {GetTitle()} has been added to player {player.playerID}.");
            var mb = player.gameObject.GetOrAddComponent<GestureOfTheDrownedMono>();
            mb.block = block;
            mb.data = data;
        }

        public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data,
            HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            //UnityEngine.Debug.Log($"[{ExtraCards.ModInitials}][Card] {GetTitle()} has been removed from player {player.playerID}.");
            var mb = player.gameObject.GetOrAddComponent<GestureOfTheDrownedMono>();
            Destroy(mb);
        }

        protected override string GetTitle()
        {
            return "Gesture of the Drowned";
        }

        protected override string GetDescription()
        {
            return "You block more often... <color=#c61a09>but use your block whenever it's ready.</color>";
        }

        protected override GameObject GetCardArt()
        {
            return Assets.GestureArt;
        }

        protected override CardInfo.Rarity GetRarity()
        {
            return RarityUtils.GetRarity("Lunar");
        }

        protected override CardInfoStat[] GetStats()
        {
            return new[]
            {
                new CardInfoStat
                {
                    positive = true,
                    stat = "Block Cooldown",
                    amount = "-55%",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
                new CardInfoStat
                {
                    positive = true,
                    stat = "Health",
                    amount = "+30%",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
                new CardInfoStat
                {
                    positive = false,
                    stat = "Block",
                    amount = "Auto",
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
            return ExtraGameCards.ModInitials;
        }
    }
}