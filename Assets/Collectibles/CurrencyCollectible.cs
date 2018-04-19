using UnityEngine;

public class CurrencyCollectible : Collectible
{
    [SerializeField] string currency;
    [SerializeField] int amount;

    protected override bool Collect(Transform collector)
    {
        collector.GetComponent<Wallet>()[currency].Increase(amount);
        return true;
    }
}
