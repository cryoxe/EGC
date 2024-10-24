using System.Collections;
using System.Collections.Generic;
using System.Linq;
using EGC.AssetsEmbedded;
using ModdingUtils.Extensions;
using UnboundLib.Cards;
using UnboundLib.GameModes;
using UnityEngine;

namespace EGC.Cards
{
    internal class PortraitOfMarkov : CustomCard
    {
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats,
            CharacterStatModifiers statModifiers, Block block)
        {
            cardInfo.allowMultiple = false;

            gun.projectileSpeed = 1.40f;
            gun.reloadTimeAdd = -0.25f;
            gun.gravity = 0.6f;
        }

        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data,
            HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            gun.spread *= 0.7f;
            characterStats.lifeSteal = (characterStats.lifeSteal != 0f)
                ? (characterStats.lifeSteal * 2)
                : (characterStats.lifeSteal + 1f);
            Extensions.CharacterStatModifiersExtension.GetAdditionalData(characterStats).markovChoice += 1;
        }

        public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data,
            HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
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
            return new[]
            {
                new CardInfoStat
                {
                    positive = true,
                    stat = "Life Steal",
                    amount = "+100%",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },

                new CardInfoStat
                {
                    positive = true,
                    stat = "Spread",
                    amount = "-30%",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },

                new CardInfoStat
                {
                    positive = true,
                    stat = "Bullet Speed",
                    amount = "+40%",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },

                new CardInfoStat
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
            return ExtraGameCards.ModInitials;
        }

        internal static IEnumerator MarkovPick()
        {
            List<Player> players = PlayerManager.instance.players;

            foreach (Player player in PlayerManager.instance.players)
            {
                while (Extensions.CharacterStatModifiersExtension.GetAdditionalData(player.data.stats).markovChoice > 0)
                {
                    Extensions.CharacterStatModifiersExtension.GetAdditionalData(player.data.stats).markovChoice -= 1;

                    yield return GameModeManager.TriggerHook(GameModeHooks.HookPlayerPickStart);

                    player.data.stats.GetAdditionalData().blacklistedCategories.Add(ExtraGameCards.Normal);
                    player.data.stats.GetAdditionalData().blacklistedCategories.Add(ExtraGameCards.Lunar);
                    player.data.stats.GetAdditionalData().blacklistedCategories.Remove(ExtraGameCards.Markov);

                    CardChoiceVisuals.instance.Show(
                        Enumerable.Range(0, players.Count).First(i =>
                            players[i].playerID == player.playerID), true);

                    yield return CardChoice.instance.DoPick(1, player.playerID, PickerType.Player);
                    yield return new WaitForSecondsRealtime(0.1f);

                    yield return GameModeManager.TriggerHook(GameModeHooks.HookPlayerPickEnd);

                    player.data.stats.GetAdditionalData().blacklistedCategories.Add(ExtraGameCards.Markov);
                    player.data.stats.GetAdditionalData().blacklistedCategories.Remove(ExtraGameCards.Normal);
                    player.data.stats.GetAdditionalData().blacklistedCategories.Remove(ExtraGameCards.Lunar);

                    yield return new WaitForSecondsRealtime(0.1f);
                }
            }

            yield break;
        }
    }
}