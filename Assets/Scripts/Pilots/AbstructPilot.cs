using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstructPilot : MonoBehaviour, IPilot
{
    public abstract PilotLevels PilotLevel { get; }
    protected bool isWork;
    public bool IsWork { get; }

    public event IPilot.EventPilotHandler IStartNavigate;
    public event IPilot.EventPilotHandler IStopNavigate;

    protected void Update()
    {
        if (isWork)
        {
            Navigation();
        }
    }
    public virtual void StartNavigate()
    {
        IStartNavigate?.Invoke(gameObject, this);
        isWork = true;
    }

    public virtual void StopNavigate()
    {
        IStopNavigate?.Invoke(gameObject, this);
        isWork = false;
    }

    protected abstract void Navigation();
}
