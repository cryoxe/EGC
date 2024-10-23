using EGC.AssetsEmbedded;
using ModsPlus;

namespace EGC.Cards.MarioPowerUps
{
    internal class OneUpMushroom : SimpleCard
    {
        public static CardInfo OneUpMushroomCard = null!;

        public override CardDetails Details => new CardDetails
        {
            Title = "1Up Mushroom",
            Description = "You're happy to see it!",
            ModName = ExtraGameCards.ModInitials,
            Art = Assets.OneUpMushArt,
            Rarity = CardInfo.Rarity.Rare,
            Theme = CardThemeColor.CardThemeColorType.PoisonGreen,
            Stats = new[]
            {
                new CardInfoStat
                {
                    positive = true,
                    stat = "Life",
                    amount = "+1",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                }
            }
        };

        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats,
            CharacterStatModifiers statModifiers, Block block)
        {
            cardInfo.allowMultiple = false;

            cardInfo.categories = new[]
            {
                ExtraGameCards.MarioPowerUps
            };

            statModifiers.respawns = 1;
        }
    }
}