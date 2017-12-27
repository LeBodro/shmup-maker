using UnityEngine;
using System;

public class Level : MonoBehaviour
{
    [SerializeField] Dialog introDialog;
    [SerializeField] Dialog bossDialog;
    [SerializeField] Dialog outroDialog;
    [SerializeField] Wave[] waves;
    [SerializeField] Formation boss;

    int current = 0;
    bool pending = true;
    Theatre theatre;

    event Action _onVictory = delegate{};

    public event Action OnVictory
    {
        add { _onVictory += value; }
        remove { _onVictory -= value; }
    }

    public void Begin(Theatre theatre)
    {
        this.theatre = theatre;
        theatre.Play(introDialog, () => pending = false);
    }

    void Update()
    {
        if (pending)
            return;

        if (current < waves.Length)
            UpdateWaves();
        else
            UpdateBoss();
    }

    void UpdateWaves()
    {
        if (waves[current].IsNotDone)
            waves[current].Update();
        else if (waves[current].AllShipsAreGone)
            current++;

        if (current == waves.Length)
        {
            pending = true;
            theatre.Play(bossDialog, () => pending = false);
        }
    }

    void UpdateBoss()
    {
        boss.Update();
        if (boss.AllShipsAreGone)
        {
            pending = true;
            theatre.Play(outroDialog, _onVictory);
        }
    }

    #if UNITY_EDITOR
    public void RemoveWave(int waveId)
    {
        var cache = new Wave[waves.Length - 1];
        for (int i = 0; i < cache.Length; i++)
        {
            if (i < waveId)
                cache[i] = waves[i];
            else
                cache[i] = waves[i + 1];
        }
        waves = cache;
    }

    public void RemoveFormation(int waveId, int formationId)
    {
        waves[waveId].RemoveFormation(formationId);
    }
    #endif
}
