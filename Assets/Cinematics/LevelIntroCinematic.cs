using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelIntroCinematic : Cinematic
{
    [SerializeField] ParticleSystem lightSpeed;
    [SerializeField] Image whiteOverlay;

    void Awake()
    {
        SetActs(new SortedList<float, System.Action>
            {
                { 0f, () => lightSpeed.Play() },
                { 0.33f, () => FadeIn(0.67f) },
                { 1.67f, () => Debug.Log("enter") },
                { 1.33f, () => lightSpeed.Stop() },
                { 0.67f, () => Debug.Log("start level") }
            }
        );
    }

    void FadeIn(float duration)
    {
        whiteOverlay.CrossFadeAlpha(0, duration, true);
    }
}
