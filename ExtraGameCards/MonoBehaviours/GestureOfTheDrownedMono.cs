using UnboundLib;
using UnityEngine;

namespace EGC.MonoBehaviours
{
    internal class GestureOfTheDrownedMono : MonoBehaviour
    {
        public Block block;
        public CharacterData data;

        public void Update()
        {
            if (block.isActiveAndEnabled && (bool)data.playerVel.GetFieldValue("simulated"))
            {
                block.TryBlock();
            }
        }
    }
}