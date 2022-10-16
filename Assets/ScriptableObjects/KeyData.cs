using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Key", menuName = "Scriptable Objects/Key")]
[System.Serializable]
public class KeyData : ScriptableObject
{
    public string keyName;

    [TextArea(20,30)]
    public string description;
}
