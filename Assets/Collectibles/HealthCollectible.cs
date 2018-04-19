using UnityEngine;

public class HealthCollectible : Collectible
{
    [SerializeField] float restoration;

    protected override bool Collect(Transform collector)
    {
        collector.GetComponent<Life>().Heal(restoration);
        return true;
    }
}
