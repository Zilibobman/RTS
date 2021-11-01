using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookingAtDriverAndStabilizatorController : MonoBehaviour, ILookAtController<Vector3>
{
    [SerializeField] protected AbstractStabilizator stopStabilizator;
    [SerializeField] protected AbstractDriver<Vector3> RotationDriver;
    protected IStabilizatorByDestination<Vector3> DirectionRotationStab;
    [SerializeField] protected AbstractStabilizator RotationDirectionStab;
    [SerializeField] protected Transform ship;
    [SerializeField] protected int precision = 3;
    protected Vector3 destination;
    protected bool isWork;

    public bool DriverWork => RotationDriver.IsWork;

    public bool StabilizatorWork
    {
        get
        {
            if (stopStabilizator != null)
                return stopStabilizator.IsWork;
            else
                return false;
        }
    }
    private void Awake()
    {
        if (ship == null)
            ship = gameObject.transform.GetParentWithTag("Ship").GetComponent<Transform>();
    }
    protected virtual void Start()
    {
        DirectionRotationStab = RotationDirectionStab as IStabilizatorByDestination<Vector3>;
    }
    public virtual void MoveTo(Vector3 destination)
    {
        this.destination = destination;
        isWork = true;
    }

    public virtual void StopMooving()
    {
        RotationDriver.Off();
        stopStabilizator?.StopStabilization();
        DirectionRotationStab?.StopStabilization();
        isWork = false;
    }
    protected virtual void Update()
    {
        if(isWork)
        {
            LookAt();
        }
    }
    protected virtual void LookAt()
    {
        if(Vector3.Angle(ship.forward, destination - ship.position) < precision)
        {
            DirectionRotationStab?.StopStabilization();
            stopStabilizator?.StartStabilization();
            RotationDriver.Off();
        }
        else
        {
            Vector3 Axis = Vector3.Cross(ship.forward, destination - ship.transform.position);
            stopStabilizator?.StopStabilization();
            RotationDriver.On(Axis);
            if (DirectionRotationStab != null)
            {
                DirectionRotationStab.Destination = Axis;
                DirectionRotationStab.StartStabilization();
            }
        }
    }
}
