namespace Nocturne.Enums
{
    public enum PlayerStatus
    {
        Idle,
        Walking,
        Sprinting,
        Crouching
    }
    public enum DoorStatus
    {
        Open,
        Closed,
        Locked
    }

    public enum ObjectStatus
    {
        Idle,
        Used
    }

    public enum PlayerAnimatorLayers
    {
        Walk,
        Idle
    }

    public enum AttributeType
    {
        Strength,
        Intelligence,
        Dexterity
    }

    public enum ItemType
    {
        Healable,
        ManaGenerator,
        Standard
    }

    public static class PlayerLayersExtensions
    {
        public static string GetLayer(this PlayerAnimatorLayers layer)
        {
            switch (layer)
            {
                case PlayerAnimatorLayers.Walk: return "Walk";
                default: return "Idle";
            }
        }
    }
}

