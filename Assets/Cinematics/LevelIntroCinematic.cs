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
                { 2f, () => lightSpeed.Stop() },
                { 0.67f, () => Debug.Log("start level") }
            }
        );
    }

    void FadeIn(float duration)
    {
        Color transparent = new Color(1, 1, 1, 0);
        whiteOverlay.CrossFadeColor(transparent, duration, false, true);
    }
}
