using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "PlayerMovement", menuName = "Scriptable Objects/Player Movement")]
public class PlayerMoveStats : ScriptableObject
{
    public float movementSpeed;
    public float runSpeed;

    public float jumpSpeed;
}