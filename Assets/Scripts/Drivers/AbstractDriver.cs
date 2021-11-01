using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractDriver<DriverInput, ShipType> : AbstractDriver<DriverInput>
{
    protected ShipType ship;
    private void Awake()
    {
        if (ship == null)
            ship = gameObject.transform.GetParentWithTag("Ship").GetComponent<ShipType>();
    }
}
public abstract class AbstractDriver<DriverInput> : AbstractDriver, IDriver<DriverInput>
{
    public abstract void On(DriverInput destination);
}
public abstract class AbstractDriver : MonoBehaviour, IDriver
{
    [SerializeField] protected float maxSpeed = float.MaxValue;
    public float MaxSpeed
    {
        get => maxSpeed;
        set => maxSpeed = value;
    }
    protected bool isWork = false;
    public bool IsWork { get => isWork; }

    // Update is called once per frame
    protected virtual void FixedUpdate()
    {
        if (isWork)
            Move();
    }
    public virtual void Off()
    {
        isWork = false;
    }
    protected abstract void Move();
}
