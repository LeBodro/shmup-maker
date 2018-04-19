using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField] ParticleSystem collectionFx;
    [SerializeField] ParticleSystem playerFx;

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            CrackleAudio.SoundController.PlaySound("Collect");
            Collect(col.transform);
            if (collectionFx != null)
            {
                Instantiate(collectionFx, transform.position, Quaternion.identity);
            }

            if (playerFx != null)
            {
                Instantiate(playerFx, col.transform, false);
            }
            Destroy(gameObject);
        }
    }

    protected virtual void Collect(Transform collector)
    {
    }
}
