using ExtraGameCards.AssetsEmbedded;
using ExtraGameCards.MonoBehaviours;
using ModdingUtils.Extensions;
using ModsPlus;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ExtraGameCards.Cards
{
    class ClayBullet : CustomEffectCard<ClayEffect>
    {
        public override CardDetails Details => new CardDetails
        {
            Title = "Clay Bullet",
            Description = "Your bullets detonate on block !",
            ModName = EGC.ModInitials,
            Art = null,
            Rarity = CardInfo.Rarity.Rare,
            Theme = CardThemeColor.CardThemeColorType.PoisonGreen,
            Stats = new CardInfoStat[]
            {
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Detonation",
                    amount = "+6",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
                new CardInfoStat()
                {
                    positive = false,
                    stat = "Attack Speed",
                    amount = "-20%",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
                new CardInfoStat()
                {
                    positive = false,
                    stat = "Damage",
                    amount = "-15%",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
                new CardInfoStat()
                {
                    positive = false,
                    stat = "Reload Speed",
                    amount = "+0.25",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
            }
        };

        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            //UnityEngine.Debug.Log($"[{ExtraCards.ModInitials}][Card] {GetTitle()} has been setup.");
            cardInfo.GetAdditionalData().canBeReassigned = false;

            gun.attackSpeed = 1.2f;
            gun.damage = 0.85f;
            gun.reloadTimeAdd = 0.25f;
        }
        protected override void Added(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            Extensions.CharacterStatModifiersExtension.GetAdditionalData(characterStats).bombs += 6;
        }
        protected override void Removed(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
        }
    }

    public class ClayEffect : CardEffect
    {
        private readonly GameObject toxicCloudCard = (GameObject)Resources.Load("0 cards/Toxic cloud");
        private readonly GameObject explosionCard = (GameObject)Resources.Load("0 cards/Explosive bullet");
        public List<Vector3> bombPositions = new List<Vector3>();


        public override void OnShoot(GameObject projectile)
        {
            var clay = projectile.AddComponent<ClayBulletMono>();
            clay.player = player;
            clay.clayBullet = this;
        }

        public override void OnBlock(BlockTrigger.BlockTriggerType blockTriggerType)
        {
            GameObject a_explosion = explosionCard.GetComponent<Gun>().objectsToSpawn[0].effect;
            GameObject a_toxicCloud = toxicCloudCard.GetComponent<Gun>().objectsToSpawn[0].effect;
            Explosion explo = a_explosion.GetComponent<Explosion>();
            explo.damage = 2f;
            explo.force = 4000f;
            explo.dmgColor = Color.green;
            explo.scaleDmg = true;
            explo.auto = true;
            Explosion toxic = a_toxicCloud.GetComponent<Explosion>();
            toxic.damage = 0.8f;
            toxic.dmgColor = Color.green;
            toxic.scaleDmg = true;
            toxic.auto = true;



            foreach (Vector3 bombPosition in bombPositions)
            {
                Instantiate(a_explosion, bombPosition, Quaternion.identity);
                Instantiate(a_toxicCloud, bombPosition, Quaternion.identity);
            }
        }
    }
}