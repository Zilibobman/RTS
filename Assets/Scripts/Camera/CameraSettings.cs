using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSettings : MonoBehaviour
{
    [SerializeField] protected float maxHeight = 15;
    [SerializeField] protected float minHeight = 5;
    [SerializeField] protected int rotationLimit = 240;
    [SerializeField] protected int rotationX = 70;
    [SerializeField] protected Transform startPoint;
    [SerializeField] protected float speed = 5;
    [SerializeField] protected float far = 5;

    public float MaxHeight { get => maxHeight; }
    public float MinHeight { get => minHeight; }
    public int RotationLimit { get => rotationLimit; }
    public int RotationX { get => rotationX; }
    public Transform StartPoint { get => startPoint; }
    public float Speed { get => speed; }
    public float Far { get => far; }

    /*public void OnMouseDown()
    {
        Activate();
    }*/
    public void Activate()
    {
        ICameraController cameraController = Camera.main.GetComponent<ICameraController>();
        cameraController.ChangeSettings(this);
        cameraController.IChangeSettings += CameraChangeSettings;
    }
    public void CameraChangeSettings(ICameraController sender, CameraSettings oldSettings, CameraSettings newSettings)
    {
        MonoBehaviour obj = sender as MonoBehaviour;
        if (oldSettings == this)
        {
            startPoint.position = new Vector3(obj.transform.position.x, startPoint.position.y, obj.transform.position.z);
        }
        if(newSettings != this)
        {
            sender.IChangeSettings -= CameraChangeSettings;
        }
    }
}
