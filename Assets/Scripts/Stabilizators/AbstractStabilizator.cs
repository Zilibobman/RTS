using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractStabilizator<ShipType> : AbstractStabilizator
{
    protected ShipType ship;
    private void Awake()
    {
        if (ship == null)
            ship = gameObject.transform.GetParentWithTag("Ship").GetComponent<ShipType>();
    }
}
public abstract class AbstractStabilizator : MonoBehaviour, IStabilizator
{
    [SerializeField] protected float speed = 0;
    public float Speed { get => speed; set => speed = value; }
    protected bool isWork = false;
    public bool IsWork { get => isWork; }
    public virtual void StartStabilization()
    {
        isWork = true;
    }

    public virtual void StopStabilization()
    {
        isWork = false;
    }

    // Update is called once per frame
    protected void FixedUpdate()
    {
        if (isWork)
        {
            Stabilizate();
        }
    }
    protected abstract void Stabilizate();
}
