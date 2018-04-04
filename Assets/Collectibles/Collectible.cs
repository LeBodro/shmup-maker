using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField] string increasedStat;
    [SerializeField] float boost = 1f;
    [SerializeField] float duration = 10f;
    [SerializeField] ParticleSystem collectionFx;
    [SerializeField] ParticleSystem playerFx;

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            Collect(col.transform);
        }
    }

    void Collect(Transform collector)
    {
        var stat = collector.GetComponent<StatDictionnary>()[increasedStat];
        stat.AddModifier(boost, duration);
        Instantiate(collectionFx, transform.position, Quaternion.identity);
        Instantiate(playerFx, collector, false);
        Destroy(gameObject);
    }
}
