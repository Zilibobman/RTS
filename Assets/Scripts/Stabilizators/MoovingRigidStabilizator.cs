using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoovingRigidStabilizator : AbstractStabilizator<Rigidbody>, IMoovingStabilizator
{
    protected override void Stabilizate()
    {
        ship.velocity = ship.velocity * Mathf.Clamp01(1f - speed * Time.fixedDeltaTime);
    }
}
