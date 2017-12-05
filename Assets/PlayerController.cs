using UnityEngine;

[RequireComponent(typeof(Spaceship))]
public class PlayerController : MonoBehaviour
{
    const string HORIZONTAL = "{0}Horizontal";
    const string VERTICAL = "{0}Vertical";

    [SerializeField] string controllerId = "P0";

    Spaceship spaceship;

    void Start()
    {
        spaceship = GetComponent<Spaceship>();
    }

    void FixedUpdate()
    {
        spaceship.MoveTowardRelative(
            GetAxis(HORIZONTAL),
            GetAxis(VERTICAL)
        );
    }

    float GetAxis(string format)
    {
        return Input.GetAxis(string.Format(format, controllerId));
    }
}
