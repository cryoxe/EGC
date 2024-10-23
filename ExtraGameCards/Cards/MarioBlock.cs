using EGC.AssetsEmbedded;
using EGC.Cards.MarioPowerUps;
using ModdingUtils.Extensions;
using ModsPlus;
using Random = UnityEngine.Random;

namespace EGC.Cards
{
    internal class MarioBlock : SimpleCard
    {
        public override CardDetails Details => new CardDetails
        {
            Title = "? Block",
            Description = "Power-Ups Await",
            ModName = ExtraGameCards.ModInitials,
            Art = Assets.MarioArt,
            Rarity = CardInfo.Rarity.Uncommon,
            Theme = CardThemeColor.CardThemeColorType.FirepowerYellow,
            Stats = new[]
            {
                new CardInfoStat
                {
                    positive = true,
                    stat = "Random Power-Up",
                    amount = "+1",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                }
            }
        };

        public override void SetupCard(
            CardInfo cardInfo,
            Gun gun,
            ApplyCardStats cardStats,
            CharacterStatModifiers statModifiers,
            Block block)
        {
            cardInfo.GetAdditionalData().canBeReassigned = false;
        }

        protected override void Added(
            Player player,
            Gun gun,
            GunAmmo gunAmmo,
            CharacterData data,
            HealthHandler health,
            Gravity gravity,
            Block block,
            CharacterStatModifiers characterStats)
        {
            AddPowerUp(player, characterStats);
        }

        private static void AddPowerUp(Player player, CharacterStatModifiers characterStat)
        {
            CardInfo addedCard = GetRandomPowerUp(characterStat);
            ModdingUtils.Utils.Cards.instance.AddCardToPlayer(player, addedCard, addToCardBar: true);
            ModdingUtils.Utils.CardBarUtils.instance.ShowAtEndOfPhase(player, addedCard);
        }

        private static CardInfo GetRandomPowerUp(CharacterStatModifiers characterStats)
        {
            var characterData = Extensions.CharacterStatModifiersExtension.GetAdditionalData(characterStats);

            string[] mushroomCards =
            {
                SuperMushroom.SuperMushroomCard.name,
                SuperMushroom.SuperMushroomCard.name,
                MiniMushroom.MiniMushroomCard.name,
                MiniMushroom.MiniMushroomCard.name,
                BooMushroom.BooMushroomCard.name,
                BooMushroom.BooMushroomCard.name,
                OneUpMushroom.OneUpMushroomCard.name,
                PoisonousMushroom.PoisonousMushroomCard.name
            };

            bool[] mushroomFlags =
            {
                true, // SuperMushroom is always available
                true, // SuperMushroom is always available
                characterData.hasMiniMush,
                characterData.hasMiniMush,
                characterData.hasBooMush,
                characterData.hasBooMush,
                characterData.hasOneUpMush,
                characterData.hasPoisonMush
            };

            int rng = Random.Range(0, mushroomCards.Length);

            if (mushroomFlags[rng])
                return GetCard(SuperMushroom.SuperMushroomCard.name);

            switch (rng)
            {
                case 2:
                case 3:
                    characterData.hasMiniMush = true;
                    break;
                case 4:
                case 5:
                    characterData.hasBooMush = true;
                    break;
                case 6:
                    characterData.hasOneUpMush = true;
                    break;
                case 7:
                    characterData.hasPoisonMush = true;
                    break;
            }

            return GetCard(mushroomCards[rng]);

            CardInfo GetCard(string name) => ModdingUtils.Utils.Cards.instance.GetCardWithObjectName(name);
        }
    }
}