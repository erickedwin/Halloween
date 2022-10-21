using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "PlayerMovement", menuName = "Scriptable Objects/Player Movement")]
public class PlayerMoveStats : ScriptableObject
{
    public float movementSpeed = 5f;
    public float runSpeed = 9f;
    public float crouchSpeed = 2.5f;
    public float jumpSpeed;
}