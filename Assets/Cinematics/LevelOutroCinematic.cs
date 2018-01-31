using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelOutroCinematic : Cinematic
{
    [SerializeField] ParticleSystem lightSpeed;
    [SerializeField] Image whiteOverlay;

    void Awake()
    {
        whiteOverlay.gameObject.SetActive(true);
        whiteOverlay.CrossFadeAlpha(0, 0, true);
        SetActs(new SortedList<float, System.Action>
            {
                { 0f, () => lightSpeed.Play() },
                { 0.33f, () => FadeOut(0.67f) },
                { 1.67f, () => Debug.Log("show scores") },
                { 2f, () => lightSpeed.Stop() },
                { 0.67f, () => Debug.Log("end") }
            }
        );
    }

    void FadeOut(float duration)
    {
        whiteOverlay.CrossFadeAlpha(1, duration, true);
    }
}
