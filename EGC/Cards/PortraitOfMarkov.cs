using ExtraGameCards.AssetsEmbedded;
using ModdingUtils.Extensions;
using System.Collections;
using System.Linq;
using UnboundLib.Cards;
using UnboundLib.GameModes;
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
            Extensions.CharacterStatModifiersExtension.GetAdditionalData(characterStats).markovChoice += 1;
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

        internal static IEnumerator MarkovPick()
        {
            foreach (Player player in PlayerManager.instance.players.ToArray())
            {
                while (Extensions.CharacterStatModifiersExtension.GetAdditionalData(player.data.stats).markovChoice > 0)
                {
                    Extensions.CharacterStatModifiersExtension.GetAdditionalData(player.data.stats).markovChoice -= 1;

                    yield return GameModeManager.TriggerHook(GameModeHooks.HookPlayerPickStart);

                    player.data.stats.GetAdditionalData().blacklistedCategories.Add(EGC.Normal);
                    player.data.stats.GetAdditionalData().blacklistedCategories.Add(EGC.Lunar);
                    player.data.stats.GetAdditionalData().blacklistedCategories.Remove(EGC.Markov);

                    CardChoiceVisuals.instance.Show(Enumerable.Range(0, PlayerManager.instance.players.Count).Where(i => PlayerManager.instance.players[i].playerID == player.playerID).First(), true);
                    yield return CardChoice.instance.DoPick(1, player.playerID, PickerType.Player);
                    yield return new WaitForSecondsRealtime(0.1f);

                    yield return GameModeManager.TriggerHook(GameModeHooks.HookPlayerPickEnd);

                    player.data.stats.GetAdditionalData().blacklistedCategories.Add(EGC.Markov);
                    player.data.stats.GetAdditionalData().blacklistedCategories.Remove(EGC.Normal);
                    player.data.stats.GetAdditionalData().blacklistedCategories.Remove(EGC.Lunar);

                    yield return new WaitForSecondsRealtime(0.1f);
                }
            }
            yield break;
        }

    }

}
