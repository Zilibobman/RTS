using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransporterTest : MonoBehaviour
{
    public List<GameObject> Ships;
    public GameObject render;
    public GameObject planet;

    public void Go()
    {
        if(Ships[0].GetComponentsInParent<Boarder>() != null)
        {
            Ships[0].GetComponentInParent<Boarder>().Board(planet);
        }
    }
}
