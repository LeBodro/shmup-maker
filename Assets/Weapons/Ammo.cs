using UnityEngine;

public class Ammo : Vehicle
{
    [SerializeField] float lifeTime = 5f;

    float damage;

    public void Setup(Team owner, int power)
    {
        gameObject.layer = 3 + (int)owner;
        damage = power;
    }

    void OnCollisionEnter(Collision coll)
    {
        Life otherLife = coll.gameObject.GetComponent<Life>();

        if (otherLife != null)
        {
            otherLife.Hurt(damage);
            hull.Kill();
        }
    }

    protected override void FixedUpdate()
    {
        var angle = transform.eulerAngles.z * Mathf.Deg2Rad;
        MoveTowardRelative(-Mathf.Sin(angle), Mathf.Cos(angle));
        base.FixedUpdate();
        lifeTime -= Time.fixedDeltaTime;
        if (lifeTime <= 0)
            hull.Kill();
    }
}
