using UnityEngine;

public enum Team
{
    Friend = 8,
    Enemy = 9,
    Neutral = 10
}

public class Weapon : MonoBehaviour
{
    [SerializeField] Ammo ammoPrefab;
    [SerializeField] float cooldown;

    Team owner;

    void Start()
    {
        SetOwner((Team)gameObject.layer);
    }

    public void Fire()
    {
        var ammo = Instantiate<Ammo>(ammoPrefab, transform.position, transform.rotation);
        ammo.SetOwner(owner);
    }

    public void SetOwner(Team owner)
    {
        this.owner = owner;
        gameObject.layer = (int)owner;
    }
}
