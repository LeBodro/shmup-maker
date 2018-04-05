using UnityEngine;

public class AmmoCollectible : Collectible
{
    enum Weapon
    {
        PRIMARY,
        SECONDARY,
    };

    [SerializeField] Ammo newAmmoType;
    [SerializeField] Weapon targetWeapon;

    protected override void Collect(Transform collector)
    {
        switch (targetWeapon)
        {
            case Weapon.PRIMARY:
                collector.GetComponent<Spaceship>().ChangePrimaryAmmo(newAmmoType);
                break;
            case Weapon.SECONDARY:
                collector.GetComponent<Spaceship>().ChangeSecondaryAmmo(newAmmoType);
                break;
        }
    }
}
