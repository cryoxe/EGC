using EGC.AssetsEmbedded;
using ModsPlus;

namespace EGC.Cards.MarioPowerUps
{
    internal class MiniMushroom : SimpleCard
    {
        public static CardInfo miniMushroomCard;
        public override CardDetails Details => new CardDetails
        {
            Title = "Mini Mushroom",
            Description = "This one makes you tiny",
            ModName = EGC.ExtraGameCards.ModInitials,
            Art = Assets.MiniMushArt,
            Rarity = CardInfo.Rarity.Uncommon,
            Theme = CardThemeColor.CardThemeColorType.DefensiveBlue,
            Stats = new CardInfoStat[]
            {
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Gravity",
                    amount = "-45%",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Size",
                    amount = "-40%",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Speed",
                    amount = "+25%",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
                new CardInfoStat()
                {
                    positive = false,
                    stat = "Health",
                    amount = "-25%",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
            }
        };

        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            cardInfo.allowMultiple = true;

            cardInfo.categories = new CardCategory[]
            {
                EGC.ExtraGameCards.MarioPowerUps
            };

            //UnityEngine.Debug.Log($"[{ExtraCards.ModInitials}][Card] {GetTitle()} has been setup.");
            statModifiers.health = 0.75f;
            statModifiers.gravity = 0.55f;
            statModifiers.movementSpeed = 1.25f;
            statModifiers.sizeMultiplier = 0.6f;
        }
    }
}