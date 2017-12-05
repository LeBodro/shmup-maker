using UnityEngine;
using System;

public class Life : MonoBehaviour
{
    [SerializeField] float maximum;

    float current;

    event Action _onDeath = delegate {};

    public event Action OnDeath
    {
        add { _onDeath += value; }
        remove { _onDeath -= value; }
    }

    void Start()
    {
        current = maximum;
    }

    public void Hurt(float damage)
    {
        current -= damage;
        if (current <= 0)
            _onDeath();
    }

    public void Heal(float restoration)
    {
        current = Mathf.Min(current + restoration, maximum);
    }
}
