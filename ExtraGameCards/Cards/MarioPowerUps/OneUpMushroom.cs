using EGC.AssetsEmbedded;
using ModsPlus;

namespace EGC.Cards.MarioPowerUps
{
    internal class OneUpMushroom : SimpleCard
    {
        public static CardInfo oneUpMushroomCard;
        public override CardDetails Details => new CardDetails
        {
            Title = "1Up Mushroom",
            Description = "You're happy to see it!",
            ModName = EGC.ExtraGameCards.ModInitials,
            Art = Assets.OneUpMushArt,
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
            cardInfo.allowMultiple = false;

            cardInfo.categories = new CardCategory[]
            {
                EGC.ExtraGameCards.MarioPowerUps
            };

            //UnityEngine.Debug.Log($"[{ExtraCards.ModInitials}][Card] {GetTitle()} has been setup.");
            statModifiers.respawns = 1;
        }
    }
}