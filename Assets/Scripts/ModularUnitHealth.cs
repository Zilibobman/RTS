using System;
using UnityEngine;

public class ModularUnitHealth : MonoBehaviour, IObservableHP
{
    private uint _maxHP;
    public uint MaxHP => _maxHP;
    private uint _curentHP;
    public uint CurrentHP => _curentHP;

    public event IObservableHP.DameginEventHandler HpChange;

    private void Awake()
    {
        foreach(var module in GetComponentsInChildren<DamagableUnit>())
        {
            module.UnitWasDestroy += UpdateHP;
            _maxHP += 1;
        }
        _curentHP = _maxHP;
    }

    void UpdateHP(object sender, EventArgs e)
    {
        _curentHP -= 1;
        HpChange?.Invoke(CurrentHP, MaxHP);
        if (_curentHP == 0)
        {
            Destroy(this.gameObject);
        }
    }
}
