using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Заглушает передвижение тела не в направлении взгляда
/// </summary>
public class DirectionMoovingRigidStabilizator : AbstractStabilizator<Rigidbody>, IDirectionStabilizator, IMoovingStabilizator
{
    protected Vector3 LastPos;
    public override void StartStabilization()
    {
        LastPos = ship.transform.position;
        base.StartStabilization();
    }

    protected override void Stabilizate()
    {
        Vector3 direction = ship.transform.position - LastPos;
        Vector3 crossUp = Vector3.Cross(ship.transform.forward, direction);
        Vector3 crossSide = Vector3.zero;
        if (crossUp != Vector3.zero)
        {
            crossSide = Vector3.Cross(ship.transform.forward, crossUp);
        }
        if (crossSide != Vector3.zero && crossSide.magnitude > 0.001)
        {
            //ship.velocity = ship.velocity + crossSide * Mathf.Clamp01(speed * Time.fixedDeltaTime);
            ship.AddForce(Vector3.Normalize(crossSide) * speed * Time.fixedDeltaTime * ship.mass, ForceMode.Impulse);
        }
        LastPos = ship.transform.position;
    }
}
