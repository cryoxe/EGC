using EGC.Extensions.SpawnBullet;
using EGC.Utils;
using UnityEngine;

namespace EGC.MonoBehaviours
{
    public class BulletThatShootGunsMono : MonoBehaviour
    {
        public Player Player { get; set; } = null!;
        public Gun Gun { get; set; } = null!;


        private float remainingTimeBeforeNextShot;

        private const float RotationSpeed = 90f;

        private void Start()
        {
            remainingTimeBeforeNextShot = Random.Range(0.05f, 0.2f);
        }

        private void Update()
        {
            if (remainingTimeBeforeNextShot > 0)
            {
                remainingTimeBeforeNextShot -= Time.deltaTime;
            }
            else
            {
                ShootARandomBulletEffect(Player, Gun, transform.position);

                remainingTimeBeforeNextShot = 0.15f;
            }

            transform.Rotate(Vector3.forward, RotationSpeed * Time.deltaTime);

        }

        public void ShootARandomBulletEffect(Player player, Gun gun, Vector3 position)
        {
            Gun newGun = gameObject.AddComponent<RandomBullet>();

            SpawnBulletsEffect effect = player.gameObject.AddComponent<SpawnBulletsEffect>();
            effect.SetDirection(new Vector3(1f, 1f, 1f));
            effect.SetPosition(position);
            effect.SetNumBullets(1);
            effect.SetTimeBetweenShots(0f);
            effect.SetInitialDelay(0.01f);

            SpawnBulletsEffect.CopyGunStats(gun, newGun);

            newGun.damage = 0.7f;
            newGun.projectileSize = 0.6f;
            newGun.projectileSpeed = 1.1f;
            newGun.spread = 1f;
            newGun.numberOfProjectiles = 1;
            newGun.destroyBulletAfter = 6f;
            gun.gravity = 1f;
            gun.recoil = 1f;
            gun.knockback = 1f;
            gun.projectileColor = Color.yellow;
            newGun.objectsToSpawn = new[] { PreventRecursion.StopRecursionObjectToSpawn };

            effect.SetGun(newGun);
        }

        public class RandomBullet : Gun
        {
        }

        public class PreventGunSpriteRecursion : ObjectsToSpawn { }
    }
}