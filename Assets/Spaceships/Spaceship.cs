using UnityEngine;

[RequireComponent(typeof(Vehicle))]
public class Spaceship : MonoBehaviour
{
    [SerializeField] Weapon primaryWeapon;
    [SerializeField] Weapon secondaryWeapon;

    Vehicle engine;

    public Vehicle Engine { get { return engine; } }

    void Start()
    {
        engine = GetComponent<Vehicle>();
    }

    public void SetTeam(Team team)
    {
        gameObject.layer = (int)team;
        if (primaryWeapon != null)
            primaryWeapon.SetOwner(team);
        if (secondaryWeapon != null)
            secondaryWeapon.SetOwner(team);
    }

    public void FirePrimaryWeapon()
    {
        if (primaryWeapon != null)
            primaryWeapon.Fire();
    }

    public void FireSecondaryWeapon()
    {
        if (secondaryWeapon != null)
            secondaryWeapon.Fire();
    }
}
