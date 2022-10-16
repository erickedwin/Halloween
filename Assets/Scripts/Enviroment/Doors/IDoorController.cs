using Yandere.Enums;

namespace Yandere.Doors
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