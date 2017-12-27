using UnityEngine;
using System;
using System.Collections.Generic;

public class Cinematic : MonoBehaviour
{
    SortedList<float, Action> acts;

    bool isPlaying;
    float elapsedTime;
    int nextActIndex;
    float nextActMoment;

    Action onDone = delegate
    {
    };

    public void PlayThen(Action onDone)
    {
        this.onDone = onDone;
        isPlaying = true;
        nextActMoment = acts.Keys[0];
    }

    protected void SetActs(SortedList<float, Action> acts)
    {
        this.acts = acts;
    }

    void Update()
    {
        if (isPlaying)
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= nextActMoment)
            {
                acts[nextActMoment]();
                nextActIndex++;
                if (nextActIndex < acts.Count)
                {
                    nextActMoment = acts.Keys[nextActIndex];
                }
                else
                {
                    isPlaying = false;
                    nextActIndex = 0;
                    elapsedTime = 0f;
                    onDone();
                }
            }
        }
    }
}
