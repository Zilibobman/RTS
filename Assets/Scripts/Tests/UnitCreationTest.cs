using System.Linq;
using UnityEngine;

public class UnitCreationTest : MonoBehaviour
{
    public UnitsCreator Creator;
    public UnitCreationInfo Unit;
    public void Test()
    {
        Creator.TryCreateUnit(Creator.PossibleUnits.Last());
    }
}
