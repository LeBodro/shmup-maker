using UnityEngine;

public class HealthCollectible : Collectible
{
    [SerializeField] float restoration;

    protected override void Collect(Transform collector)
    {
        collector.GetComponent<Life>().Heal(restoration);
    }
}
