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

    Action update;

    void Start()
    {
        update = UpdateGameplay;
    }

    void Update()
    {
        if (current <= waves.Length)
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
