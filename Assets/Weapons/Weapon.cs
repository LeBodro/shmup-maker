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
    float secondsToNextShot;

    void Start()
    {
        SetOwner((Team)gameObject.layer);
    }

    public void Fire()
    {
        if (secondsToNextShot <= 0)
        {
            var ammo = Instantiate<Ammo>(ammoPrefab, transform.position, transform.rotation);
            ammo.SetOwner(owner);
            secondsToNextShot = cooldown;
        }
    }

    public void SetOwner(Team owner)
    {
        this.owner = owner;
        gameObject.layer = (int)owner;
    }

    void Update()
    {
        secondsToNextShot -= Time.deltaTime;
    }
}
