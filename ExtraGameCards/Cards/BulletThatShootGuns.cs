using System;
using EGC.AssetsEmbedded;
using EGC.MonoBehaviours;
using ModdingUtils.GameModes;
using ModsPlus;
using Photon.Pun.UtilityScripts;
using UnboundLib;
using UnityEngine;
using UnityEngine.UI;

namespace EGC.Cards
{
    internal class BulletThatShootGuns : CustomEffectCard<BulletThatShootGunsEffect>
    {
        public override CardDetails Details => new CardDetails
        {
            Title = "Bullet Gun",
            Description = "The bullet that shoot guns!",
            ModName = ExtraGameCards.ModInitials,
            Art = null,
            Rarity = CardInfo.Rarity.Rare,
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
            cardInfo.allowMultiple = false;

            gun.reloadTime = 1.3f;
            gun.projectileSpeed = 0.9f;
            gun.attackSpeed = 1.3f;
            gun.damage = 0.7f;

            gun.gravity = 1.3f;
            gun.recoil = 0.1f;
            gun.knockback = 50f;

            gun.destroyBulletAfter = 3.5f;
        }
    }

    public class BulletThatShootGunsEffect : CardEffect
    {
        private Gun gunHeld = null!;

        private GameObject gunAmmo = null!;

        private GameObject gunBarrel = null!;
        private SpriteRenderer gunBarrelSpriteRenderer = null!;

        private GameObject gunHandle = null!;
        private SpriteMask gunBarrelSpriteMask = null!;
        private SpriteMask gunHandleSpriteMask = null!;

        private Sprite bulletSprite = null!;

        private Sprite oldBarrelSprite = null!;
        private Vector3 oldBarrelScale = Vector3.zero;
        private Vector3 oldBarrelRotation = Vector3.zero;
        private Material oldBarrelMaterial = null!;
        private int oldBarrelFrontSortingLayerID;


        protected override void Start()
        {
            base.Start();

            bulletSprite = Assets.BulletSprite.GetComponent<SpriteRenderer>().sprite;

            gunHeld = player.GetComponent<Holding>().holdable.GetComponent<Gun>();

            gunAmmo = gunHeld.transform.GetChild(1).GetChild(1).gameObject;
            gunBarrel = gunHeld.transform.GetChild(1).GetChild(3).gameObject;
            gunHandle = gunHeld.transform.GetChild(1).GetChild(2).gameObject;

            gunBarrelSpriteRenderer = gunBarrel.GetComponent<SpriteRenderer>();
            gunBarrelSpriteMask = gunBarrel.GetComponent<SpriteMask>();

            gunHandleSpriteMask = gunHandle.GetComponent<SpriteMask>();

            oldBarrelScale = gunBarrel.transform.localScale;
            oldBarrelRotation = gunBarrel.transform.localEulerAngles;
            oldBarrelSprite = gunBarrelSpriteRenderer.sprite;

            oldBarrelMaterial = gunBarrelSpriteRenderer.material;
            oldBarrelFrontSortingLayerID = gunBarrelSpriteMask.frontSortingLayerID;

            SetNewGunSprite();
        }

        public override void OnShoot(GameObject projectile)
        {
            ProjectileHit projectileHit = projectile.GetComponent<ProjectileHit>();
            MoveTransform move = projectile.GetComponent<MoveTransform>();

            this.ExecuteAfterFrames(5, () =>
            {
                if (projectileHit.ownWeapon != gunHeld.gameObject) return;

                var spawnedAttack = projectile.GetComponent<SpawnedAttack>();
                if (!spawnedAttack)
                    return;

                GameObject gunSprite = Instantiate(Assets.GunSprite, projectile.transform);
                gunSprite.transform.rotation = Quaternion.Euler(
                    new Vector3(0, 0, Mathf.Atan2(move.velocity.y, move.velocity.x) * Mathf.Rad2Deg));

                gunSprite.transform.localScale = new Vector3(10f, 10f, 10f);
                gunSprite.GetComponent<SpriteRenderer>().sortingOrder = 1000000;

                var mono = gunSprite.AddComponent<BulletThatShootGunsMono>();
                mono.Player = player;
                mono.Gun = gunHeld;
            });
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            SetOldGunSprite();
        }

        private void SetNewGunSprite()
        {
            gunBarrelSpriteRenderer.sprite = bulletSprite;

            gunBarrelSpriteMask.sprite = bulletSprite;

            gunBarrel.transform.localScale = new Vector3(7.5f, 7.5f, 1f);
            gunBarrel.transform.localEulerAngles = new Vector3(0, 0, 90);

            gunBarrel.GetComponent<SFPolygon>().enabled = false;

            gunBarrelSpriteMask.material = new Material(Shader.Find("Sprites/Default"))
            {
                color = new Color(0.9f, 0.9f, 0.9f, 1f)
            };
            gunBarrelSpriteMask.frontSortingLayerID = 0;

            gunHandleSpriteMask.enabled = false;

            gunAmmo.transform.localScale = new Vector3(1.5f, 1.5f, 1f);
        }

        private void SetOldGunSprite()
        {
            gunBarrelSpriteRenderer.sprite = oldBarrelSprite;
            gunBarrelSpriteMask.sprite = oldBarrelSprite;

            gunBarrel.transform.localScale = oldBarrelScale;
            gunBarrel.transform.localEulerAngles = oldBarrelRotation;

            gunBarrelSpriteRenderer.material = oldBarrelMaterial;
            gunBarrelSpriteMask.frontSortingLayerID = oldBarrelFrontSortingLayerID;

            gunBarrel.GetComponent<SFPolygon>().enabled = true;

            gunHandleSpriteMask.enabled = true;

            gunAmmo.transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }
}