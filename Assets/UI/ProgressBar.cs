using System.Collections.Generic;
using UnityEngine;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] AnimationCurve distribution;
    [SerializeField][Range(0, 1)] float progress;

    IList<GameObject> cells;

    void Start()
    {
        var count = transform.childCount;
        cells = new List<GameObject>(count);
        for (int i = 0; i < count; i++)
            cells.Add(transform.GetChild(i).gameObject);
    }

    public void SetRatio(float progress)
    {
        Debug.Log(progress);
        var displayedProgress = distribution.Evaluate(progress);
        int activeUpperBound = Mathf.RoundToInt((float)cells.Count * displayedProgress);

        for (int i = 0; i < cells.Count; i++)
            cells[i].SetActive(i < activeUpperBound);
    }
}
