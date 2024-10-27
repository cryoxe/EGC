using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Photon.Pun;

namespace EGC.Extensions.SpawnBullet
{
    public class SpawnBulletsEffectController : MonoBehaviourPun
    {
        private readonly List<SpawnBulletsEffect> spawnBulletsComponents = new List<SpawnBulletsEffect>();

        private void Awake()
        {
            // spawnBulletsComponents.AddRange(GetComponents<SpawnBulletsEffect>());
        }

        public void Shoot(int componentIndex, int bulletViewID, int numProj, float dmgM, float seed)
        {
            photonView.RPC("RPCA_ShootController", RpcTarget.All, componentIndex, bulletViewID,  numProj,  dmgM,  seed);
        }

        [PunRPC]
        [UsedImplicitly]
        private void RPCA_ShootController(int componentIndex, int bulletViewID, int numProj, float dmgM, float seed)
        {
            if (componentIndex >= 0 && componentIndex < spawnBulletsComponents.Count)
            {
                UnityEngine.Debug.Log($"RPCA_Shoot: {componentIndex}");
                spawnBulletsComponents[componentIndex].HandleShoot(bulletViewID,  numProj,  dmgM,  seed);
            }
        }

        public void AddSpawnBulletEffect(SpawnBulletsEffect spawnBulletsEffect)
        {
            spawnBulletsComponents.Add(spawnBulletsEffect);
            UnityEngine.Debug.Log($"Added SpawnBulletEffect, total: {spawnBulletsComponents.Count}");
        }

        public void RemoveSpawnBulletEffect(SpawnBulletsEffect spawnBulletsEffect)
        {
            spawnBulletsComponents.Remove(spawnBulletsEffect);
            UnityEngine.Debug.Log($"Removed SpawnBulletEffect, total: {spawnBulletsComponents.Count}");
        }
    }
}