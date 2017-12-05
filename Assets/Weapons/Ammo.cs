using UnityEngine;

public class Ammo : MonoBehaviour
{
    [SerializeField] float damage;
    [SerializeField] ParticleSystem explosionFx;

    public void SetOwner(Team owner)
    {
        gameObject.layer = 3 + (int)owner;
    }

    void OnCollisionEnter(Collision coll)
    {
        Life otherLife = coll.gameObject.GetComponent<Life>();

        if (otherLife != null)
        {
            otherLife.Hurt(damage);
            Explode();
        }

        Destroy(gameObject);
    }

    void Explode()
    {
        Instantiate<ParticleSystem>(explosionFx, transform.position, Quaternion.identity);
    }
}
