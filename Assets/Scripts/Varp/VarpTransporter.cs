using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class VarpTransporter : MonoBehaviour
{
    public IEnumerable<Unit> Ships;
    [SerializeField] protected GoToVector3PointPilot pilot;
    protected GameObject Destination;
    private AbstractDriver<Destinations> driver;
    private RenderModelCreator _modelCreator = new RenderModelCreator();
    private GameObject _render;

    private void Awake()
    {
        driver = GetComponentInChildren<AbstractDriver<Destinations>>();
    }
    public void GoTo(GameObject Destination)
    {
        driver.MaxSpeed = Ships.First().VarpSettings.Speed;
        foreach (var ship in Ships)
        {
            ship.transform.parent = null;
            ship.gameObject.SetActive(false);
        }
        this.Destination = Destination;
        pilot.destination = Destination.transform.position;
        if (_render != null)
        {
            GameObject.Destroy(_render.gameObject);
        }
        _render = Instantiate(Ships.First().ModelInfo.ModelObject, gameObject.transform);
        _render.SetActive(true);
        pilot.StartNavigate();
    }
    protected void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == Destination)
        {
            other.gameObject.GetComponent<VarpLander>().Land(Ships);
            pilot.StopNavigate();
            GameObject.Destroy(this.gameObject);
        }
    }
}
