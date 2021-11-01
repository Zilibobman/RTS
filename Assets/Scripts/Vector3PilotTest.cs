using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vector3PilotTest : MonoBehaviour
{
    public GoToVector3PointPilot pilot;
    public Transform destination;
    public void Go()
    {
        pilot.destination = destination.position;
        pilot.StartNavigate();
    }
}
