using System;
using System.Collections.Generic;

public class ConflictSideStorage
{
    private ConflictSides _conflictSide;
    public ConflictSideStorage(ConflictSides conflictSide)
    {
        _conflictSide = conflictSide;
    }
    private Dictionary<ResourceTypes, ResourceContainer> _storage = new Dictionary<ResourceTypes, ResourceContainer>();

    public ConflictSides ConflictSide { get => _conflictSide; }
    public void PullResources(ResourceContainer container)
    {
        if(!_storage.ContainsKey(container.ResourceType))
        {
            _storage[container.ResourceType] = new ResourceContainer(container.ResourceType);
        }
        _storage[container.ResourceType].TryAddResource(container);
        ResourceCountChange?.Invoke(container.ResourceType, _storage[container.ResourceType].Count);
    }
    public uint GetResourcesCount(ResourceTypes resourceType)
    { 
        if(_storage.ContainsKey(resourceType))
        {
            return _storage[resourceType].Count;
        }
        return 0;
    }

    public bool TryGetResources(ResourceTypes resourceType, uint amount)
    {
        if (_storage.ContainsKey(resourceType) && _storage[resourceType].Count >= amount)
        {
            _storage[resourceType].GetResource(amount);
            ResourceCountChange?.Invoke(resourceType, _storage[resourceType].Count);
            return true;
        }
        return false;
    }
    public bool TryGetResources(IEnumerable<(ResourceTypes resourceType, uint amount)> resources)
    {
        bool successfully = true;
        foreach(var resource in resources)
        {
            if (!_storage.ContainsKey(resource.resourceType) || _storage[resource.resourceType].Count < resource.amount)
            {
                successfully = false;
                break;
            }
        }
        if (successfully)
        {
            foreach (var resource in resources)
            {
                TryGetResources(resource.resourceType, resource.amount);
            }
        }

        return successfully;
    }

    public event ResourceCountChangeHandler ResourceCountChange;
    public delegate void ResourceCountChangeHandler(ResourceTypes resourceType, uint newCount);

}
