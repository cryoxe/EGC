using ExtraGameCards.AssetsEmbedded;
using ExtraGameCards.MonoBehaviours;
using ModdingUtils.Extensions;
using ModsPlus;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ExtraGameCards.Cards
{
    class MarioBlock : SimpleCard
    {
        public override CardDetails Details => new CardDetails
        {
            Title = "? Block",
            Description = "Power-Ups Await",
            ModName = EGC.ModInitials,
            Art = null,
            Rarity = CardInfo.Rarity.Uncommon,
            Theme = CardThemeColor.CardThemeColorType.NatureBrown,
            Stats = new CardInfoStat[]
            {
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Random Power-Up",
                    amount = "+1",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
            }
        };

        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            //UnityEngine.Debug.Log($"[{ExtraCards.ModInitials}][Card] {GetTitle()} has been setup.");
            
        }
        protected override void Added(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            AddPowerUp(player);
        }
        protected override void Removed(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
        }

        private void AddPowerUp(Player player)
        {
            CardInfo addedCard = getRandomPowerUp();
            ModdingUtils.Utils.Cards.instance.AddCardToPlayer(player, addedCard, addToCardBar: true);
            ModdingUtils.Utils.CardBarUtils.instance.ShowAtEndOfPhase(player, addedCard);
        }

        private CardInfo getRandomPowerUp()
        {
            int rng = Random.Range(0, 5);
            switch (rng)
            {
                case 0:
                    return ModdingUtils.Utils.Cards.instance.GetCardWithObjectName(SuperMushroom.superMushroomCard.name);
                case 1:
                    return ModdingUtils.Utils.Cards.instance.GetCardWithObjectName(MiniMushroom.miniMushroomCard.name);
                case 2:
                    return ModdingUtils.Utils.Cards.instance.GetCardWithObjectName(OneUpMushroom.oneUpMushroomCard.name);
                case 3:
                    return ModdingUtils.Utils.Cards.instance.GetCardWithObjectName(PoisonousMushroom.poisonousMushroomCard.name);
                case 4:
                    return ModdingUtils.Utils.Cards.instance.GetCardWithObjectName(BooMushroom.booMushroomCard.name);
                default:
                    return null;
            }
        }
    }
}