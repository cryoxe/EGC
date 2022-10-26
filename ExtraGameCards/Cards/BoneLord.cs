using ExtraGameCards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnboundLib;
using UnboundLib.Cards;
using UnityEngine;

namespace SimplyCard.Cards
{
    class BoneLord : CustomCard
    {
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            //Edits values on card itself, which are then applied to the player in `ApplyCardStats`
            //UnityEngine.Debug.Log($"[{ExtraCards.ModInitials}][Card] {GetTitle()} has been setup.");
            statModifiers.health = 1.6f;
            gun.damage = 1.4f;
            gun.attackSpeed = 1.4f;
            gun.reloadTime = 0.7f;
            gun.knockback = 1.4f;
            gun.projectileSpeed = 1.3f;

        }
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            //Edits values on player when card is selected
            //UnityEngine.Debug.Log($"[{ExtraCards.ModInitials}][Card] {GetTitle()} has been added to player {player.playerID}.");
            var random = new System.Random();
            UnityEngine.Debug.Log(player.data.currentCards);
            int cardListCount = player.data.currentCards.Count();
            UnityEngine.Debug.Log(cardListCount);
            if (cardListCount <= 2) { return; }
            else
            {
                int randomCard = random.Next(0, cardListCount-1);
                ModdingUtils.Utils.Cards.instance.RemoveCardFromPlayer(player, randomCard);
                randomCard = random.Next(0, cardListCount-1);
                ModdingUtils.Utils.Cards.instance.RemoveCardFromPlayer(player, randomCard);
            }
        }
        public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            //Run when the card is removed from the player
            //UnityEngine.Debug.Log($"[{ExtraCards.ModInitials}][Card] {GetTitle()} has been removed from player {player.playerID}.");
        }

        protected override string GetTitle()
        {
            return "Boon Of The Bone Lord";
        }
        protected override string GetDescription()
        {
            return "It requires a sacrifice.";
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
                    positive = false,
                    stat = "Sacrifice",
                    amount = "2",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
                new CardInfoStat()
                {
                    positive = true,
                    stat = "All",
                    amount = "+35%",
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
    }
}