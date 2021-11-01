using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UnitsCreator : MonoBehaviour, IHaveConflictSide
{
    [SerializeField]private UnitCreationInfo[] _possibleUnits;
    [SerializeField] private ConflictSides _conflictSide;

    public IEnumerable<UnitCreationInfo> PossibleUnits => _possibleUnits;
    public ConflictSides ConflictSide => _conflictSide;
    private Queue<UnitCreationInfo> _productionQueue = new Queue<UnitCreationInfo>();
    private ConflictSideStorage _ResourcesStorage;
    private float _currentUnitInProductionTime;
    private Terrain _terrain;
    private UnitLanderWithCreation _lander;
    private void Awake()
    {
        _lander = new UnitLanderWithCreation();
    }
    private void Start()
    {
        _ResourcesStorage = ConflictSidesStorages.instance.GetConflictSideStorage(_conflictSide);
    }

    private void OnEnable()
    {
        _terrain = GetComponentInParent<Terrain>();
    }

    private void Update()
    {
        if (_productionQueue.Count != 0)
        {
            if(_productionQueue.Peek().TimeForCreate < _currentUnitInProductionTime)
            {
                _currentUnitInProductionTime += Time.deltaTime;
            }
            else
            {
                _lander.Land(_productionQueue.Dequeue().Unit.GetComponent<Unit>(), transform, _terrain);
                _currentUnitInProductionTime = 0;
            }
        }
    }

    public bool TryCreateUnit(UnitCreationInfo unitCreationInfo)
    {
        bool u = _ResourcesStorage.TryGetResources(unitCreationInfo.Price.Select(res => (res.Resource, res.Price)));
        bool h = _possibleUnits.Contains(unitCreationInfo);
        if (_possibleUnits.Contains(unitCreationInfo) && 
            _ResourcesStorage.TryGetResources(unitCreationInfo.Price.Select(res => (res.Resource, res.Price))))
        {
            _productionQueue.Enqueue(unitCreationInfo);
            return true;
        }
        else
        {
            return false;
        }
    }
}
