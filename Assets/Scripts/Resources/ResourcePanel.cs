using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourcePanel : MonoBehaviour, IHaveConflictSide
{
    [SerializeField] private ConflictSides _conflictSide;
    [SerializeField] private GameObject _resoursLabelObj;
    private Dictionary<ResourceTypes, Text> _resourcesCountText;

    public static ResourcePanel instance;
    public ConflictSides ConflictSide => _conflictSide;

    void Start()
    {
        _resourcesCountText = new Dictionary<ResourceTypes, Text>();
        ConflictSidesStorages.instance.GetConflictSideStorage(_conflictSide).ResourceCountChange += UpdateResourcesCountText;
    }
    private void UpdateResourcesCountText(ResourceTypes resourceType, uint newCount)
    {
        if(!_resourcesCountText.ContainsKey(resourceType))
        {
            GameObject resourceLable = Instantiate(_resoursLabelObj, this.gameObject.transform);
            resourceLable.transform.Find("ResourceName").GetComponent<Text>().text = resourceType.ToString();
            _resourcesCountText[resourceType] = resourceLable.transform.Find("ResourceCount").GetComponent<Text>();
        }
        _resourcesCountText[resourceType].text = newCount.ToString();
    }
}
