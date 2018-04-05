using UnityEngine;

public class Wallet : MonoBehaviour
{
    public float Money { get; private set; }

    public void Add(float amount)
    {
        Money += amount;
    }

    public bool TryToBuy(float amount)
    {
        if (amount > Money)
        {
            return false;
        }
        else
        {
            Money -= amount;
            return true;
        }
    }
}
