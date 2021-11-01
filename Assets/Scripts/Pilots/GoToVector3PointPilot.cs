using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToVector3PointPilot : AbstructPilot
{
    protected Transform ship;
    [SerializeField] protected float precision = 0.3f;
    protected const PilotLevels pilotLevel = PilotLevels.PointLevel;
    public override PilotLevels PilotLevel => pilotLevel;
    protected IMoovingController<Destinations> forwardController;
    protected ILookAtController<Vector3> LookingAtController;
    public Vector3 destination = Vector3.zero;
    public override void StopNavigate()
    {
        forwardController.StopMooving();
        LookingAtController.StopMooving();
        base.StopNavigate();
    }
    private void Awake()
    {
        if (ship == null)
            ship = gameObject.transform.GetParentWithTag("Ship").GetComponent<Transform>();
    }
    protected virtual void Start()
    {
        forwardController = ship.GetComponentInChildren<IMoovingController<Destinations>>();
        LookingAtController = ship.GetComponentInChildren<ILookAtController<Vector3>>();
    }
    protected override void Navigation()
    {
        if (Vector3.Distance(ship.position, destination) > precision)
        {
            if(!forwardController.DriverWork && !forwardController.StabilizatorWork)
            {
                forwardController.MoveTo(Destinations.Forward);
            }
            if (!LookingAtController.DriverWork && !LookingAtController.StabilizatorWork)
            {
                LookingAtController.MoveTo(destination);
            }
        }
        else
        {
            StopNavigate();
        }
    }
}
