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
using ModsPlus;
using Photon.Pun;

namespace ExtraGameCards.Cards
{
    class Egocentrism : CustomEffectCard<EgoEffect>
    {
        public static CardInfo egocentrismCard;
        public override CardDetails Details => new CardDetails
        {
            Title = "Egocentrism",
            Description = "You are stronger <color=#c61a09> But every round, a random card is converted into this card</color>",
            ModName = EGC.ModInitials,
            Art = null,
            Rarity = CardInfo.Rarity.Uncommon,
            Theme = CardThemeColor.CardThemeColorType.ColdBlue,
            Stats = new CardInfoStat[]
            {
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Card (W.I.P)",
                    amount = "Upgradeable",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Damage",
                    amount = "+15%",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Attack Speed",
                    amount = "+10%",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
                new CardInfoStat()
                {
                    positive = false,
                    stat = "Reload time",
                    amount = "+0.15s",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
            }
        };

        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            cardInfo.allowMultiple = true;

            cardInfo.categories = new CardCategory[]
            {
                EGC.Lunar
            };

            gun.projectileColor = Color.cyan;
            gun.ammo = 1;
            gun.attackSpeed = 0.9f;
            gun.reloadTimeAdd = 0.15f;
            gun.damage = 1.15f;
        }

        protected override void Added(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            Extensions.CharacterStatModifiersExtension.GetAdditionalData(characterStats).egocentrismPower += 1;

            switch (Extensions.CharacterStatModifiersExtension.GetAdditionalData(characterStats).egocentrismPower)
            {
                case 2:
                    gun.numberOfProjectiles += 1;
                    break;
                case 3:
                    gun.bursts += 1;
                    gun.timeBetweenBullets += 0.25f;
                    break;
                case 4:
                    gun.ammo += 2;
                    gun.numberOfProjectiles += 1;
                    break;
                case 5:
                    gun.ammo += 2;
                    gun.ammoReg += 0.5f;
                    gun.attackSpeed -= 0.1f; 
                    gun.numberOfProjectiles += 1;
                    break;
                case 7:
                    gun.ammo += 3;
                    gun.projectileSpeed += 0.3f; 
                    gun.reflects += 1;
                    gun.attackSpeed -= 0.1f;
                    gun.numberOfProjectiles += 1;
                    break;
            }
        }
    }
    class EgoEffect : CardEffect
    {
        public override IEnumerator OnBattleStart(IGameModeHandler gameModeHandler)
        {
            UnityEngine.Debug.Log($"New Ego card !\nNumber of ego = {Extensions.CharacterStatModifiersExtension.GetAdditionalData(characterStats).egocentrismPower}");
            if (PhotonNetwork.OfflineMode)
            {
                RPCA_Replace(UnityEngine.Random.Range(1, 999999));
                yield return null;
            }
            else
            {
                this.gameObject.GetComponent<PhotonView>().RPC("RPCA_Replace", RpcTarget.All, new object[]
                {
                    UnityEngine.Random.Range(0, 999999)
                });

                yield return null;
            }
        }
        private IEnumerator Replace(int seed)
        {
            System.Random random = new System.Random(seed);
            List<CardInfo> playerCards = player.data.currentCards;
            var tries = 0;
            while (!(tries > 50))
            {
                tries++;
                int randomCardIdx = random.Next(0, playerCards.Count - 1);
                var oldCard = playerCards[randomCardIdx];
                if (!instance.CardIsNotBlacklisted(oldCard, new[] { CustomCardCategories.instance.CardCategory("CardManipulation"), CustomCardCategories.instance.CardCategory("NoRemove") })) { continue; }
                //if (!instance.PlayerIsAllowedCard(player, oldCard)) { continue; }
                UnityEngine.Debug.Log("Trying to remove : " + oldCard.cardName);
                yield return instance.RemoveCardFromPlayer(player, playerCards[randomCardIdx], SelectionType.Oldest);

                yield return new WaitForSeconds(0.4f);

                //CardInfo egoCard = instance.GetCardWithObjectName("Egocentrism");
                CardInfo egoCard = instance.GetCardWithObjectName(EgocentrismWIP.StaticCardEgo.name);
                UnityEngine.Debug.Log("Adding a copy of : " + egoCard.cardName);
                instance.AddCardToPlayer(player, egoCard, addToCardBar: true);

                instance.ReplaceCard(player, playerCards.IndexOf(oldCard), egoCard, "Eg", 2, 2, true);

                yield break;
            }
        }

        [PunRPC]
        private void RPCA_Replace(int seed)
        {
            StartCoroutine(Replace(seed));
        }
    }
}