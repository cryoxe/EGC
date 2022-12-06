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
            Art = Assets.MarioArt,
            Rarity = CardInfo.Rarity.Uncommon,
            Theme = CardThemeColor.CardThemeColorType.FirepowerYellow,
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
            AddPowerUp(player, characterStats);
        }
        protected override void Removed(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
        }

        private void AddPowerUp(Player player, CharacterStatModifiers characterStat)
        {
            CardInfo addedCard = getRandomPowerUp(characterStat);
            ModdingUtils.Utils.Cards.instance.AddCardToPlayer(player, addedCard, addToCardBar: true);
            ModdingUtils.Utils.CardBarUtils.instance.ShowAtEndOfPhase(player, addedCard);
        }

        private CardInfo getRandomPowerUp(CharacterStatModifiers characterStats)
        {
            int rng = Random.Range(0, 5);
            switch (rng)
            {
                case 0:
                    return ModdingUtils.Utils.Cards.instance.GetCardWithObjectName(SuperMushroom.superMushroomCard.name);

                case 1:
                    if (!Extensions.CharacterStatModifiersExtension.GetAdditionalData(characterStats).hasMiniMush)
                    {
                        Extensions.CharacterStatModifiersExtension.GetAdditionalData(characterStats).hasMiniMush = true;
                        return ModdingUtils.Utils.Cards.instance.GetCardWithObjectName(MiniMushroom.miniMushroomCard.name);
                    }
                    else { return ModdingUtils.Utils.Cards.instance.GetCardWithObjectName(SuperMushroom.superMushroomCard.name); }
                    
                case 2:
                    if (!Extensions.CharacterStatModifiersExtension.GetAdditionalData(characterStats).hasOneUpMush)
                    {
                        Extensions.CharacterStatModifiersExtension.GetAdditionalData(characterStats).hasOneUpMush = true;
                        return ModdingUtils.Utils.Cards.instance.GetCardWithObjectName(OneUpMushroom.oneUpMushroomCard.name);
                    }
                    else { return ModdingUtils.Utils.Cards.instance.GetCardWithObjectName(SuperMushroom.superMushroomCard.name); }

                case 3:
                    if (!Extensions.CharacterStatModifiersExtension.GetAdditionalData(characterStats).hasPoisonMush)
                    {
                        Extensions.CharacterStatModifiersExtension.GetAdditionalData(characterStats).hasPoisonMush = true;
                        return ModdingUtils.Utils.Cards.instance.GetCardWithObjectName(PoisonousMushroom.poisonousMushroomCard.name);
                    }
                    else { return ModdingUtils.Utils.Cards.instance.GetCardWithObjectName(SuperMushroom.superMushroomCard.name); }

                case 4:
                    if (!Extensions.CharacterStatModifiersExtension.GetAdditionalData(characterStats).hasBooMush)
                    {
                        Extensions.CharacterStatModifiersExtension.GetAdditionalData(characterStats).hasBooMush = true;
                        return ModdingUtils.Utils.Cards.instance.GetCardWithObjectName(BooMushroom.booMushroomCard.name);
                    }
                    else { return ModdingUtils.Utils.Cards.instance.GetCardWithObjectName(SuperMushroom.superMushroomCard.name); }

                default:
                    return null;
            }
        }
    }
}