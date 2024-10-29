using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;
using Photon.Pun;
using UnboundLib;
using UnboundLib.GameModes;
using UnityEngine;
using Object = UnityEngine.Object;

namespace EGC.Extensions.SpawnBullet
{

	public class SpawnBulletsEffect : MonoBehaviour
	{
		private float initialDelay = 1f;

		private int numBullets = 1;
		private int numShot = 0;
		private Gun gunToShootFrom = null;
		private List<Vector3> directionsToShoot = new List<Vector3>();
		private List<Vector3> positionsToShootFrom = new List<Vector3>();
		private float timeBetweenShots = 0f;
		private float timeSinceLastShot;
		private GameObject newWeaponsBase;

		private Player player;

		private SpawnBulletsEffectController? controller;
		private int id;
		private void Awake()
		{
			player = gameObject.GetComponent<Player>();

			controller = gameObject.GetOrAddComponent<SpawnBulletsEffectController>();

			if (controller is null)
			{
				UnityEngine.Debug.LogError("controller is null");
				return;
			}

			controller.AddSpawnBulletEffect(this);


			var allEffects = GetComponents<SpawnBulletsEffect>();
			id = 0;
			foreach (var effect in allEffects)
			{
				if (effect == this)
				{
					break;
				}
				id++;
			}
		}

		private void Start()
		{
			ResetTimer();
			timeSinceLastShot += initialDelay;
		}

		private void Update()
		{
			if (numShot >= numBullets || gunToShootFrom == null)
			{
				Destroy(this);
			}
			else if (Time.time >= timeSinceLastShot + timeBetweenShots)
			{
				Shoot();
			}
		}

		private void OnDisable()
		{
			Destroy(this);
		}

		private void OnDestroy()
		{
			controller.RemoveSpawnBulletEffect(this);
			Destroy(newWeaponsBase);
		}

		private void Shoot()
		{
			int currentNumberOfProjectiles = gunToShootFrom.lockGunToDefault ? 1 : (gunToShootFrom.numberOfProjectiles + Mathf.RoundToInt(gunToShootFrom.chargeNumberOfProjectilesTo * 0f));
			for (int i = 0; i < gunToShootFrom.projectiles.Length; i++)
			{
				for (int j = 0; j < currentNumberOfProjectiles; j++)
				{
					Vector3 directionToShootThisBullet;
					if (directionsToShoot.Count == 0)
					{
						directionToShootThisBullet = Vector3.down;
					}
					else
					{
						directionToShootThisBullet = directionsToShoot[numShot % directionsToShoot.Count];
					}
					if (gunToShootFrom.spread != 0f)
					{
						// randomly spread shots
						float d = gunToShootFrom.multiplySpread;
						float num = UnityEngine.Random.Range(-gunToShootFrom.spread, gunToShootFrom.spread);
						num /= (1f + gunToShootFrom.projectileSpeed * 0.5f) * 0.5f;
						directionToShootThisBullet += Vector3.Cross(directionToShootThisBullet, Vector3.forward) * num * d;
					}

					if ((bool)typeof(Gun).InvokeMember("CheckIsMine", BindingFlags.Instance | BindingFlags.InvokeMethod | BindingFlags.NonPublic, null, gunToShootFrom, new object[] { }))
					{
						Vector3 positionToShootFrom;
						if (positionsToShootFrom.Count == 0)
						{
							positionToShootFrom = Vector3.zero;
						}
						else
						{
							positionToShootFrom = positionsToShootFrom[numShot % positionsToShootFrom.Count];
						}
						GameObject bulletGameObject = PhotonNetwork.Instantiate(gunToShootFrom.projectiles[i].objectToSpawn.gameObject.name, positionToShootFrom, Quaternion.LookRotation(directionToShootThisBullet), 0, null);

						if (PhotonNetwork.OfflineMode)
						{
							HandleShoot(bulletGameObject.GetComponent<PhotonView>().ViewID, currentNumberOfProjectiles, 1f, UnityEngine.Random.Range(0f, 1f));
						}
						else
						{
							// gameObject.GetComponent<PhotonView>().RPC("RPCA_Shoot", RpcTarget.All, new object[]
							// {
							// 	bulletGameObject.GetComponent<PhotonView>().ViewID,
							// 	currentNumberOfProjectiles,
							// 	1f,
							// 	UnityEngine.Random.Range(0f, 1f)
							// });

							if (controller is null)
								return;

							var bulletView = bulletGameObject.GetComponent<PhotonView>();
							if (bulletView is null)
							{
								UnityEngine.Debug.LogError("bulletGameObject.GetComponent<PhotonView>() is null");
								return;
							}

							UpdateID();
							controller.Shoot(id, bulletView.ViewID, currentNumberOfProjectiles, 1f, UnityEngine.Random.Range(0f, 1f));
						}
					}
				}
			}
			ResetTimer();

		}

