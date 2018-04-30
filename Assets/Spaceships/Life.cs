using UnityEngine;
using System;

public class Life : MonoBehaviour
{
    [SerializeField] float maximum;
    [SerializeField] ParticleSystem deathFxPrefab;
    [SerializeField] String deathSound = "Explosion";

    float _current;

    Stat health;

    float Maximum { get { return health != null ? health.ProcessedValue : maximum; } }

    public float Current
    {
        get { return _current; }
        private set
        {
            float oldValue = _current;
            _current = value;
            _onChange(oldValue / Maximum, _current / Maximum);
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
        var stats = GetComponent<StatDictionnary>();
        if (stats != null)
        {
            health = stats["Health"];
        }
        Current = Maximum;
    }

    public void Hurt(float damage)
    {
        Current = Mathf.Max(Current - damage, 0);
        if (Current <= 0)
            Kill();
    }

    public void Heal(float restoration)
    {
        Current = Mathf.Min(Current + restoration, Maximum);
    }

    public void Kill()
    {
        Current = 0;
        if (deathFxPrefab != null)
        {
            Instantiate<ParticleSystem>(deathFxPrefab, transform.position, Quaternion.identity);
            CrackleAudio.SoundController.PlaySound(deathSound);
        }
        _onDeath();
    }

    public void KillSilently()
    {
        _onDeath();
    }
}
