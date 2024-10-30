using EGC.AssetsEmbedded;
using ExitGames.Client.Photon.StructWrapping;
using ModsPlus;
using UnboundLib;
using UnityEngine;

namespace EGC.Cards
{
    public class TheBucket : CustomEffectCard<BucketEffect>
    {
        public override CardDetails Details => new CardDetails
        {
            Title = "The Stanley Parable Reassurance Bucket (TM)",
            Description = "A feeling of safety washes over you.",
            ModName = ExtraGameCards.ModInitials,
            Art = null,
            Rarity = CardInfo.Rarity.Rare,
            Theme = CardThemeColor.CardThemeColorType.FirepowerYellow,
            Stats = new[]
            {
                new CardInfoStat
                {
                    positive = true,
                    stat = "Health",
                    amount = "+20%",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
                new CardInfoStat
                {
                    positive = true,
                    stat = "Additional block",
                    amount = "+1",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
                new CardInfoStat
                {
                    positive = false,
                    stat = "Ammo",
                    amount = "-1",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
                new CardInfoStat
                {
                    positive = false,
                    stat = "Gravity",
                    amount = "+30%",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
            }
        };

        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats,
            CharacterStatModifiers statModifiers, Block block)
        {
            cardInfo.allowMultiple = false;

            block.additionalBlocks = 1;
            statModifiers.health = 1.2f;

            gun.ammo = -1;
            statModifiers.gravity = 1.3f;
        }
    }

    public class BucketEffect : CardEffect
    {
        protected override void Start()
        {
            base.Start();

            var audioSource = gameObject.AddComponent<AudioSource>();

            this.ExecuteAfterFrames(3, () =>
            {
                audioSource.PlayOneShot(Assets.BucketNoise,
                    0.6f * Optionshandler.vol_Master * Optionshandler.vol_Sfx);

                GameObject limbs = player.transform.Find("Limbs").gameObject;
                GameObject blockOrb = limbs.transform.GetChild(1).Find("ShieldStone").gameObject;

                GameObject canvas = blockOrb.transform.Find("Canvas").gameObject;
                canvas.GetComponent<Canvas>().sortingOrder = 101;

                GameObject bucket = Instantiate(Assets.BucketSprite, blockOrb.transform);
                bucket.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);

                bucket.GetComponent<SpriteRenderer>().sortingOrder = 100;
            });
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            GameObject limbs = player.transform.Find("Limbs").gameObject;
            GameObject blockOrb = limbs.transform.GetChild(1).Find("ShieldStone").gameObject;

            GameObject canvas = blockOrb.transform.Find("Canvas").gameObject;
            canvas.GetComponent<Canvas>().sortingOrder = 0;

            Destroy(blockOrb.transform.Find("S_Bucket(Clone)").gameObject);

            Destroy(gameObject.GetComponent<AudioSource>());
        }
    }
}