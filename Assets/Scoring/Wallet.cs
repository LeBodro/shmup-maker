using UnityEngine;
using System;
using System.Collections.Generic;

public class Wallet : MonoBehaviour
{
    [SerializeField] string[] currencies;

    IDictionary<string, Currency> mappedCurrencies;

    public Currency this [string currencyName]
    { 
        get
        { 
            if (mappedCurrencies == null)
                Initialize();
            return mappedCurrencies[currencyName];
        } 
    }

    void Awake()
    {
        if (mappedCurrencies == null)
            Initialize();
    }

    void Initialize()
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
    int _amount;

    public int Amount
    {
        get { return _amount; }
        private set
        {
            value = Mathf.Max(0, value);
            if (_amount != value)
                _onChange(_amount, value);
            _amount = value;
        }
    }

    event Action<int, int> _onChange = delegate {};

    public event Action<int, int> OnChange
    {
        add { _onChange += value; }
        remove { _onChange -= value; }
    }

    public void Increase(int amount)
    {
        Amount += amount;
    }

    public void Decrease(int amount)
    {
        Amount -= amount;
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
