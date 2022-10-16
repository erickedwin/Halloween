using UnityEngine;

[CreateAssetMenu(fileName = "Note Base", menuName = "Scriptable Objects/Note")]
[System.Serializable]
public class NoteData : ScriptableObject
{
    //May or may not be a redundant field
    [SerializeField]
    public string noteName;

    [SerializeField]
    public string noteTitle;

    [SerializeField]
    [TextArea(20, 30)]
    public string description;

    [SerializeField]
    public Sprite icon;
}
