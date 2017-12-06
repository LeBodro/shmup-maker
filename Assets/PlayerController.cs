using UnityEngine;

[RequireComponent(typeof(Vehicle))]
public class PlayerController : MonoBehaviour
{
    const string HORIZONTAL = "{0}Horizontal";
    const string VERTICAL = "{0}Vertical";
    const string FIRE = "{0}Action{1}";

    [SerializeField] string controllerId = "P0";
    [SerializeField] Weapon[] weapons;

    Vehicle spaceship;

    void Start()
    {
        spaceship = GetComponent<Vehicle>();
    }

    void FixedUpdate()
    {
        spaceship.MoveTowardRelative(
            GetAxis(HORIZONTAL),
            GetAxis(VERTICAL)
        );
    }

    void Update()
    {
        for (int i = 0; i < weapons.Length && i < 3; i++)
            if (weapons[i] != null && GetButton(FIRE, i + 1))
                weapons[i].Fire();
                
                
    }

    float GetAxis(string format)
    {
        return Input.GetAxis(string.Format(format, controllerId));
    }

    bool GetButton(string format, int button)
    {
        return Input.GetAxis(string.Format(format, controllerId, button)) > 0.1;
    }
}
