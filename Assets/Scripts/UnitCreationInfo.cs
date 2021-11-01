using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/UnitCreationInfo", order = 1)]
public class UnitCreationInfo : ScriptableObject
{
    public PriceStruct[] Price;
    public GameObject Unit;
    public float TimeForCreate;
}

[System.Serializable] 
public struct PriceStruct
{
    public ResourceTypes Resource;
    public uint Price;
}
