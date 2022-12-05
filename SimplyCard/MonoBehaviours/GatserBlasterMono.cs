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

        public Player player;
        public Gun gun;
        public CharacterData data;
        public HealthHandler health;
        public Gravity gravity;
        public Block block;
        public GunAmmo gunAmmo;
        public CharacterStatModifiers statModifiers;

        private Vector2 targetPos;
        private Vector2 originPos;
        private Vector3 direction;
        private Quaternion rotation;

        private GameObject blasterSprite;

        void IPunInstantiateMagicCallback.OnPhotonInstantiate(PhotonMessageInfo info)
        {
            object[] data = info.photonView.InstantiationData;
            originPos = (Vector2)data[0];
        }

        void Start()
        {
            ProjectileHit hit = gameObject.GetComponent<ProjectileHit>();
            hit.AddHitAction(OnHit);
        }

        void OnHit()
        {
            targetPos = gameObject.transform.position;
            bool ok = false;
            while (!ok){
                originPos = targetPos + Random.insideUnitCircle * 15;
                if (originPos.x > -36 & originPos.x < 36 & originPos.y > -18 & originPos.y < 18)
                {
                    ok = true;
                }
            }

            direction = targetPos - originPos;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90;
            rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            UnityEngine.Debug.Log("Instantiate GasterBlaster");
            blasterSprite = PhotonNetwork.Instantiate(Assets.GasterBlasterSprite.name, originPos, rotation, data: new object[] { originPos });

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
            gasterBlasterInstantMono.targetPos = targetPos;
            gasterBlasterInstantMono.rotation = rotation;


        }
    }
}
