using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcePermanentMinerComponent : MonoBehaviour
{
    [SerializeField] private ConflictSides _conflictSide;
    [SerializeField] private ResourceTypes _resourceType;
    [SerializeField] private float _productivity;
    private float _delay;
    private ConflictSideStorage _conflictSideStorage;
    private PermanentResourceGetter _resourceGetter;
    void Start()
    {
        _conflictSideStorage = ConflictSidesStorages.instance.GetConflictSideStorage(_conflictSide);
        _resourceGetter = new PermanentResourceGetter(_resourceType, 1);
        _delay = 60 / _productivity;
        InvokeRepeating("GetResource", _delay, _delay);
    }

    private void GetResource()
    {
        _conflictSideStorage.PullResources(_resourceGetter.GetResources());
    }
}
