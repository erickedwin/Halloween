using Lean.Pool;
using System.Collections.Generic;
using UnityEngine;

namespace Nocturne.GeneralTools
{
    public static class Helpers
    {
        private static Camera mainCam;

        //Cache the current main camera.
        public static Camera camera
        {
            get
            {
                if (mainCam == null)
                {
                    mainCam = Camera.main;
                }
                return mainCam;
            }
        }

        private static GameObject _player;

        //Cache the current player.
        public static GameObject player
        {
            get
            {
                if (_player == null)
                {
                    _player = GameObject.FindGameObjectWithTag("Player");
                }

                return _player;
            }
        }

        private static readonly Dictionary<float, WaitForSeconds> WaitDictionary = new Dictionary<float, WaitForSeconds>();

        //Non GC-Allocating version of WaitForSeconds.
        public static WaitForSeconds GetWait(float time)
        {
            if (WaitDictionary.TryGetValue(time, out var wait)) return wait;

            WaitDictionary[time] = new WaitForSeconds(time);

            return WaitDictionary[time];
        }

        private static readonly Dictionary<float, WaitForSecondsRealtime> WaitDictionaryRealtime = new Dictionary<float, WaitForSecondsRealtime>();

        //Non GC-Allocating version of WaitForSecondsRealtime.
        public static WaitForSecondsRealtime GetWaitRealtime(float time)
        {
            if (WaitDictionaryRealtime.TryGetValue(time, out var wait)) return wait;

            WaitDictionaryRealtime[time] = new WaitForSecondsRealtime(time);

            return WaitDictionaryRealtime[time];
        }

        //Cache the WaitForEndOfFrame;
        private static readonly WaitForEndOfFrame WaitEndDictionary = new WaitForEndOfFrame();

        public static WaitForEndOfFrame GetEnd()
        {
            return WaitEndDictionary;
        }

        //Destroys the children of a specific entity.
        //...don't take this out of context, please.
        public static void DestroyAllChildren(this Transform t)
        {
            foreach (Transform child in t)
            {
                //Replace with Destroy(child.gameObject) if you don't use Lean Pool (which you should).
                LeanPool.Despawn(child.gameObject);
            }
        }
    }
}