using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using Enemy.Enums;

namespace Enemy.Doors
{
    public class SlideDoorController : MonoBehaviour, IDoorController
    {
        [SerializeField]
        private Transform currentDoor;

        [SerializeField]
        private Vector3 openPosition;

        [SerializeField]
        private Vector3 closedPosition;

        private Vector3 originalPosition;

        [SerializeField]
        private bool needsKey;

        [SerializeField]
        private KeyData keyDoor;

        [SerializeField]
        private UnityEvent OnLockedDoor;

        private DoorState currentState;

        // Start is called before the first frame update
        private void Start()
        {
            if (currentDoor == null)
            {
                currentDoor = gameObject.transform;
            }

            originalPosition = currentDoor.localPosition;
            if (needsKey)
            {
                currentState = DoorState.Locked;
                if (keyDoor == null)
                {
                    Debug.LogError("Esta puerta necesita una llave");
                }
            }
            else
            {
                currentState = DoorState.Closed;
            }
        }

        public void CheckKey()
        {
            //var hasKey = keyManager.instance.CheckIfKeySaved(keyDoor);
            //if(hasKey)
            if (true)
            {
                //Unlocks the door. Recommended to add some effect or additional sound.
                OpenDoor();
            }
            else
            {
                //Gives some warning about the door being locked.
                //OnLockedDoor.Invoke();
            }
        }

        public void CloseDoor()
        {
            //Add some sound effect if possible
            currentState = DoorState.Closed;
            currentDoor.DOLocalMove(closedPosition, 0.4f);
        }

        public void OpenDoor()
        {
            //Add some sound effect if possible
            currentState = DoorState.Open;
            currentDoor.DOLocalMove(openPosition, 0.4f);
        }

        public void SwitchDoorState()
        {
            switch (currentState)
            {
                case DoorState.Locked:
                    CheckKey();
                    break;

                case DoorState.Open:
                    CloseDoor();
                    break;

                default:
                    OpenDoor();
                    break;
            }
        }

        public DoorState GetCurrentState()
        {
            return currentState;
        }
    }
}