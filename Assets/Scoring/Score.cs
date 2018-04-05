using UnityEngine;
using System.Collections.Generic;

public class Score : MonoBehaviour
{
    IDictionary<int, int> scores = new Dictionary<int, int>(8);

    static Score instance;

    void Start()
    {
        instance = this;
    }

    public static void Increase(int player, int value)
    {
        instance.IncreaseImpl(player, value);
    }

    void IncreaseImpl(int player, int value)
    {
        if (!scores.ContainsKey(player))
        {
            scores[player] = value;
        }
        else
        {
            scores[player] += value;
        }
    }
}
