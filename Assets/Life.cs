using UnityEngine;
using System;

public class Life : MonoBehaviour
{
    [SerializeField] float maximum;
    [SerializeField] ParticleSystem deathFxPrefab;

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
            Kill();
    }

    public void Heal(float restoration)
    {
        current = Mathf.Min(current + restoration, maximum);
    }

    public void Kill()
    {
        if (deathFxPrefab != null)
            Instantiate<ParticleSystem>(deathFxPrefab, transform.position, Quaternion.identity);
        _onDeath();
    }

    public void KillSilently()
    {
        _onDeath();
    }
}
