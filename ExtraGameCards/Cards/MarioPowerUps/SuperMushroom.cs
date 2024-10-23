using EGC.AssetsEmbedded;
using ModsPlus;

namespace EGC.Cards.MarioPowerUps
{
    internal class SuperMushroom : SimpleCard
    {
        public static CardInfo superMushroomCard;
        public override CardDetails Details => new CardDetails
        {
            Title = "Super Mushroom",
            Description = "You're a big boy now!",
            ModName = EGC.ExtraGameCards.ModInitials,
            Art = Assets.SuperMushArt,
            Rarity = CardInfo.Rarity.Uncommon,
            Theme = CardThemeColor.CardThemeColorType.DestructiveRed,
            Stats = new CardInfoStat[]
            {
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Health",
                    amount = "+30%",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Damage",
                    amount = "+20%",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Gravity",
                    amount = "-10%",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
                new CardInfoStat()
                {
                    positive = false,
                    stat = "Size",
                    amount = "+20%",
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
            statModifiers.health = 1.3f;
            gun.damage = 1.2f;
            statModifiers.gravity = 0.9f;
            statModifiers.sizeMultiplier = 1.2f;
        }
    }
}