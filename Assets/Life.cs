using UnityEngine;
using System;

public class Life : MonoBehaviour
{
    [SerializeField] float maximum;
    [SerializeField] ParticleSystem deathFxPrefab;

    float _current;

    float Current
    {
        get { return _current; }
        set
        {
            float oldValue = _current;
            _current = value;
            _onChange(oldValue / maximum, _current / maximum);
        }
    }

    event Action<float, float> _onChange = delegate {};

    public event Action<float, float> OnChange
    {
        add { _onChange += value; }
        remove { _onChange -= value; }
    }

    event Action _onDeath = delegate {};

    public event Action OnDeath
    {
        add { _onDeath += value; }
        remove { _onDeath -= value; }
    }

    void Start()
    {
        Current = maximum;
    }

    public void Hurt(float damage)
    {
        Current -= damage;
        if (Current <= 0)
            Kill();
    }

    public void Heal(float restoration)
    {
        Current = Mathf.Min(Current + restoration, maximum);
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
