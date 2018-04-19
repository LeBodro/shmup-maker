using UnityEngine;

public class CurrencyCollectible : Collectible
{
    [SerializeField] string currency;
    [SerializeField] int amount;

    protected override void Collect(Transform collector)
    {
        collector.GetComponent<Wallet>()[currency].Increase(amount);
    }
}
