using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class SelectedUnitsContainer
{
    private static List<CanBeChosenUnit> _units = new List<CanBeChosenUnit>();
    public static IEnumerable<CanBeChosenUnit> Units 
    {
        get 
        {
            if(_units.Count == 0)
            {
                _units.AddRange(GameObject.FindGameObjectsWithTag("Ship").Select(ob => ob.GetComponent<CanBeChosenUnit>()));
            }
            return _units.ToArray(); 
        }

    }
    public static void SetUnits(IEnumerable<CanBeChosenUnit> units)
    {
        _units.Clear();
        _units.AddRange(units);
    }
}
