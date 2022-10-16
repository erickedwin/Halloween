using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Nocturne.GeneralTools
{
    public class StandardDelay : MonoBehaviour
    {
        // This is a standard sequence for any purpose.

        [SerializeField]
        private float delayTime;

        [SerializeField]
        private UnityEvent OnEndDelay;

        public void StartDelay() => StartCoroutine(Delay());

        public void StartCustomDelay(float custom) => StartCoroutine(Delay(custom));

        public void StartDelayRealtime() => StartCoroutine(DelayRealtime());

        public void StartDelayRealtime(float custom) => StartCoroutine(DelayRealtime(custom));

        private IEnumerator Delay()
        {
            yield return Helpers.GetWait(delayTime);
            OnEndDelay.Invoke();
        }

        private IEnumerator Delay(float customTime)
        {
            yield return Helpers.GetWait(customTime);
            OnEndDelay.Invoke();
        }

        private IEnumerator DelayRealtime()
        {
            yield return Helpers.GetWaitRealtime(delayTime);
            OnEndDelay.Invoke();
        }

        private IEnumerator DelayRealtime(float customTime)
        {
            yield return Helpers.GetWaitRealtime(customTime);
            OnEndDelay.Invoke();
        }
    }
}