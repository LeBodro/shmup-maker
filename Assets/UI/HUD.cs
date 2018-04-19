using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [SerializeField] Transform lifeBarHolder;
    [SerializeField] LifeBar lifeBarPrefab;
    [SerializeField] SimpleColorScheme playerColors;
    [SerializeField] Text money;

    IDictionary<int, LifeBar> barsByPlayer = new Dictionary<int, LifeBar>();

    public void RegisterPlayer(PlayerController player)
    {
        LifeBar bar = null;
        if (barsByPlayer.ContainsKey(player.ControllerId))
        {
            bar = barsByPlayer[player.ControllerId];
        }
        else
        {
            bar = Instantiate<LifeBar>(lifeBarPrefab, lifeBarHolder, false);
            bar.SetPlayer(player.ControllerId, playerColors);
            barsByPlayer.Add(player.ControllerId, bar);
        }
        player.GetComponent<Life>().OnChange += (oldRatio, newRatio) => bar.SetLifeRatio(newRatio);
        player.GetComponent<Wallet>()["Averias"].OnChange += (oldValue, newValue) => bar.Money = newValue;

        // TODO: do something on death too.
    }
}
