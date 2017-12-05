using UnityEngine;

public enum Team
{
    Federation,
    Android
}

public class Weapon : MonoBehaviour
{
    [SerializeField] Ammo ammoPrefab;
    [SerializeField] float cooldown;

    Team owner;

    public void Fire()
    {
        var ammo = Instantiate<Ammo>(ammoPrefab, transform.position, transform.rotation);
        ammo.SetOwner(owner);
    }

    public void SetOwner(Team owner)
    {
        this.owner = owner;
    }
}
