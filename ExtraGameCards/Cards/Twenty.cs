﻿using EGC.AssetsEmbedded;
using UnboundLib.Cards;
using UnityEngine;

namespace EGC.Cards
{
    internal class Twenty : CustomCard
    {
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats,
            CharacterStatModifiers statModifiers, Block block)
        {
            cardInfo.allowMultiple = false;

            gun.damage = 0.75f;
            gun.numberOfProjectiles = 1;
            gun.spread = 0.04f;
        }

        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data,
            HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            gunAmmo.maxAmmo *= 2;
            player.RPCA_SetFace(46,
                new Vector2(0.0f, 0.0f),
                0,
                new Vector2(0, 0),
                0,
                new Vector2(0, 0),
                0,
                new Vector2(0, 0));
        }

        protected override string GetTitle()
        {
            return "20/20";
        }

        protected override string GetDescription()
        {
            return "I can see and thus shoot better !";
        }

        protected override GameObject GetCardArt()
        {
            return Assets.TwentyArt;
        }

        protected override CardInfo.Rarity GetRarity()
        {
            return CardInfo.Rarity.Rare;
        }

        protected override CardInfoStat[] GetStats()
        {
            return new[]
            {
                new CardInfoStat
                {
                    positive = true,
                    stat = "Bullet",
                    amount = "Double",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
                new CardInfoStat
                {
                    positive = true,
                    stat = "Max Ammo",
                    amount = "X2",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
                new CardInfoStat
                {
                    positive = false,
                    stat = "Damage",
                    amount = "-25%",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                }
            };
        }

        protected override CardThemeColor.CardThemeColorType GetTheme()
        {
            return CardThemeColor.CardThemeColorType.FirepowerYellow;
        }

        public override string GetModName()
        {
            return ExtraGameCards.ModInitials;
        }
    }
}