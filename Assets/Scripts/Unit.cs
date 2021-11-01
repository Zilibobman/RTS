using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] private ModelInfo _modelInfo;
    [SerializeField] private VarpSettings _varpSettings;

    public ModelInfo ModelInfo => _modelInfo;
    public VarpSettings VarpSettings => _varpSettings;
    public void Awake()
    {
        if (_modelInfo == null)
            _modelInfo = GetComponentInChildren<ModelInfo>();
        if(_varpSettings == null)
            _varpSettings = GetComponentInChildren<VarpSettings>();
    }
}
