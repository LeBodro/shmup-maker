using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class NavCourse : MonoBehaviour
{
    [SerializeField] NavPoint[] steps;

    void Awake()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y);
    }

    void OnDrawGizmos()
    {
        #if UNITY_EDITOR
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position, 2f);

        if (Selection.activeTransform != transform && !IsInNavPoints(Selection.activeTransform))
            Gizmos.color = new Color(0, 1, 0, 0.1f);

        if (steps != null && steps.Length > 0)
        {
            Gizmos.DrawLine(transform.position, steps[0].Position);
            var previousPosition = steps[0].Position;
            for (int i = 1; i < steps.Length; i++)
            {
                if (steps[i] != null)
                {
                    Gizmos.DrawLine(previousPosition, steps[i].Position);
                    previousPosition = steps[i].Position;
                }
            }
        }
        #endif
    }

    bool IsInNavPoints(Transform value)
    {
        foreach (var point in steps)
            if (point.transform == value)
                return true;
        return false;
    }

    public Vector3 GetSpawnPosition()
    {
        return transform.position;
    }

    public Vector3[] GetSteps()
    {
        Vector3[] stepPositions = new Vector3[steps.Length];
        for (int i = 0; i < steps.Length; i++)
            stepPositions[i] = steps[i].Position;

        return stepPositions;
    }

    public NavPoint AddNavPoint()
    {
        int n = steps == null ? 1 : steps.Length + 1;
        NavPoint point = new GameObject(string.Format("NavPoint ({0})", n)).AddComponent<NavPoint>();
        
        point.transform.SetParent(transform);
        point.transform.position = transform.position + Vector3.right * 6;
        int nullCount = 0;
        if (steps != null)
        {
            if (steps.Length > 0)
            {
                for (int i = 0; i < steps.Length; i++)
                    if (steps[i] != null)
                        point.transform.position = steps[i].Position + Vector3.right * 6;
                    else
                        nullCount++;
            }
            
            var stepsCache = steps;
            steps = new NavPoint[stepsCache.Length + 1 - nullCount];
            int currentIndex = 0;
            for (int i = 0; i < stepsCache.Length; i++)
            {
                if (stepsCache[i] != null)
                {
                    steps[currentIndex] = stepsCache[i];
                    currentIndex++;
                }
            }
            steps[currentIndex] = point;
        }
        else
        {
            steps = new []{ point };
        }


        return point;
    }
}
