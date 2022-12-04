using ExtraGameCards.AssetsEmbedded;
using ExtraGameCards.MonoBehaviours;
using ModdingUtils.Extensions;
using ModsPlus;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ExtraGameCards.Cards
{
    class OneUpMushroom : SimpleCard
    {
        public static CardInfo oneUpMushroomCard;
        public override CardDetails Details => new CardDetails
        {
            Title = "1Up Mushroom",
            Description = "You're happy to see it!",
            ModName = EGC.ModInitials,
            Art = null,
            Rarity = CardInfo.Rarity.Rare,
            Theme = CardThemeColor.CardThemeColorType.PoisonGreen,
            Stats = new CardInfoStat[]
            {
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Life",
                    amount = "+1",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
            }
        };

        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            cardInfo.allowMultiple = true;

            cardInfo.categories = new CardCategory[]
            {
                EGC.MarioPowerUps
            };

            //UnityEngine.Debug.Log($"[{ExtraCards.ModInitials}][Card] {GetTitle()} has been setup.");
            statModifiers.respawns = 1;
        }
    }
}