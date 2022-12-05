using ExtraGameCards.AssetsEmbedded;
using ExtraGameCards.MonoBehaviours;
using ModdingUtils.Extensions;
using ModsPlus;
using Photon.Pun;
using System.Collections;
using UnboundLib;
using UnityEngine;

namespace ExtraGameCards.Cards
{
    class GasterBlaster : CustomEffectCard<BlastEffect>
    {
        public override CardDetails Details => new CardDetails
        {
            Title = "Gaster Blaster",
            Description = "your opponents are gonna have a bad time",
            ModName = EGC.ModInitials,
            Art = Assets.GasterBlasterArt,
            Rarity = CardInfo.Rarity.Rare,
            Theme = CardThemeColor.CardThemeColorType.TechWhite,
            Stats = new CardInfoStat[]
            {
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Blast",
                    amount = "10%",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Attack Speed",
                    amount = "+20%",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
                new CardInfoStat()
                {
                    positive = false,
                    stat = "Health",
                    amount = "-30%",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
            }
        };

        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            //UnityEngine.Debug.Log($"[{ExtraCards.ModInitials}][Card] {GetTitle()} has been setup.");
            cardInfo.GetAdditionalData().canBeReassigned = false;
            cardInfo.allowMultiple = false;

            statModifiers.health = 0.7f;
            gun.attackSpeed = 0.8f;
        }
    }
    public class BlastEffect : CardEffect
    {
        private GameObject Projectile;

        public override void OnShoot(GameObject projectile)
        {
            UnityEngine.Debug.Log(player.transform.position);

            Projectile = projectile;
            if (PhotonNetwork.OfflineMode)
            {
                this.RPCA_Blast(UnityEngine.Random.Range(0, 99));
            }
            else
            {
                this.gameObject.GetComponent<PhotonView>().RPC("RPCA_Blast", RpcTarget.All, new object[]
                {
                    UnityEngine.Random.Range(0, 99)
                });
            }

        }

        [PunRPC]
        private void RPCA_Blast(int random)
        {
            if (random < 20 && (bool)this.data.playerVel.GetFieldValue("simulated"))
            {
                GatserBlasterMono sensor = Projectile.AddComponent<GatserBlasterMono>();
                sensor.health = health;
                sensor.gravity = gravity;
                sensor.block = block;
                sensor.data = data;
                sensor.player = player;
                sensor.gun = gun;
                sensor.gunAmmo = gunAmmo;
            }
        }
    }

}