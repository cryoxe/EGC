using EGC.AssetsEmbedded;
using ModsPlus;
using UnityEngine;

namespace EGC.Cards.MarioPowerUps
{
    internal class PoisonousMushroom : CustomEffectCard<PoisonousEffect>
    {
        public static CardInfo poisonousMushroomCard;

        public override CardDetails Details => new CardDetails
        {
            Title = "Poison Mushroom",
            Description = "Ouch, it hurts! You take damage when you shoot!",
            ModName = EGC.ExtraGameCards.ModInitials,
            Art = Assets.PoisonMushArt,
            Rarity = CardInfo.Rarity.Rare,
            Theme = CardThemeColor.CardThemeColorType.EvilPurple,
            Stats = new CardInfoStat[]
            {
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
                    stat = "Flat Damage",
                    amount = "+10%",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Lifesteal",
                    amount = "+200%",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
                new CardInfoStat()
                {
                    positive = false,
                    stat = "Reload Time",
                    amount = "+0.5s",
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
            gun.projectileColor = Color.magenta;
            gun.damage = 1.2f;
            gun.percentageDamage = 0.1f;
            gun.reloadTimeAdd = 0.5f;    
        }

        protected override void Added(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            characterStats.lifeSteal = (characterStats.lifeSteal != 0f) ? (characterStats.lifeSteal * 3) : (characterStats.lifeSteal + 2f);
        }

    }

    internal class PoisonousEffect : CardEffect
    {
        private Color32 purple = new Color32(186, 85, 211, 255);

        public override void OnShoot(GameObject projectile)
        {
            Vector2 damage = Vector2.up * 50;
            player.data.healthHandler.TakeDamageOverTime(damage, Vector2.zero, 10, 0.25f, purple, lethal: false);
        }
    }
}