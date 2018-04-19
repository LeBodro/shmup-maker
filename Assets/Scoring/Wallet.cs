using UnityEngine;
using System;
using System.Collections.Generic;

public class Wallet : MonoBehaviour
{
    [SerializeField] string[] currencies;

    IDictionary<string, Currency> mappedCurrencies;

    public Currency this [string currencyName]
    { 
        get { return mappedCurrencies[currencyName]; } 
    }

    void Start()
    {
        mappedCurrencies = new Dictionary<string, Currency>(currencies.Length);
        foreach (var currencyName in currencies)
        {
            mappedCurrencies.Add(currencyName, new Currency());
        }
    }
}

public class Currency
{
    public int Amount { get; private set; }

    public void Increase(int amount)
    {
        Amount += amount;
        Amount = Mathf.Max(0, Amount);
    }

    public void Decrease(int amount)
    {
        Amount -= amount;
        Amount = Mathf.Max(0, Amount);
    }

    public bool TryTrade(int amount, Action onSuccess = null, Action onFailure = null)
    {
        bool canTrade = amount <= Amount;

        if (canTrade)
        {
            Amount -= amount;
            TryInvoke(onSuccess);
        }
        else
        {
            TryInvoke(onFailure);
        }

        return canTrade;
    }

    void TryInvoke(Action callback)
    {
        if (callback != null)
            callback();
    }
}
