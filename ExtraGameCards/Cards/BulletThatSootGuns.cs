using ModsPlus;

namespace EGC.Cards
{
    internal class BulletThatSootGuns : SimpleCard
    {
        public override CardDetails Details => new CardDetails
        {
            Title = "Bullet Gun",
            Description = "The bullet that shoot guns!",
            ModName = ExtraGameCards.ModInitials,
            Art = null,
            Rarity = CardInfo.Rarity.Uncommon,
            Theme = CardThemeColor.CardThemeColorType.FirepowerYellow,
            Stats = new[]
            {
                new CardInfoStat
                {
                    positive = false,
                    stat = "Attack Speed",
                    amount = "-30%",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
                new CardInfoStat
                {
                    positive = false,
                    stat = "Damage",
                    amount = "-30%",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
                new CardInfoStat
                {
                    positive = false,
                    stat = "Bullet Speed",
                    amount = "-30%",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
                new CardInfoStat
                {
                    positive = false,
                    stat = "Reload Speed",
                    amount = "-30%",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
            }
        };

        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats,
            CharacterStatModifiers statModifiers, Block block)
        {
            //UnityEngine.Debug.Log($"[{ExtraCards.ModInitials}][Card] {GetTitle()} has been setup.");
            cardInfo.allowMultiple = false;

            gun.reloadTime = 1.3f;
            gun.projectileSpeed = 0.7f;
            gun.attackSpeed = 1.3f;
            gun.damage = 0.7f;
        }
    }
}