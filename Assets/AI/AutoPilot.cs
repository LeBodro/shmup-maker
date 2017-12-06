using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Vehicle))]
public class AutoPilot : MonoBehaviour
{
    const float REACHED_THRESHOLD = 1.5f;

    Vehicle ship;
    Queue<Vector3> destinations = new Queue<Vector3>();

    void Start()
    {
        ship = GetComponent<Vehicle>();
    }

    public void QueueDestination(Vector3 destination)
    {
        destinations.Enqueue(destination);
    }

    public void OverrideDestination(Vector3 destination)
    {
        destinations.Clear();
        QueueDestination(destination);
    }

    public void FixedUpdate()
    {
        if (destinations.Count > 0)
        {
            var currentDestination = destinations.Peek();

            ship.MoveToward(currentDestination);
            if (Vector3.Distance(transform.position, currentDestination) <= REACHED_THRESHOLD)
                destinations.Dequeue();
        }
    }
}
