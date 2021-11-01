using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelInfo : MonoBehaviour
{
    [SerializeField] private GameObject _modelObject;
    private float _modelSize;
    public GameObject ModelObject => _modelObject;
    public float ModelSize => _modelSize;
    private void Awake()
    {
        if(_modelObject == null)
        {
            _modelObject = gameObject.transform.GetChildWithTag("Model").gameObject;
        }
        _modelSize = _modelObject.GetComponentInChildren<MeshFilter>().sharedMesh.bounds.size.magnitude;
    }
}
