using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ShipSelector
{
    [SerializeField] Spaceship[] selectables;

    IDictionary<int, int> selectedShipById = new Dictionary<int, int>();

    public Spaceship GetInstanceForId(int id)
    {
        if (selectedShipById.ContainsKey(id))
            selectedShipById[id] = (selectedShipById[id] + 1) % selectables.Length;
        else
            selectedShipById[id] = 0;
        
        return Object.Instantiate(selectables[selectedShipById[id]], Vector3.zero, Quaternion.identity);
    }
}
