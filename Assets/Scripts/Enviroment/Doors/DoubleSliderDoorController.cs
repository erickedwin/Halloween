using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using Yandere.Enums;

namespace Yandere.Doors
{
    public class DoubleSliderDoorController : MonoBehaviour, IDoorController
    {
        [SerializeField]
        private Transform leftDoor;

        [SerializeField]
        private Transform rightDoor;

        [SerializeField]
        private Vector3 leftOpenPosition;

        [SerializeField]
        private Vector3 leftClosedPosition;

        [SerializeField]
        private Vector3 rightOpenPosition;

        [SerializeField]
        private Vector3 rightClosedPosition;

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
            if (leftDoor == null)
            {
                Debug.LogError("Debe asignar una puerta izquierda");
            }

            if (rightDoor == null)
            {
                Debug.LogError("Debe asignar una puerta derecha");
            }

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
            leftDoor.DOLocalMove(leftClosedPosition, 0.4f);
            rightDoor.DOLocalMove(rightClosedPosition, 0.4f);
        }

        public DoorState GetCurrentState()
        {
            return currentState;
        }

        public void OpenDoor()
        {
            //Add some sound effect if possible
            currentState = DoorState.Open;
            leftDoor.DOLocalMove(leftOpenPosition, 0.4f);
            rightDoor.DOLocalMove(rightOpenPosition, 0.4f);
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
    }
}