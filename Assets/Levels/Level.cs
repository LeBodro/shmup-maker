using UnityEngine;
using System;

public class Level : MonoBehaviour
{
    [SerializeField] Dialog introDialog;
    [SerializeField] Dialog bossDialog;
    [SerializeField] Dialog outroDialog;
    [SerializeField] AIWave[] waves;
    [SerializeField] AIWave boss;

    int current = 0;
    bool pending = true;
    Theatre theatre;

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
            theatre.Play(outroDialog, () => Debug.Log("Congratulations!"));
        }
    }
}
