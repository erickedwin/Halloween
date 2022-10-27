using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using Enemy.Enums;

namespace Enemy.Doors
{
    public class DoubleDoorsController : MonoBehaviour, IDoorController
    {
        [SerializeField]
        private Transform leftDoor;

        [SerializeField]
        private Transform rightDoor;

        [SerializeField]
        private Vector3 leftOpenRotation;

        [SerializeField]
        private Vector3 leftClosedRotation;

        [SerializeField]
        private Vector3 rightOpenRotation;

        [SerializeField]
        private Vector3 rightClosedRotation;

        [SerializeField]
        private bool needsKey;

        [SerializeField]
        private KeyData keyDoor;

        [SerializeField]
        private UnityEvent OnLockedDoor;

        private DoorState currentState;

        // TODO: modify the behaviour so the doors can be opened individually (if it's needed).
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
            leftDoor.DOLocalRotate(leftClosedRotation, 0.4f);
            rightDoor.DOLocalRotate(rightClosedRotation, 0.4f);
        }

        public void OpenDoor()
        {
            //Add some sound effect if possible
            currentState = DoorState.Open;
            leftDoor.DOLocalRotate(leftOpenRotation, 0.4f);
            rightDoor.DOLocalRotate(rightOpenRotation, 0.4f);
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