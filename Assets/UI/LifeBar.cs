using UnityEngine;
using UnityEngine.UI;

public class LifeBar : MonoBehaviour
{
    [SerializeField] ProgressBar lifeDisplay;
    [SerializeField] Animator damageIndicator;
    [SerializeField] Text playerId;
    [SerializeField] Text money;

    public int Money { set { money.text = value.ToString(); } }

    public void SetLifeRatio(float ratio)
    {
        lifeDisplay.SetRatio(ratio);
        damageIndicator.SetFloat("DamageRatio", (1 - ratio) * 3);
    }

    public void SetPlayer(int id, SimpleColorScheme brush)
    {
        playerId.text = string.Format("P{0}", id);
        brush.Paint(id, playerId);
        var graphics = lifeDisplay.GetComponentsInChildren<Graphic>();
        for (int i = 0; i < graphics.Length; i++)
            brush.Paint(id, graphics[i]);
    }
}
