using UnityEngine;

public class NavCourse : MonoBehaviour
{
    [SerializeField] NavPoint[] steps;

    void Awake()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position, 2f);

        Gizmos.DrawLine(transform.position, steps[0].Position);
        for (int i = 1; i < steps.Length; i++)
            Gizmos.DrawLine(steps[i - 1].Position, steps[i].Position);
    }

    public bool TryGetStep(int index, out Vector3 step)
    {
        if (index > steps.Length || index < 0)
        {
            step = steps[steps.Length - 1].Position;
            return false;
        }

        if (index == 0)
            step = transform.position;
        else
            step = steps[index - 1].Position;
        return true;
    }
}
