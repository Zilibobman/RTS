using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class PilotsController : MonoBehaviour
{
    [SerializeField] protected GameObject objectWithPilots;
    [SerializeField] protected HashSet<IPilot> pilots = new HashSet<IPilot>();
    [SerializeField] protected MoovingRigidStabilizator moovingStabilizator;
    [SerializeField] protected RotateRigidStabilizator rotateStabilizator;

    void Start()
    {
        if(objectWithPilots != null)
        {
            foreach(var pilot in objectWithPilots.GetComponents<IPilot>())
            {
                pilots.Add(pilot);
            }
        }
        UpdatePilotsList();
    }
    private void UpdatePilotsList()
    {
        foreach(var pilot in pilots)
        {
            pilot.IStartNavigate += StopNavigateOtherPilots;
            pilot.IStopNavigate += Stop;
        }
    }
    private void StopNavigateOtherPilots(GameObject sender, IPilot pilotSender)
    {
        foreach(var pilot in pilots.Where(pl => pl.PilotLevel <= pilotSender.PilotLevel && pl != pilotSender))
        {
            pilot.StopNavigate();
        }
        rotateStabilizator.StopStabilization();
        moovingStabilizator.StopStabilization();

    }
    private void Stop(GameObject sender, IPilot pilotSender)
    {
        rotateStabilizator.StartStabilization();
        moovingStabilizator.StartStabilization();

    }
}
