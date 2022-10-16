using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData : ScriptableObject
{
    public string Id;

    public string itemName;

    public Sprite image;

    public string description;

    public bool consumable;

    //NOTA: esto es solo para que se pueda regenerar ciertos puntos dependiendo de si es salud o mana.
    //Lo puse asi para que sea más generico.
    public float restorationPoints;

    public bool accumulative;

    public float maxCapacity;
}
