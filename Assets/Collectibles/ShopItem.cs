using UnityEngine;

public class ShopItem : Collectible
{
    [SerializeField] int cost;
    [SerializeField] string stat;
    [SerializeField] float permanentIncrease;

    Transform collector;
    bool transactionDone;

    protected override bool Collect(Transform collector)
    {
        this.collector = collector;
        collector.GetComponent<Wallet>()["Averias"].TryTrade(cost, BoostStat);
        return transactionDone;
    }

    void BoostStat()
    {
        collector.GetComponent<StatDictionnary>()[stat].PermanentlyIncrease(permanentIncrease);
        transactionDone = true;
    }
}
