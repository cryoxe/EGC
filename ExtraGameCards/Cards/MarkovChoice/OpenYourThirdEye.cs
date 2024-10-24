﻿using UnboundLib.Cards;
using UnityEngine;

namespace EGC.Cards.MarkovChoice
{
    internal class OpenYourThirdEye : CustomCard
    {
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats,
            CharacterStatModifiers statModifiers, Block block)
        {
            //UnityEngine.Debug.Log($"[{ExtraCards.ModInitials}][Card] {GetTitle()} has been setup.");
            cardInfo.categories = new[]
            {
                ExtraGameCards.Markov
            };

            statModifiers.health = 0.6f;
            gun.damage = 1.3f;
            gun.ammo = 1;
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
            return "Open Your Third Eye";
        }

        protected override string GetDescription()
        {
            return "And finally become complete.";
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