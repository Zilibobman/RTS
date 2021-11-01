using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForwardTransformDriver : AbstractDriver<Destinations, Transform>, IForwardDriver<Destinations>
{
    protected Destinations destination;
    public override void On(Destinations destination)
    {
        this.destination = destination;
        isWork = true;
    }

    protected override void Move()
    {
        switch (destination)
        {
            case Destinations.Forward:
                ship.transform.position = Vector3.MoveTowards(ship.transform.position, maxSpeed * ship.forward + ship.transform.position, maxSpeed * Time.fixedDeltaTime);
                break;
            case Destinations.Reverse:
                ship.transform.position = Vector3.MoveTowards(ship.transform.position, maxSpeed * ship.forward, -maxSpeed * Time.fixedDeltaTime);
                break;
            default:
                break;
        }
    }
}
