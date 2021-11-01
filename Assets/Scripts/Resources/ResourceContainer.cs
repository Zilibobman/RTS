using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceContainer
{
    private ResourceTypes _resourceType;
    private uint _count;
    public ResourceContainer(ResourceTypes resourceType, uint count = 0)
    {
        _resourceType = resourceType;
        _count = count;
    }
    public ResourceTypes ResourceType => _resourceType;
    public uint Count => _count;
    public bool TryAddResource(ResourceContainer container)
    {
        if (container.ResourceType == _resourceType)
        {
            _count += container.GetResource(container.Count);
            return true;
        }
        else return false;
    }
    public uint GetResource(uint Amount)
    {
        uint res = 0;
        if (_count >= Amount)
        {
            _count -= Amount;
            res = Amount;
        }
        else
        {
            res = _count;
            _count = 0;
        }
        return res;
    }
}
