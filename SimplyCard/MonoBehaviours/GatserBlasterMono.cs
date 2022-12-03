using UnityEngine;
using System.Collections.Generic;
using HarmonyLib;
using ModdingUtils.MonoBehaviours;
using UnboundLib;
using ExtraGameCards.AssetsEmbedded;
using ExtraGameCards.Utils;
using Photon.Pun;
using System.Collections;

namespace ExtraGameCards.MonoBehaviours
{
    public class GatserBlasterMono : MonoBehaviour, IPunInstantiateMagicCallback
    {
        private float duration = 5f;

        public Player player;
        public Gun gun;
        public CharacterData data;
        public HealthHandler health;
        public Gravity gravity;
        public Block block;
        public GunAmmo gunAmmo;
        public CharacterStatModifiers statModifiers;

        private Vector2 targetPos;
        private Vector2 shootPos;
        private Vector2 originPos;
        private Vector2 direction;
        private Quaternion rotation;

        private GameObject blasterSprite;

        void IPunInstantiateMagicCallback.OnPhotonInstantiate(PhotonMessageInfo info)
        {
            object[] data = info.photonView.InstantiationData;
            targetPos = (Vector2)data[0];
            originPos = (Vector2)data[1];
            shootPos = (Vector2)data[2];
            direction = (Vector2)data[3];
        }

        void Awake()
        {
            ProjectileHit hit = gameObject.GetComponent<ProjectileHit>();
            hit.AddHitAction(OnHit);
        }

        void OnHit()
        {
            UnityEngine.Debug.Log("We hit something");
            targetPos = gameObject.transform.position;
            System.Random random = new System.Random();
            float rPosY = random.Next(((int)targetPos.y) - 20, ((int)targetPos.y) +21);
            float rPosX = random.Next(((int)targetPos.x) - 20, ((int)targetPos.x) + 21);
            shootPos = new Vector2(rPosX, rPosY);
            if (Mathf.Abs(shootPos.y) > Mathf.Abs(shootPos.x))
            {
                rPosX = random.Next(((int)shootPos.x) - 30, ((int)shootPos.x) + 31);
                if (shootPos.y >= 0)
                {
                    rPosY = 105f;
                }
                else
                {
                    rPosY = -105f;
                }
            }
            else
            {
                rPosY = random.Next(((int)shootPos.y) - 30, ((int)shootPos.y) + 31);
                if (shootPos.x >= 0)
                {
                    rPosX = 105f;
                }
                else
                {
                    rPosX = -105f;
                }
            }
            originPos = new Vector2(rPosX, rPosY);

            direction = targetPos - shootPos;
            rotation = Quaternion.LookRotation(direction, Vector3.up);

            UnityEngine.Debug.Log("Instantiate GasterBlaster");
            blasterSprite = PhotonNetwork.Instantiate(Assets.GasterBlasterSprite.name, Assets.GasterBlasterSprite.transform.position, Assets.GasterBlasterSprite.transform.rotation, data: new object[] { targetPos, originPos, shootPos, direction });
            blasterSprite.transform.position = originPos;
            blasterSprite.transform.rotation = rotation;

            UnityEngine.Debug.Log($"(CHECK) SHOOTING : {shootPos}, TARGET : {targetPos}, ROTATION : {rotation}");

            GasterBlasterInstantMono gasterBlasterInstantMono = blasterSprite.AddComponent<GasterBlasterInstantMono>();
            gasterBlasterInstantMono.player = player;
            gasterBlasterInstantMono.health = health;
            gasterBlasterInstantMono.block = block;
            gasterBlasterInstantMono.statModifiers = statModifiers;
            gasterBlasterInstantMono.data = data;
            gasterBlasterInstantMono.gravity = gravity;
            gasterBlasterInstantMono.gun = gun;
            gasterBlasterInstantMono.gunAmmo = gunAmmo;

            gasterBlasterInstantMono.originPos = originPos;
            gasterBlasterInstantMono.direction = direction;
            gasterBlasterInstantMono.shootingPos = shootPos;
            gasterBlasterInstantMono.targetPos = targetPos;
            gasterBlasterInstantMono.rotation = rotation;


        }
    }
}
