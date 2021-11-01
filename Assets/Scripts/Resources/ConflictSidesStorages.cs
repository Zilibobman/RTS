using System;
using System.Collections.Generic;
using UnityEngine;

public class ConflictSidesStorages : MonoBehaviour
{
    public static ConflictSidesStorages instance;
    private Dictionary<ConflictSides, ConflictSideStorage> _storages = new Dictionary<ConflictSides, ConflictSideStorage>();
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            foreach (var conSide in Enum.GetValues(typeof(ConflictSides)))
            {
                ConflictSides conflictSide = (ConflictSides)conSide;
                _storages[conflictSide] = new ConflictSideStorage(conflictSide);
            }
        }
        else
        {
            GameObject.Destroy(this.gameObject);
        }
    }
    public ConflictSideStorage GetConflictSideStorage(ConflictSides conflictSide)
    {
        return _storages[conflictSide];
    }
}
