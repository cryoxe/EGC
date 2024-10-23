using EGC.AssetsEmbedded;
using ModsPlus;

namespace EGC.Cards.MarioPowerUps
{
    internal class BooMushroom : SimpleCard
    {
        public static CardInfo BooMushroomCard = null!;

        public override CardDetails Details => new CardDetails
        {
            Title = "Boo Mushroom",
            Description = "You're a ghost now !",
            ModName = ExtraGameCards.ModInitials,
            Art = Assets.BooMushArt,
            Rarity = CardInfo.Rarity.Uncommon,
            Theme = CardThemeColor.CardThemeColorType.TechWhite,
            Stats = new[]
            {
                new CardInfoStat
                {
                    positive = true,
                    stat = "Bullets",
                    amount = "+ Ghost",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
                new CardInfoStat
                {
                    positive = true,
                    stat = "Damage",
                    amount = "Unblockable",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
                new CardInfoStat
                {
                    positive = false,
                    stat = "Damage",
                    amount = "-40%",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
                new CardInfoStat
                {
                    positive = false,
                    stat = "Health",
                    amount = "-30%",
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

            statModifiers.health = 0.7f;
            gun.damage = 0.6f;
            gun.unblockable = true;
            gun.ignoreWalls = true;
        }
    }
}