using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shild : MonoBehaviour, IHaveHP, IHaveMaxHP, IObservableHP
{
    [SerializeField] private uint _maxHP;
    [SerializeField] private float HealDelay;
    [SerializeField] private float HealPerMinute;
    private float TimeFromLastDamage;
    private float TimeToHeal;
    public uint CurrentHP { get; private set; }
    public uint MaxHP => _maxHP;
    
    void Awake()
    {
        CurrentHP = _maxHP;
        TimeToHeal = 60 / HealPerMinute;
        StartCoroutine("Heal");
    }

    public event IObservableHP.DameginEventHandler HpChange;
    public Damage BlockDamage(Damage damage)
    {
        uint resDamageValue;
        if (damage.Value > CurrentHP)
        {
            resDamageValue = damage.Value - CurrentHP;
        }
        else
        {
            resDamageValue = 0;
        }
        CurrentHP -= (uint)Mathf.Min(damage.Value, CurrentHP);
        TimeFromLastDamage = 0;
        HpChange?.Invoke(CurrentHP, _maxHP);
        return new Damage(damage.DamageType, resDamageValue);
    }
    IEnumerator Heal()
    {
        while (true)
        {
            if (TimeFromLastDamage < HealDelay)
            {
                TimeFromLastDamage += TimeToHeal;
            }
            else if (CurrentHP < _maxHP)
            {
                CurrentHP += 1;
                HpChange?.Invoke(CurrentHP, _maxHP);
            }
            yield return new WaitForSeconds(TimeToHeal);
        }
    }
}
