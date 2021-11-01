using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitLander
{ 
    public void Land(Unit unit, Transform transform, Terrain terrain)
    {
        LandInPosition(unit, FindPosition(unit, transform), terrain).SetActive(true);
    }

    protected virtual Vector3 FindPosition(Unit unit, Transform transform)
    {
        float size = unit.ModelInfo.ModelObject.GetComponentInChildren<MeshFilter>().sharedMesh.bounds.size.magnitude;
        float SearchRadius = size;
        float PositionRadius = size / 2;
        Vector3 position = new Vector3();
        for (int i = 1; i < int.MaxValue; i++)
        {
            if (transform.TryFindFreePosition(PositionRadius, SearchRadius * i, out position))
            {
                break;
            }
        }
        return position;
    }

    protected virtual GameObject LandInPosition(Unit unit, Vector3 position, Terrain terrain)
    {
        unit.transform.position = position;
        unit.transform.parent = terrain.transform;
        return unit.gameObject;
    }
}
