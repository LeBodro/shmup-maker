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

    public void FirePrimaryWeapon()
    {
        primaryWeapon.Fire();
    }

    public void FireSecondaryWeapon()
    {
        if (secondaryWeapon != null)
            secondaryWeapon.Fire();
    }
}
