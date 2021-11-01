using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitLanderWithCreation : UnitLander
{
    protected override GameObject LandInPosition(Unit unit, Vector3 position, Terrain terrain)
    {
        return Object.Instantiate(unit.gameObject, position, Quaternion.Euler(Vector3.zero), terrain.transform);
    }
}
