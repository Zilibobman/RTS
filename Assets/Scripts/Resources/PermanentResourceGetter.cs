using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class PermanentResourceGetter : IResourceGetter
{
    private ResourceTypes _resourceType;
    private uint _productivity;
    public ResourceTypes ResourceType => _resourceType;
    public bool CanProduce => true;
    public PermanentResourceGetter(ResourceTypes resourceType, uint productivity)
    {
        _resourceType = resourceType;
        _productivity = productivity;
    }

    public ResourceContainer GetResources()
    {
        return new ResourceContainer(_resourceType, _productivity);
    }
}
