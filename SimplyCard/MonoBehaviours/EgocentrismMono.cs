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

namespace ExtraGameCards.MonoBehaviours
{
    internal class EgocentrismMono : MonoBehaviour
    {
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

        public static IEnumerator NewEgocentrismCard(Player player)
        {
            UnityEngine.Debug.Log("Trying to create another Egocentrism");
            System.Random random = new System.Random();

            List<CardInfo> playerCards = player.data.currentCards;
            UnityEngine.Debug.Log("there is " + playerCards.Count + " cards on this player");
            if (player.data.currentCards.Count - 1 <= 0)
            {
                UnityEngine.Debug.Log("not enought cards !");
                yield return null;
            }
            var tries = 0;
            while (!(tries > 50))
            {
                tries++;
                int randomCardIdx = random.Next(0, playerCards.Count - 1);
                var oldCard = playerCards[randomCardIdx];
                if (!instance.CardIsNotBlacklisted(oldCard, new[] { CustomCardCategories.instance.CardCategory("CardManipulation"), CustomCardCategories.instance.CardCategory("NoRemove") })) { continue; }
                if (!instance.PlayerIsAllowedCard(player, oldCard)) { continue; }

                UnityEngine.Debug.Log("Trying to replace : " + oldCard.cardName);

                CardInfo newEgocentrism = instance.GetCardWithObjectName("Egocentrism");
                UnityEngine.Debug.Log(newEgocentrism.cardName);

                yield return instance.ReplaceCard(player, playerCards.IndexOf(oldCard), newEgocentrism, "EG", 2f, 2f, true);
                UnityEngine.Debug.Log("Success !");
                yield break;
            }
        }
    }
}
