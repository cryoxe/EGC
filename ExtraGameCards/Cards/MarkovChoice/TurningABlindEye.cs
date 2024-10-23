using UnboundLib.Cards;
using UnityEngine;

namespace EGC.Cards.MarkovChoice
{
    internal class TurningABlindEye : CustomCard
    {
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats,
            CharacterStatModifiers statModifiers, Block block)
        {
            //UnityEngine.Debug.Log($"[{ExtraCards.ModInitials}][Card] {GetTitle()} has been setup.");
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
            HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            //UnityEngine.Debug.Log($"[{ExtraCards.ModInitials}][Card] {GetTitle()} has been added to player {player.playerID}.");
        }

        public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data,
            HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            //UnityEngine.Debug.Log($"[{ExtraCards.ModInitials}][Card] {GetTitle()} has been removed from player {player.playerID}.");
        }

        protected override string GetTitle()
        {
            return "Turning a blind eye";
        }

        protected override string GetDescription()
        {
            return "You refuse to believe what you read.";
        }

        protected override GameObject GetCardArt()
        {
            return null;
        }

        protected override CardInfo.Rarity GetRarity()
        {
            return CardInfo.Rarity.Common;
        }

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