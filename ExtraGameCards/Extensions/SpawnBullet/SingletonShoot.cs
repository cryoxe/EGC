using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

namespace EGC.Extensions.SpawnBullet
{
    public class SingletonShoot : MonoBehaviour
    {
        private static SingletonShoot _instance = null!;

        private Gun gunToShootFrom = null!;
        public void SetGunToShootFrom(Gun gun) => gunToShootFrom = gun;

        private PhotonView _photonView;
        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
            }
            else
            {
                Destroy(this);
            }

            _photonView = GetComponent<PhotonView>();

            if (_photonView == null)
            {
                Debug.LogError("PhotonView is missing on SingletonShoot");
            }

            UnityEngine.Debug.Log("SingletonShoot is awake");
        }

        public void Shoot(Gun gun, int bulletViewID, int numProj, float dmgM, float seed)
        {
            gunToShootFrom = gun;
            _photonView.RPC("RPCA_Shoot", RpcTarget.All, bulletViewID, numProj, dmgM, seed);
        }

        [PunRPC]
        private void RPCA_Shoot(int bulletViewID, int numProj, float dmgM, float seed)
        {
            GameObject bulletObj = PhotonView.Find(bulletViewID).gameObject;
            gunToShootFrom.BulletInit(bulletObj, numProj, dmgM, seed);
        }
    }
}