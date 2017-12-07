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

    public void Begin()
    {
        pending = false;
    }

    void Update()
    {
        if (pending)
            return;

        if (current < waves.Length)
            UpdateGameplay();
    }

    void UpdateGameplay()
    {
        if (waves[current].IsNotDone)
            waves[current].Update();
        else if (waves[current].AllShipsAreGone)
            current++;
    }
}
