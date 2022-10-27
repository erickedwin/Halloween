using Enemy.Enums;

namespace Enemy.Doors
{
    public interface IDoorController
    {
        void SwitchDoorState();

        void CheckKey();

        void OpenDoor();

        void CloseDoor();

        DoorState GetCurrentState();
    }
}