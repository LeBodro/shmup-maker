using UnityEngine;

public class LifeBar : MonoBehaviour
{
    [SerializeField] ProgressBar lifeDisplay;
    [SerializeField] Animator damageIndicator;

    public void SetLifeRatio(float ratio)
    {
        lifeDisplay.SetRatio(ratio);
        damageIndicator.SetFloat("DamageRatio", 1 - ratio);
    }
}
