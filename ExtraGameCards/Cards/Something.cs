using System;
using System.Linq;
using EGC.AssetsEmbedded;
using EGC.MonoBehaviours;
using UnboundLib;
using UnboundLib.Cards;
using UnboundLib.Utils;
using UnityEngine;
using UnityEngine.UI.ProceduralImage;

namespace EGC.Cards
{
    public class Something : CustomCard
    {
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            //UnityEngine.Debug.Log($"[{ExtraCards.ModInitials}][Card] {GetTitle()} has been setup.");
            cardInfo.allowMultiple = false;

            statModifiers.movementSpeed = 1.2f;

        }
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            //UnityEngine.Debug.Log($"[{ExtraCards.ModInitials}][Card] {GetTitle()} has been added to player {player.playerID}.");

            Player[] players = PlayerManager.instance.players.ToArray();
            foreach (Player otherPlayer in PlayerManager.instance.players)
            {
                if (otherPlayer == player) { continue; }

                //Will's code and HDC's code    XD
                var abyssalCard = CardManager.cards.Values.Select(card => card.cardInfo).First(c => c.name.Equals("AbyssalCountdown"));
                var statMods = abyssalCard.gameObject.GetComponentInChildren<CharacterStatModifiers>();
                var abyssalObj = statMods.AddObjectToPlayer;

                var thingObj = Instantiate(abyssalObj, otherPlayer.transform);
                thingObj.name = "A_DeathAura";
                thingObj.transform.localPosition = Vector3.zero;

                var abyssal = thingObj.GetComponent<AbyssalCountdown>();

                var somethingCountdown = thingObj.GetOrAddComponent<SomethingMono>();
                somethingCountdown.numberOfSomething += 1;
                somethingCountdown.player = otherPlayer;
                somethingCountdown.soundUpgradeChargeLoop = abyssal.soundAbyssalChargeLoop;
                somethingCountdown.counter = 0;
                somethingCountdown.timeToFill = 8f;
                somethingCountdown.outerRing = abyssal.outerRing;
                somethingCountdown.fill = abyssal.fill;
                somethingCountdown.rotator = abyssal.rotator;
                somethingCountdown.still = abyssal.still;
                somethingCountdown.player = otherPlayer;
                somethingCountdown.duration = 3.4f;
                somethingCountdown.defaultRLTime = gun.reloadTime;
                somethingCountdown.characterStats = characterStats;

                EGC.ExtraGameCards.Instance.ExecuteAfterFrames(5, () =>
                {
                    try
                    {
                        UnityEngine.GameObject.Destroy(abyssal);

                        var COs = thingObj.GetComponentsInChildren<Transform>().Where(child => child.parent == thingObj.transform).Select(child => child.gameObject).ToArray();

                        foreach (var CO in COs)
                        {
                            if (CO.transform.gameObject != thingObj.transform.Find("Canvas").gameObject)
                            {
                                UnityEngine.GameObject.Destroy(CO);
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        UnityEngine.Debug.Log("First Catch");
                        UnityEngine.Debug.LogException(e);
                    }
                    try
                    {
                        somethingCountdown.outerRing.color = new Color32(230, 0, 0, 255);
                        somethingCountdown.fill.color = new Color32(0, 0, 0, 90);
                        somethingCountdown.rotator.gameObject.GetComponentInChildren<ProceduralImage>().color = somethingCountdown.outerRing.color;
                        somethingCountdown.still.gameObject.GetComponentInChildren<ProceduralImage>().color = somethingCountdown.outerRing.color;
                        thingObj.transform.Find("Canvas/Size/BackRing").GetComponent<ProceduralImage>().color = new Color32(26, 26, 26, 100);
                    }
                    catch (Exception e)
                    {
                        UnityEngine.Debug.Log("Last Catch");
                        UnityEngine.Debug.LogException(e);
                    }
                });
                //UnityEngine.Debug.Log("Something Aura added");


            }
            

        }
        public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            //UnityEngine.Debug.Log($"[{ExtraCards.ModInitials}][Card] {GetTitle()} has been removed from player {player.playerID}.");
            Player[] players = PlayerManager.instance.players.ToArray();
            foreach (Player otherPlayer in PlayerManager.instance.players)
            {
                if(otherPlayer == player) { continue; }

                SomethingMono mb = otherPlayer.gameObject.GetComponent<SomethingMono>();
                if(mb.numberOfSomething <= 1)
                {
                    //UnityEngine.Debug.Log("SomethingRemoved");
                    Destroy(mb);
                }


            }
        }

        protected override string GetTitle()
        {
            return "Something?";
        }
        protected override string GetDescription()
        {
            return "It follows...";
        }
        protected override GameObject GetCardArt()
        {
            return Assets.SomethingArt;
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
                    stat = "Speed",
                    amount = "+20%",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Death Sentence",
                    amount = "8s",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                }
            };
        }
        protected override CardThemeColor.CardThemeColorType GetTheme()
        {
            return CardThemeColor.CardThemeColorType.TechWhite;
        }
        public override string GetModName()
        {
            return EGC.ExtraGameCards.ModInitials;
        }

    }
}