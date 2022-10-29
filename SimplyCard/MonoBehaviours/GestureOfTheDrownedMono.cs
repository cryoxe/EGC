using UnityEngine;
using ModdingUtils.MonoBehaviours;
using UnboundLib.Cards;
using UnboundLib.GameModes;
using UnboundLib;
using ModdingUtils.Extensions;
using static ModdingUtils.Utils.Cards;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using SimplyCard.Cards;

namespace ExtraGameCards.MonoBehaviours
{
    internal class GestureOfTheDrownedMono : MonoBehaviour
    {
        public Block block;

        public void Update()
        {
            if (block.isActiveAndEnabled)
            {
                block.TryBlock();
            }
        }
    }
}
