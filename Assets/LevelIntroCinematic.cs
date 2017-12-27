using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelIntroCinematic : Cinematic
{
    [SerializeField] ParticleSystem lightSpeed;

    void Awake()
    {
        SetActs(new SortedList<float, System.Action>
            {
                { 0f, () => lightSpeed.Play() },
                { 1.67f, () => Debug.Log("enter") },
                { 3.67f, () => lightSpeed.Stop() },
                { 4.33f, () => Debug.Log("start level") }
            }
        );
    }
}
