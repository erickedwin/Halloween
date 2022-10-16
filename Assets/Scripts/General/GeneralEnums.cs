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