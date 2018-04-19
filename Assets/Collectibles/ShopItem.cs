using UnityEngine;

public class ShopItem : Collectible
{
    [SerializeField] int cost;
    [SerializeField] string stat;
    [SerializeField] float permanentIncrease;

    Transform collector;

    protected override void Collect(Transform collector)
    {
        this.collector = collector;
        collector.GetComponent<Wallet>()["Averias"].TryTrade(cost, BoostStat);
    }

    void BoostStat()
    {
        collector.GetComponent<StatDictionnary>()[stat].PermanentlyIncrease(permanentIncrease);
    }
}
