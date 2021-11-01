using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForwardRigidDriver : AbstractDriver<Destinations, Rigidbody>, IForwardDriver<Destinations>, IHavePower
{
    protected Destinations destination;
    [SerializeField] protected float power = 1;
    public float Power { get => power; set => power = value; }
    public override void On(Destinations destination)
    {
        this.destination = destination;
        isWork = true;
    }

    protected override void Move()
    {
        Vector3 Velocity = ship.velocity * Mathf.Cos(Mathf.Deg2Rad * Vector3.Angle(ship.transform.forward, ship.velocity));
        Vector3 Force = ship.transform.forward * maxSpeed - Velocity;
        switch (destination)
        {
            case Destinations.Forward:
                ship.AddForce(Force * power * Time.fixedDeltaTime * ship.mass, ForceMode.Impulse);
                break;
            case Destinations.Reverse:
                ship.AddForce(Force * power * Time.fixedDeltaTime * ship.mass * -1, ForceMode.Impulse);
                break;
            default:
                break;
        }
    }
}
public enum Destinations
{
    Forward,
    Reverse
}
