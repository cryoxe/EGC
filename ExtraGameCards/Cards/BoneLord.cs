using System.Collections;
using System.Collections.Generic;
using CardChoiceSpawnUniqueCardPatch.CustomCategories;
using ModdingUtils.Extensions;
using UnboundLib.Cards;
using UnityEngine;
using static ModdingUtils.Utils.Cards;

namespace EGC.Cards
{
    public class BoneLord : CustomCard
    {
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            //UnityEngine.Debug.Log($"[{ExtraCards.ModInitials}][Card] {GetTitle()} has been setup.");
            cardInfo.allowMultiple = false;

            statModifiers.health = 1.4f;
            gun.damage = 1.4f;
            gun.knockback = 1.6f;
            gun.projectileSpeed = 1.4f;

        }
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            //UnityEngine.Debug.Log($"[{ExtraCards.ModInitials}][Card] {GetTitle()} has been added to player {player.playerID}.");

            //May remove more cards in the future and give curses according to the number of cards that could not be removed
            EGC.ExtraGameCards.Instance.StartCoroutine(RemoveRandomCardOfPlayer(player));

        }
        public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            //UnityEngine.Debug.Log($"[{ExtraCards.ModInitials}][Card] {GetTitle()} has been removed from player {player.playerID}.");

        }

        protected override string GetTitle()
        {
            return "Boon Of The Bone Lord";
        }
        protected override string GetDescription()
        {
            return "<color=#DC0027>It requires a sacrifice.</color>";
        }
        protected override GameObject GetCardArt()
        {
            return null;
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
                    stat = "Health",
                    amount = "+40%",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Knockback",
                    amount = "+60%",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Damage",
                    amount = "+40%",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Bullet Speed",
                    amount = "+40%",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
            };
        }
        protected override CardThemeColor.CardThemeColorType GetTheme()
        {
            return CardThemeColor.CardThemeColorType.EvilPurple;
        }
        public override string GetModName()
        {
            return EGC.ExtraGameCards.ModInitials;
        }

        private IEnumerator RemoveRandomCardOfPlayer(Player player, int numberOfCardToRemove = 1)
        {
            yield return new WaitForSeconds(0.5f);
            System.Random random = new System.Random();

            for (int i = 0; i < numberOfCardToRemove; i++)
            {
                List<CardInfo> playerCards = player.data.currentCards;
                UnityEngine.Debug.Log("there is " + playerCards.Count + " cards on this player");
                if (player.data.currentCards.Count-1 <= 0)
                {
                    yield return null;
                }
                var tries = 0;
                while (!(tries > 50))
                {
                    tries++;
                    int randomCardIdx = random.Next(0, playerCards.Count - 1);
                    var card = playerCards[randomCardIdx];
                    if (!instance.CardIsNotBlacklisted(card, new[] { CustomCardCategories.instance.CardCategory("CardManipulation"), CustomCardCategories.instance.CardCategory("NoRemove") })) { continue; }
                    if (!instance.PlayerIsAllowedCard(player, card)) { continue; }
                    UnityEngine.Debug.Log("Trying to remove : " + card.cardName);
                    yield return instance.RemoveCardFromPlayer(player, playerCards[randomCardIdx], SelectionType.Oldest);
                    UnityEngine.Debug.Log("Success!");
                    break;
                }

            }
        }
    }
}