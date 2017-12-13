using UnityEngine;

public class HUD : MonoBehaviour
{
    [SerializeField] Transform lifeBarHolder;
    [SerializeField] LifeBar lifeBarPrefab;
    [SerializeField] SimpleColorScheme playerColors;

    public void RegisterPlayer(PlayerController player)
    {
        LifeBar bar = Instantiate<LifeBar>(lifeBarPrefab, lifeBarHolder, false);
        player.GetComponent<Life>().OnChange += (oldRatio, newRatio) => bar.SetLifeRatio(newRatio);
        bar.SetPlayer(player.ControllerId, playerColors);

        // TODO: do something on death too.
    }
}
