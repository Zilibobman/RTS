using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��������� ����� ��������� �������
/// </summary>
public class RotateRigidStabilizator : AbstractStabilizator<Rigidbody>, IRotateStabilizator
{
    protected override void Stabilizate()
    {
        ship.angularVelocity = ship.angularVelocity * Mathf.Clamp01(1f - speed * Time.fixedDeltaTime);
    }
}
