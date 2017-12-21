using UnityEngine;

[RequireComponent(typeof(Spaceship))]
public class AutoPilot : MonoBehaviour
{
    const float REACHED_THRESHOLD = 1.5f;

    Spaceship ship;
    NavNode destination;

    void Start()
    {
        ship = GetComponent<Spaceship>();
    }

    public void StartCourseAt(NavNode start)
    {
        destination = start;
    }

    void FixedUpdate()
    {
        FollowCourse();
    }

    void FollowCourse()
    {
        ship.Engine.MoveToward(destination.Position);
        if (Vector3.Distance(transform.position, destination.Position) <= REACHED_THRESHOLD)
        {
            if (destination.IsLast)
                ship.Remove();
            else
                destination = destination.Next;
        }
    }

    void Update()
    {
        ship.FirePrimaryWeapon();
    }
}
