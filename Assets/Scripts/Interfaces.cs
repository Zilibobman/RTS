using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

#region HP and Damage
public interface IDamagable
{
    public void GetDamage(Damage damage);
}
public interface IHaveHP
{
    public uint CurrentHP { get; }
}
public interface IHaveMaxHP
{
    public uint MaxHP { get; }
}
public interface IHeallable
{
    public void Heal(int HP);
}
public interface IDamager
{
    public void Hurt(IDamagable victim);
}
public interface IObservableHP : IHaveHP, IHaveMaxHP
{
    public delegate void DameginEventHandler(uint CurrentHealth, uint MaxHealth);
    public event DameginEventHandler HpChange;
}
#endregion

#region Speed Mooving and Driver
public interface IHavePower
{
    public float Power { get; set; }
}
public interface IHaveSpeed
{
    public float Speed { get; set; }
}
public interface IHaveMaxSpeed
{
    public float MaxSpeed { get; set; }
}
public interface IDriver : IHaveMaxSpeed, ICanWork
{
    public void Off();
}
public interface IDriver<directionType> : IDriver
{
    public void On(directionType destination);
}
public interface IRotateDriver<directionType> : IDriver<directionType> { }
public interface IForwardDriver<directionType> : IDriver<directionType> { }

public interface IMoovingByTrjectory<TrjectoryType>
{
    public void GoTo(TrjectoryType Trjectory);
}
#endregion

#region Stabilizator
public interface IStabilizator : IHaveSpeed, ICanWork
{
    public void StartStabilization();
    public void StopStabilization();
}
public interface IRotateStabilizator : IStabilizator { }
public interface IDirectionStabilizator : IStabilizator { }
public interface IMoovingStabilizator : IStabilizator { }

public interface IStabilizatorByDestination<DestinationType> : IStabilizator 
{
    public DestinationType Destination { get; set; }

}

#endregion

#region Driver and Stabilizator Controller
public interface IDriverAndStabilizatorController
{
    public void StopMooving();
    public bool DriverWork { get; }
    public bool StabilizatorWork { get; }
}
public interface IDriverAndStabilizatorController<DriverInput> : IDriverAndStabilizatorController
{
    public void MoveTo(DriverInput destination);
}
public interface ILookAtController<DriverInput> : IDriverAndStabilizatorController<DriverInput> { }
public interface IMoovingController<DriverInput> : IDriverAndStabilizatorController<DriverInput> { }

#endregion

#region Conflict Side
public interface IHaveConflictSide
{
    public ConflictSides ConflictSide { get; }
}
public interface IHaveConflictSideAndDamageble : IHaveConflictSide, IDamagable { }
#endregion

#region Shield
public interface IShield : IDamagable, IHaveHP, IHaveMaxHP, IHeallable, IHaveConflictSideAndDamageble
{

}
#endregion

#region Guns and Bullets
public interface IHaveDamage
{
    public int Damage { get; set; }
}
public interface IHaveAim
{
    public Transform AimObj { get; set; }
}
public interface IBullet : IHaveSpeed, IDamager, IHaveConflictSide, IHaveDamage { }
public interface IBulletWithAim : IBullet, IHaveAim { }
public interface IGun
{
    public IMakeAShot Shoter { get; set; }
    public List<IBulletModifyer> Modifiers { get; set; }
    public ParticleSystem Particle { get; set; }
}
public interface IAutoGun : IGun
{
    public float Time_Bullet_Spawn { get; set; }
}
public interface IBulletModifyer
{
    public void Modify(IBullet bullet);
}
public interface IMakeAShot
{
    public void MakeAShot(GameObject Bullet);
}
#endregion

#region Pilots
public interface IPilot : ICanWork
{
    public PilotLevels PilotLevel { get; }
    public delegate void EventPilotHandler(GameObject sender, IPilot pilot);
    public void StartNavigate();
    public event EventPilotHandler IStartNavigate;
    public void StopNavigate();
    public event EventPilotHandler IStopNavigate;

}
public interface IPilotByTrajectory : IPilot
{
    public delegate void EventEndingTrajectoryHandler(GameObject sender, bool is_return);
    public bool Is_return { get; set; }
    public event EventEndingTrajectoryHandler IEndTrajectory;
}
public interface IPilotByTrajectory<TrajectoryType> : IPilotByTrajectory
{
    public TrajectoryType Trajectory { get; set; }
}
#endregion

#region Ships
public interface IShip : IHaveHP, IHaveMaxHP, IHaveConflictSideAndDamageble
{
    public IShield Shield { get; }
    public IDriver Driver { get; }
    public event EventHandler ShipWasDestroy;
}
#endregion

public interface ICanWork
{
    public bool IsWork { get; }
}

public interface ICameraController
{
    public void ChangeSettings(CameraSettings cameraSettings);
    public delegate void EventChangeSettingsHandler(ICameraController sender, CameraSettings oldSettings, CameraSettings newSettings);
    public event EventChangeSettingsHandler IChangeSettings;
}

public interface IResourceGetter
{
    ResourceTypes ResourceType { get; }
    public bool CanProduce { get; }
    public ResourceContainer GetResources();
}

