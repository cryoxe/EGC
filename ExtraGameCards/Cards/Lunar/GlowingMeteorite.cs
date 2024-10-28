using EGC.AssetsEmbedded;
using EGC.MonoBehaviours;
using RarityLib.Utils;
using UnboundLib;
using UnboundLib.Cards;
using UnityEngine;

namespace EGC.Cards.Lunar
{
    internal class GlowingMeteorite : CustomCard
    {
        public static CardInfo GlowingMeteoriteCard = null!;

        public override bool GetEnabled() => false;

        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats,
            CharacterStatModifiers statModifiers, Block block)
        {
            //UnityEngine.Debug.Log($"[{ExtraCards.ModInitials}][Card] {GetTitle()} has been setup.");
            cardInfo.allowMultiple = false;

            cardInfo.categories = new[]
            {
                ExtraGameCards.Lunar
            };
            statModifiers.health = 1.4f;
            block.cdAdd = 0.25f;
        }

        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data,
            HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            //UnityEngine.Debug.Log($"[{ExtraCards.ModInitials}][Card] {GetTitle()} has been added to player {player.playerID}.");
            var mb = player.gameObject.GetOrAddComponent<GlowingMeteoriteMono>();
            mb.player = player;
            mb.gun = gun;
            mb.block = block;
            mb.health = health;
            mb.stats = characterStats;
            mb.gunAmmo = gunAmmo;
            mb.data = data;
            mb.gravity = gravity;
        }

        public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data,
            HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            //UnityEngine.Debug.Log($"[{ExtraCards.ModInitials}][Card] {GetTitle()} has been removed from player {player.playerID}.");
            var mb = player.gameObject.GetOrAddComponent<GlowingMeteoriteMono>();
            Destroy(mb);
        }

        protected override string GetTitle()
        {
            return "Glowing Meteorite";
        }

        protected override string GetDescription()
        {
            return "Blocking create a meteorite shower... <color=#c61a09>but you take damage as well...</color>";
        }

        protected override GameObject GetCardArt()
        {
            return Assets.GlowingArt;
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
                    stat = "Health",
                    amount = "+40%",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
                new CardInfoStat
                {
                    positive = false,
                    stat = "Meteor Cooldown",
                    amount = "10s",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
                new CardInfoStat
                {
                    positive = false,
                    stat = "Block Cooldown",
                    amount = "+0.25s",
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