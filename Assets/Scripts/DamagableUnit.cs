using System;
using UnityEngine;

public class DamagableUnit : MonoBehaviour, IHaveConflictSideAndDamageble, IObservableHP
{
    [SerializeField] private ConflictSides _conflictSide;
    [SerializeField] private uint _maxHP;
    [SerializeField] private Shild _shild;
    public float DamageForceThreshold = 1f;
    public float DamageForceScale = 5f;


    public uint CurrentHP { get; private set; }
    public uint MaxHP => _maxHP;

    public event IObservableHP.DameginEventHandler HpChange;

    public event EventHandler UnitWasDestroy;

    public ConflictSides ConflictSide => _conflictSide;

    void Awake()
    {
        CurrentHP = _maxHP;
    }

    private void OnCollisionEnter(Collision other)
    {
        // Collision would usually be on another component, putting it all here for simplicity
        float force = other.relativeVelocity.magnitude;
        if (force > DamageForceThreshold)
        {
            GetDamage(new Damage(DamageTypes.collision, (uint)((force - DamageForceThreshold) * DamageForceScale)));
        }
    }
    public void GetDamage(Damage damage)
    {
        if (_shild != null)
        {
            damage = _shild.BlockDamage(damage);
        }
        CurrentHP -= (uint)Mathf.Min(damage.Value, CurrentHP);
        HpChange?.Invoke(CurrentHP, _maxHP);
        if (CurrentHP == 0)
        {
            UnitWasDestroy?.Invoke(this, new EventArgs());
            this.gameObject.SetActive(false);
        }
    }
}

public struct Damage
{
    private DamageTypes _damageType;
    public DamageTypes DamageType => _damageType;
    private uint _value;
    public uint Value => _value;
    public Damage(DamageTypes damageType, uint value)
    {
        _damageType = damageType;
        _value = value;
    }
}