		// [PunRPC]
		// private void RPCA_Shoot(int bulletViewID, int numProj, float dmgM, float seed)
		// {
		// 	GameObject bulletObj = PhotonView.Find(bulletViewID).gameObject;
		// 	gunToShootFrom.BulletInit(bulletObj, numProj, dmgM, seed, true);
		// 	numShot++;
		// }


		public void HandleShoot(int bulletViewID, int numProj, float dmgM, float seed)
		{
			GameObject bulletObj = PhotonView.Find(bulletViewID).gameObject;
			if (!bulletObj)
			{
				UnityEngine.Debug.LogError("bulletObj is null");
				return;
			}

			gunToShootFrom.BulletInit(bulletObj, numProj, dmgM, seed, true);
			numShot++;
		}

		public void SetGun(Gun gun)
		{
			newWeaponsBase = Instantiate(player.GetComponent<Holding>().holdable.GetComponent<Gun>().gameObject, new Vector3(500f, 500f, -100f), Quaternion.identity);
			DontDestroyOnLoad(newWeaponsBase);
			foreach (Transform child in newWeaponsBase.transform)
			{
				if (child.GetComponentInChildren<Renderer>() != null)
				{
					foreach (Renderer renderer in child.GetComponentsInChildren<Renderer>())
					{
						renderer.enabled = false;
					}
				}
			}
			gunToShootFrom = newWeaponsBase.GetComponent<Gun>();
			CopyGunStats(gun, gunToShootFrom);
			//Destroy(gun, 1f);
		}
		public void SetNumBullets(int num)
		{
			numBullets = num;
		}
		public void SetPosition(Vector3 pos)
		{
			positionsToShootFrom = new List<Vector3> { pos };
		}
		public void SetDirection(Vector3 dir)
		{
			directionsToShoot = new List<Vector3> { dir };
		}
		public void SetPositions(List<Vector3> pos)
		{
			positionsToShootFrom = pos;
		}
		public void SetDirections(List<Vector3> dir)
		{
			directionsToShoot = dir;
		}
		public void SetTimeBetweenShots(float delay)
		{
			timeBetweenShots = delay;
		}
		public void SetInitialDelay(float delay)
		{
			initialDelay = delay;
		}
		private void ResetTimer()
		{
			timeSinceLastShot = Time.time;
		}
		public static void CopyGunStats(Gun copyFromGun, Gun copyToGun)
		{
			copyToGun.ammo = copyFromGun.ammo;
			copyToGun.ammoReg = copyFromGun.ammoReg;
			copyToGun.attackID = copyFromGun.attackID;
			copyToGun.attackSpeed = copyFromGun.attackSpeed;
			copyToGun.attackSpeedMultiplier = copyFromGun.attackSpeedMultiplier;
			copyToGun.bodyRecoil = copyFromGun.bodyRecoil;
			copyToGun.bulletDamageMultiplier = copyFromGun.bulletDamageMultiplier;
			copyToGun.bulletPortal = copyFromGun.bulletPortal;
			copyToGun.bursts = copyFromGun.bursts;
			copyToGun.chargeDamageMultiplier = copyFromGun.chargeDamageMultiplier;
			copyToGun.chargeEvenSpreadTo = copyFromGun.chargeEvenSpreadTo;
			copyToGun.chargeNumberOfProjectilesTo = copyFromGun.chargeNumberOfProjectilesTo;
			copyToGun.chargeRecoilTo = copyFromGun.chargeRecoilTo;
			copyToGun.chargeSpeedTo = copyFromGun.chargeSpeedTo;
			copyToGun.chargeSpreadTo = copyFromGun.chargeSpreadTo;
			copyToGun.cos = copyFromGun.cos;
			copyToGun.currentCharge = copyFromGun.currentCharge;
			copyToGun.damage = copyFromGun.damage;
			copyToGun.damageAfterDistanceMultiplier = copyFromGun.damageAfterDistanceMultiplier;
			copyToGun.defaultCooldown = copyFromGun.defaultCooldown;
			copyToGun.dmgMOnBounce = copyFromGun.dmgMOnBounce;
			copyToGun.dontAllowAutoFire = copyFromGun.dontAllowAutoFire;
			copyToGun.drag = copyFromGun.drag;
			copyToGun.dragMinSpeed = copyFromGun.dragMinSpeed;
			copyToGun.evenSpread = copyFromGun.evenSpread;
			copyToGun.explodeNearEnemyDamage = copyFromGun.explodeNearEnemyDamage;
			copyToGun.explodeNearEnemyRange = copyFromGun.explodeNearEnemyRange;
			copyToGun.forceSpecificAttackSpeed = copyFromGun.forceSpecificAttackSpeed;
			copyToGun.forceSpecificShake = copyFromGun.forceSpecificShake;
			copyToGun.gravity = copyFromGun.gravity;
			copyToGun.hitMovementMultiplier = copyFromGun.hitMovementMultiplier;
			//copyToGun.holdable = copyFromGun.holdable;
			copyToGun.ignoreWalls = copyFromGun.ignoreWalls;
			copyToGun.isProjectileGun = copyFromGun.isProjectileGun;
			copyToGun.isReloading = copyFromGun.isReloading;
			copyToGun.knockback = copyFromGun.knockback;
			copyToGun.lockGunToDefault = copyFromGun.lockGunToDefault;
			copyToGun.multiplySpread = copyFromGun.multiplySpread;
			copyToGun.numberOfProjectiles = copyFromGun.numberOfProjectiles;
			copyToGun.objectsToSpawn = copyFromGun.objectsToSpawn;
			copyToGun.overheatMultiplier = copyFromGun.overheatMultiplier;
			copyToGun.percentageDamage = copyFromGun.percentageDamage;
			copyToGun.player = copyFromGun.player;
			copyToGun.projectielSimulatonSpeed = copyFromGun.projectielSimulatonSpeed;
			copyToGun.projectileColor = copyFromGun.projectileColor;
			copyToGun.projectiles = copyFromGun.projectiles;
			copyToGun.projectileSize = copyFromGun.projectileSize;
			copyToGun.projectileSpeed = copyFromGun.projectileSpeed;
			copyToGun.randomBounces = copyFromGun.randomBounces;
			copyToGun.recoil = copyFromGun.recoil;
			copyToGun.recoilMuiltiplier = copyFromGun.recoilMuiltiplier;
			copyToGun.reflects = copyFromGun.reflects;
			copyToGun.reloadTime = copyFromGun.reloadTime;
			copyToGun.reloadTimeAdd = copyFromGun.reloadTimeAdd;
			copyToGun.shake = copyFromGun.shake;
			copyToGun.shakeM = copyFromGun.shakeM;
			copyToGun.ShootPojectileAction = copyFromGun.ShootPojectileAction;
			//copyToGun.shootPosition = copyFromGun.shootPosition;
			copyToGun.sinceAttack = copyFromGun.sinceAttack;
			copyToGun.size = copyFromGun.size;
			copyToGun.slow = copyFromGun.slow;
			copyToGun.smartBounce = copyFromGun.smartBounce;
			//copyToGun.soundDisableRayHitBulletSound = copyFromGun.soundDisableRayHitBulletSound;
			//copyToGun.soundGun = copyFromGun.soundGun;
			//copyToGun.soundImpactModifier = copyFromGun.soundImpactModifier;
			//copyToGun.soundShotModifier = copyFromGun.soundShotModifier;
			copyToGun.spawnSkelletonSquare = copyFromGun.spawnSkelletonSquare;
			copyToGun.speedMOnBounce = copyFromGun.speedMOnBounce;
			copyToGun.spread = copyFromGun.spread;
			copyToGun.teleport = copyFromGun.teleport;
			copyToGun.timeBetweenBullets = copyFromGun.timeBetweenBullets;
			copyToGun.timeToReachFullMovementMultiplier = copyFromGun.timeToReachFullMovementMultiplier;
			copyToGun.unblockable = copyFromGun.unblockable;
			copyToGun.useCharge = copyFromGun.useCharge;
			copyToGun.waveMovement = copyFromGun.waveMovement;

			copyToGun.GetAdditionalData().allowStop = copyFromGun.GetAdditionalData().allowStop;

			Traverse.Create(copyToGun).Field("attackAction").SetValue((Action)Traverse.Create(copyFromGun).Field("attackAction").GetValue());
			//Traverse.Create(copyToGun).Field("gunAmmo").SetValue((GunAmmo)Traverse.Create(copyFromGun).Field("gunAmmo").GetValue());
			Traverse.Create(copyToGun).Field("gunID").SetValue((int)Traverse.Create(copyFromGun).Field("gunID").GetValue());
			Traverse.Create(copyToGun).Field("spreadOfLastBullet").SetValue((float)Traverse.Create(copyFromGun).Field("spreadOfLastBullet").GetValue());

			Traverse.Create(copyToGun).Field("forceShootDir").SetValue((Vector3)Traverse.Create(copyFromGun).Field("forceShootDir").GetValue());
		}

		public void Destroy()
		{
			UnityEngine.GameObject.Destroy(this);
		}

		public static IEnumerator DestroyOnBattleEnd()
		{
			SpawnBulletsEffect[] allEffects = FindObjectsOfType<SpawnBulletsEffect>();
			foreach (SpawnBulletsEffect effect in allEffects)
			{
				effect.Destroy();
			}
			yield break;
		}

		private void UpdateID()
		{
			var allEffects = GetComponents<SpawnBulletsEffect>();
			id = 0;
			foreach (var effect in allEffects)
			{
				if (effect == this)
				{
					break;
				}
				id++;
			}
		}
	}
}
