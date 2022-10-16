using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Nocturne.GeneralTools
{
    public class StartingDelay : MonoBehaviour
    {
        // This is a standard sequence for any purpose.

        [SerializeField]
        private float delayTime;

        [SerializeField]
        private UnityEvent OnEndDelay;

        // Start is called before the first frame update
        private IEnumerator Start()
        {
            yield return Helpers.GetWait(delayTime);
            OnEndDelay.Invoke();
        }
    }
}