using ExtraGameCards;
using UnityEngine;
using ModdingUtils.MonoBehaviours;
using UnboundLib.Cards;
using UnboundLib.GameModes;
using UnboundLib;
using ModdingUtils.Extensions;
using static ModdingUtils.Utils.Cards;
using CardChoiceSpawnUniqueCardPatch.CustomCategories;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using ExtraGameCards.Cards;

namespace ExtraGameCards.MonoBehaviours
{
    internal class EgocentrismMono : MonoBehaviour
    {
        //public CardInfo egocentrism;
        public Player player;
        public void Start()
        {
            UnityEngine.Debug.Log("INIT Egocentrism Mono");
            player = gameObject.GetComponent<Player>();
            GameModeManager.AddHook(GameModeHooks.HookRoundStart, (gm) =>
            {
                Unbound.Instance.StartCoroutine(NewEgocentrismCard(player));
                return new List<object>().GetEnumerator();
            });
        }

        public void OnOnDestroy()
        {
            GameModeManager.RemoveHook(GameModeHooks.HookRoundStart, (gm) =>
            {
                return new List<object>().GetEnumerator();
            });
        }

        public static IEnumerator NewEgocentrismCard(Player player)
        {
            bool cardFound = false;
            UnityEngine.Debug.Log("Trying to create another Egocentrism");
            System.Random random = new System.Random();

            List<CardInfo> playerCards = player.data.currentCards;
            UnityEngine.Debug.Log("there is " + playerCards.Count + " cards on this player");
            if (player.data.currentCards.Count - 1 <= 0)
            {
                UnityEngine.Debug.Log("not enough cards !");
                yield return null;
            }
            var tries = 0;
            while (!(tries > 75))
            {
                tries++; 
                int randomCardIdx = random.Next(0, playerCards.Count - 1);
                var oldCard = playerCards[randomCardIdx];
                if (!instance.CardIsNotBlacklisted(oldCard, new[] { CustomCardCategories.instance.CardCategory("CardManipulation"), CustomCardCategories.instance.CardCategory("NoRemove"), CustomCardCategories.instance.CardCategory("Lunar") })) { continue; }
                //if (!instance.PlayerIsAllowedCard(player, oldCard)) { continue; }
                UnityEngine.Debug.Log("Trying to remove : " + oldCard.cardName);
                yield return instance.RemoveCardFromPlayer(player, playerCards[randomCardIdx], SelectionType.Oldest);

                yield return new WaitForSeconds(0.4f);

                //CardInfo egoCard = instance.GetCardWithObjectName("Egocentrism");
                CardInfo egoCard = instance.GetCardWithObjectName(Egocentrism.StaticCardEgo.name);
                UnityEngine.Debug.Log("Adding a copy of : " + egoCard.cardName);
                instance.AddCardToPlayer(player, egoCard, addToCardBar: true);

                instance.ReplaceCard(player, playerCards.IndexOf(oldCard), egoCard, "", 2, 2, true);

                cardFound = true;
                yield break;
            }
            if (!cardFound) { UnityEngine.Debug.Log("No card found to be removed"); }
        }
    }
}
