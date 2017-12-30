using UnityEngine;

[RequireComponent(typeof(Spaceship))]
public class PlayerController : MonoBehaviour
{
    const string HORIZONTAL = "P{0}Horizontal";
    const string VERTICAL = "P{0}Vertical";
    const string FIRE = "P{0}Action{1}";

    public int ControllerId { get; set; }


    Spaceship spaceship;

    void Start()
    {
        spaceship = GetComponent<Spaceship>();
        spaceship.SetTeam(Team.Friend);
    }

    public void Restore()
    {
        //TODO: Make this less hacky, give the player back his ship if it was destroyed.
        GetComponent<Life>().Heal(999);
    }

    void FixedUpdate()
    {
        spaceship.Engine.MoveTowardRelative(
            GetAxis(HORIZONTAL),
            GetAxis(VERTICAL)
        );
    }

    void Update()
    {
        if (GetButton(FIRE, 1))
            spaceship.FirePrimaryWeapon();

        if (GetButton(FIRE, 2))
            spaceship.FireSecondaryWeapon();
    }

    float GetAxis(string format)
    {
        return Input.GetAxis(string.Format(format, ControllerId));
    }

    bool GetButton(string format, int button)
    {
        return Input.GetAxis(string.Format(format, ControllerId, button)) > 0.1;
    }
}
