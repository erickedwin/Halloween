using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.Events;
using Enemy.Enums;

namespace Enemy.Doors
{
    public class StandardDoorController : MonoBehaviour, IDoorController
    {
        [SerializeField]
        private Transform currentDoor;

        [SerializeField]
        private Vector3 openRotation;

        [SerializeField]
        private Vector3 closedRotation;

        [SerializeField]
        private float time = 0.4f;

        private Vector3 originalRotation;

        [SerializeField]
        private bool needsKey;

        [SerializeField]
        private KeyData keyDoor;

        [SerializeField]
        private UnityEvent OnLockedDoor;

        private DoorState currentState;

        public static event Func<KeyData, bool> OnCheckKey;

        // Start is called before the first frame update
        private void Start()
        {
            if (currentDoor == null)
            {
                currentDoor = gameObject.transform;
            }

            originalRotation = currentDoor.localRotation.eulerAngles;
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
            var hasKey = OnCheckKey.Invoke(keyDoor);
            //if(hasKey)
            if (hasKey)
            {
                //Unlocks the door. Recommended to add some effect or additional sound.
                OpenDoor();
            }
            else
            {
                //Gives some warning about the door being locked.
                OnLockedDoor.Invoke();
            }
        }

        public void CloseDoor()
        {
            //Add some sound effect if possible
            currentState = DoorState.Closed;
            currentDoor.DOLocalRotate(closedRotation, time);
        }

        public void OpenDoor()
        {
            //Add some sound effect if possible
            currentState = DoorState.Open;
            currentDoor.DOLocalRotate(openRotation, time);
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