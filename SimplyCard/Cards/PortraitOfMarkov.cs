using ExtraGameCards;
using ExtraGameCards.AssetsEmbedded;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnboundLib;
using UnboundLib.GameModes;
using UnboundLib.Cards;
using ModdingUtils.Extensions;
using UnityEngine;

namespace ExtraGameCards.Cards
{
    class PortraitOfMarkov : CustomCard
    {
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            //UnityEngine.Debug.Log($"[{ExtraCards.ModInitials}][Card] {GetTitle()} has been setup.");
            CardInfoExtension.GetAdditionalData(cardInfo).canBeReassigned = false;
            cardInfo.allowMultiple = false;

            gun.projectileSpeed = 1.40f;
            gun.reloadTimeAdd = -0.25f;
            gun.gravity = 0.6f;
        }
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            //UnityEngine.Debug.Log($"[{ExtraCards.ModInitials}][Card] {GetTitle()} has been added to player {player.playerID}.");
            gun.spread *= 0.7f;
            characterStats.lifeSteal = (characterStats.lifeSteal != 0f) ? (characterStats.lifeSteal * 2) : (characterStats.lifeSteal + 1f);

            GameModeManager.AddOnceHook(GameModeHooks.HookPlayerPickEnd, (gm) => { EGC.Instance.StartCoroutine(TreasurePick(player)); return new List<object>().GetEnumerator();});
        }
        
        public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            //UnityEngine.Debug.Log($"[{ExtraCards.ModInitials}][Card] {GetTitle()} has been removed from player {player.playerID}.");
        }

        protected override string GetTitle()
        {
            return "Portrait of Markov";
        }
        protected override string GetDescription()
        {
            return "Basically, it's about this <b>[REDACTED]</b>...";
        }
        protected override GameObject GetCardArt()
        {
            return Assets.PortraitOfMarkovArt;
        }
        protected override CardInfo.Rarity GetRarity()
        {
            return CardInfo.Rarity.Rare;
        }
        protected override CardInfoStat[] GetStats()
        {
            return new CardInfoStat[]
            {
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Life Steal",
                    amount = "+100%",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },

                new CardInfoStat()
                {
                    positive = true,
                    stat = "Spread",
                    amount = "-30%",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },

                new CardInfoStat()
                {
                    positive = true,
                    stat = "Bullet Speed",
                    amount = "+40%",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },

                new CardInfoStat()
                {
                    positive = true,
                    stat = "Reload Speed",
                    amount = "-0.25s",
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
            return EGC.ModInitials;
        }

        public static IEnumerator TreasurePick(Player player)
        {
            while (!CardChoice.instance.IsPicking) yield return null;
            if (CardChoice.instance.pickrID == player.playerID)
            {
                player.data.stats.GetAdditionalData().blacklistedCategories.Add(EGC.Normal);
                player.data.stats.GetAdditionalData().blacklistedCategories.Add(EGC.Lunar);
                player.data.stats.GetAdditionalData().blacklistedCategories.Remove(EGC.Markov);
                GameModeManager.AddOnceHook(GameModeHooks.HookPlayerPickEnd, (gm) => EndTreasurePick(player));
            }
            else
                GameModeManager.AddOnceHook(GameModeHooks.HookPlayerPickStart, (gm) => { EGC.Instance.StartCoroutine(TreasurePick(player)); return new List<object>().GetEnumerator(); });
            yield break;
        }

        public static IEnumerator EndTreasurePick(Player player)
        {
            player.data.stats.GetAdditionalData().blacklistedCategories.Add(EGC.Markov);
            player.data.stats.GetAdditionalData().blacklistedCategories.Remove(EGC.Normal);
            player.data.stats.GetAdditionalData().blacklistedCategories.Remove(EGC.Lunar);
            //CardChoiceVisuals.instance.Show(Enumerable.Range(0, PlayerManager.instance.players.Count).Where(i => PlayerManager.instance.players[i].playerID == player.playerID).First(), true);
            //yield return CardChoice.instance.DoPick(1, player.playerID, PickerType.Player);
            yield break;
        }
    }

}
