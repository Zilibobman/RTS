using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForwardMoovingDriverAndStabilizatorController : MonoBehaviour, IMoovingController<Destinations>
{
    [SerializeField] protected AbstractStabilizator DirectionMoovingStabilizator;
    [SerializeField] protected AbstractDriver<Destinations> ForwardDriver;

    public bool DriverWork => ForwardDriver.IsWork;

    public bool StabilizatorWork
    {
        get
        {
            if (DirectionMoovingStabilizator != null)
                return DirectionMoovingStabilizator.IsWork;
            else
                return false;
        }
    }

    public virtual void MoveTo(Destinations destination)
    {
        DirectionMoovingStabilizator?.StartStabilization();
        ForwardDriver.On(destination);
    }

    public virtual void StopMooving()
    {
        ForwardDriver.Off();
        DirectionMoovingStabilizator?.StopStabilization();
    }
}