using UnityEngine;

public class StatBoostCollectible : Collectible
{
    [SerializeField] string increasedStat;
    [SerializeField] float boost = 1f;
    [SerializeField] float duration = 10f;

    protected override bool Collect(Transform collector)
    {
        var stat = collector.GetComponent<StatDictionnary>()[increasedStat];
        stat.AddModifier(boost, duration);
        return true;
    }
}
