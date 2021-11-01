using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionRotationRigidStabilizator : AbstractStabilizator<Rigidbody>, IRotateStabilizator, IMoovingStabilizator, IStabilizatorByDestination<Vector3>
{
    protected Vector3 needAxes;
    public Vector3 Destination { get => needAxes; set => needAxes = value; }

    protected override void Stabilizate()
    {
        Vector3 crossUp = Vector3.Cross(needAxes, ship.angularVelocity);
        Vector3 crossSide = Vector3.zero;
        if (crossUp != Vector3.zero)
        {
            crossSide = Vector3.Cross(needAxes, crossUp);
        }
        if (crossSide != Vector3.zero && crossSide.magnitude > 0.001)
        {
            //ship.velocity = ship.velocity + crossSide * Mathf.Clamp01(speed * Time.fixedDeltaTime);
            ship.AddTorque(Vector3.Normalize(crossSide) * speed * Time.fixedDeltaTime * ship.mass, ForceMode.Impulse);
        }
        //LastPos = ship.transform.position;
        //ship.angularVelocity = ship.angularVelocity + ((needAxes - ship.angularVelocity)* Mathf.Clamp01(speed * Time.fixedDeltaTime));
    }

}
