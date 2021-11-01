using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTransformDriver : AbstractDriver<Vector3, Transform>, IRotateDriver<Vector3>
{
    protected Vector3 axis;
    public override void On(Vector3 axis)
    {
        this.axis = axis.normalized;
        isWork = true;
    }
    protected override void Move()
    {
        Vector3 targetDirection = Vector3.Cross(axis, ship.forward).normalized;
        Vector3 newDirection = Vector3.RotateTowards(ship.forward, targetDirection, maxSpeed * Time.fixedDeltaTime, 0.0f);
        //Vector3 newDirection = Vector3.RotateTowards(ship.position + ship.forward, ship.position + Direction, maxSpeed * Time.fixedDeltaTime, 0.0f);
        // Рисуем луч, указывающий на нашу цель в
        Debug.DrawRay(ship.position, newDirection, Color.red);
        ship.rotation = Quaternion.LookRotation(newDirection);
    }
}
