using UnityEngine;

namespace EGC.Utils
{
    //from PCE
    public static class PreventRecursion
    {
        private static GameObject? _stopRecursion;

        internal static GameObject StopRecursion
        {
            get
            {
                if (_stopRecursion != null)
                {
                    return _stopRecursion;
                }

                _stopRecursion = new GameObject("StopRecursion", typeof(StopRecursion),
                    typeof(DestroyOnUnparentAfterInitialized));
                Object.DontDestroyOnLoad(_stopRecursion);

                return _stopRecursion;
            }
        }

        internal static ObjectsToSpawn StopRecursionObjectToSpawn
        {
            get
            {
                ObjectsToSpawn obj = new ObjectsToSpawn
                {
                    AddToProjectile = StopRecursion
                };

                return obj;
            }
        }
    }

    public class DestroyOnUnparentAfterInitialized : MonoBehaviour
    {
        private static bool _initialized = false;
        private bool isOriginal;

        private void Start()
        {
            if (!_initialized)
            {
                isOriginal = true;
            }
        }

        private void LateUpdate()
        {
            if (isOriginal)
            {
                return;
            }

            if (gameObject.transform.parent == null)
            {
                Destroy(gameObject);
            }
        }
    }
}