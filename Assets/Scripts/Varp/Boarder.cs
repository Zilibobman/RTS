using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Boarder : MonoBehaviour
{
    [SerializeField]protected GameObject transporterObject;
    public void Board(GameObject anotherPlanet)
    {
        Dictionary<GameObject, List<Unit>> ShipsByPlanets = new Dictionary<GameObject, List<Unit>>();
        List<VarpSettings> UnitsWithVarp = new List<VarpSettings>();
        foreach(var unit in SelectedUnitsContainer.Units)
        {
            VarpSettings varpSetting = unit.GetComponent<VarpSettings>();
            if (varpSetting != null)
                UnitsWithVarp.Add(varpSetting);
        }
        foreach (var unitWithVarp in SelectedUnitsContainer.Units.Where(u => u.VarpSettings != null))
        {
            GameObject key = unitWithVarp.GetComponentInParent<VarpLander>().gameObject;
            if (key != null && key != anotherPlanet)
            {
                if (!ShipsByPlanets.ContainsKey(key))
                {
                    ShipsByPlanets[key] = new List<Unit>();
                }
                ShipsByPlanets[key].Add(unitWithVarp);
            }
        }
        foreach(var shipsByPlanet in ShipsByPlanets)
        {
            foreach(var group in shipsByPlanet.Value.GroupBy(sh => sh.VarpSettings.Speed))
            {
                VarpTransporter transporter = Instantiate(transporterObject, shipsByPlanet.Key.transform.position, shipsByPlanet.Key.transform.rotation).GetComponent<VarpTransporter>();
                transporter.Ships = group;
                transporter.gameObject.transform.LookAt(anotherPlanet.transform.position);
                transporter.GoTo(anotherPlanet);
            }
        }
    }
}
