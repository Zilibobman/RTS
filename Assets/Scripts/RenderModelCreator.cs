using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderModelCreator
{
    public GameObject GetFirstModelFromList(IEnumerable<GameObject> objects)
    {
        foreach(var obj in objects)
        {
            var model = obj.transform.GetChildWithTag("Model");
            if(model != null)
            {
                return model.gameObject;
            }
        }
        return new GameObject();
    }
}
