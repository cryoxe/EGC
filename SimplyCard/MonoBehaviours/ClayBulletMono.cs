using ExtraGameCards;
using UnityEngine;
using ModdingUtils.MonoBehaviours;
using UnboundLib.Cards;
using UnboundLib.GameModes;
using UnboundLib;
using ModdingUtils.Extensions;
using static ModdingUtils.Utils.Cards;
using CardChoiceSpawnUniqueCardPatch.CustomCategories;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using ExtraGameCards.Cards;

namespace ExtraGameCards.MonoBehaviours
{
    internal class ClayBulletMono : MonoBehaviour
    {
        public Player? player = null;
        public int? maxBomb = null;
        public ClayEffect? clayBullet = null;

        public void Awake()
        {
            ProjectileHit hit = gameObject.GetComponent<ProjectileHit>();
            hit.AddHitAction(OnHit);
        }

        private void OnHit()
        {
            maxBomb = Extensions.CharacterStatModifiersExtension.GetAdditionalData(player.data.stats).bombs;
            if ( clayBullet.bombPositions.Count < maxBomb )
            {
                UnityEngine.Debug.Log($"Adding new position : {transform.position}");
                clayBullet.bombPositions.Add(transform.position);
            }
            else
            {
                UnityEngine.Debug.Log($"Removing first position...");
                UnityEngine.Debug.Log($"Adding new position : {transform.position}");
                clayBullet.bombPositions.Remove(clayBullet.bombPositions[0]);
                clayBullet.bombPositions.Add(transform.position);
            }
            UnityEngine.Debug.Log($"{clayBullet.bombPositions.Count} bomb(s) placed.");
        }
        
    }
}
