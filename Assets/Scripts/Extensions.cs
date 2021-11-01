using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extensions
{

    public static Transform GetChildWithTag(this Transform tr, string tag, bool checkInactive = false)
    {
        if (tr.gameObject.CompareTag(tag))
        {
            return tr;
        }
        foreach (Transform t in tr.GetComponentsInChildren<Transform>(checkInactive))
        {
            if (t.gameObject.CompareTag(tag) == true) 
            {
                return t;
            }
        }
        return null;
    }
    public static Transform GetParentWithTag(this Transform tr, string tag)
    {
        if(tr.gameObject.CompareTag(tag))
        {
            return tr;
        }
        if (tr.parent != null)
        {
            return tr.parent.GetParentWithTag(tag);
        }
        return null;
    }
    public static bool TryFindFreePosition(this Transform tr, float PositionRadius, float SearchRadius, out Vector3 result)
    {
        bool isCheck = false;
        result = new Vector3(Random.Range(SearchRadius, -SearchRadius), 0f, Random.Range(SearchRadius, -SearchRadius));
        for(int i = 0; i < 1000; i++)
        {
            result = new Vector3(Random.Range(SearchRadius, -SearchRadius), 0f, Random.Range(SearchRadius, -SearchRadius)) + tr.position;
            Collider[] hitColliders = Physics.OverlapSphere(result, PositionRadius);
            if (hitColliders.Length == 0)
            {
                isCheck = true;
                break;
            }
        }
        return isCheck;
    }
}
