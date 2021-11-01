using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateRigidDriver : AbstractDriver<Vector3,Rigidbody>, IRotateDriver<Vector3>, IHavePower
{
    protected Vector3 axis;
    [SerializeField] protected float power = 1;
    public float Power { get => power; set => power = value; }
    public override void On(Vector3 axis)
    {
        this.axis = axis;
        isWork = true;
    }

    protected override void Move()
    {
        Vector3 Velocity = ship.angularVelocity;
        float speed = maxSpeed - Velocity.magnitude;
        ship.AddTorque(Vector3.Normalize(axis) * power * speed * Time.fixedDeltaTime * ship.mass, ForceMode.Impulse);
    }
}
