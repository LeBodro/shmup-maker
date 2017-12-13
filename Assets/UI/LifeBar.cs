using UnityEngine;
using UnityEngine.UI;

public class LifeBar : MonoBehaviour
{
    [SerializeField] ProgressBar lifeDisplay;
    [SerializeField] Animator damageIndicator;
    [SerializeField] Text playerId;

    public void SetLifeRatio(float ratio)
    {
        lifeDisplay.SetRatio(ratio);
        damageIndicator.SetFloat("DamageRatio", 1 - ratio);
    }

    public void SetPlayer(int id)
    {
        playerId.text = string.Format("P{0}", id);
        // TODO: set color too
    }
}
