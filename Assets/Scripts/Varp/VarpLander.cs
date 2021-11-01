using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class VarpLander : MonoBehaviour
{
    [SerializeField] protected Terrain BattleField;
    [SerializeField] protected Transform LandPoint;
    private UnitLander _lander;
    private void Awake()
    {
        _lander = new UnitLander();
    }

    public void Land(IEnumerable<Unit> units)
    {
        foreach(var unit in units)
        {
            float size = unit.ModelInfo.ModelSize;
            float SearchRadius = size;
            float PositionRadius = size / 2;
            for(int i = 1; i < int.MaxValue; i++)
            {
                if(LandPoint.TryFindFreePosition(PositionRadius, SearchRadius * i, out Vector3 position))
                {
                    unit.transform.position = position;
                    unit.transform.parent = BattleField.transform;
                    break;
                }
            }
            unit.gameObject.SetActive(true);
        }
    }

}
