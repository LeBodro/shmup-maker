using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteInEditMode]
public class NavNode : MonoBehaviour
{
    const float RADIUS = 1.5f;

    [SerializeField] NavNode next;

    public Vector3 Position { get { return transform.position; } }

    public NavNode Next { get { return next; } }

    public bool IsLast { get { return next == null; } }

    #if UNITY_EDITOR
    public void OnDrawGizmos()
    {
        bool isSelected = Selection.activeGameObject == gameObject;
        Color c = IsLast ? Color.yellow : Color.green;
        c.a = isSelected ? 1f : 0.25f;
        Gizmos.color = c;
        Gizmos.DrawSphere(transform.position, RADIUS);

        if (!IsLast)
        {
            Vector3 destination = next.transform.position;
            Vector3 direction = (destination - transform.position).normalized;
            Gizmos.DrawLine(transform.position, destination - RADIUS * direction);
            Gizmos.DrawWireSphere(destination - RADIUS * direction, 0.25f);
            Gizmos.DrawWireSphere(destination - 2f * direction, 0.5f);
            Gizmos.DrawWireSphere(destination - 2.75f * direction, 0.75f);
        }
    }

    public NavNode InsertNode()
    {
        NavNode node = new GameObject("NavNode").AddComponent<NavNode>();
        node.SetNext(next);
        next = node;
        node.transform.position = transform.position + Vector3.right * 6;
        return node;
    }

    public void SetNext(NavNode next)
    {
        this.next = next;
    }

    void Update()
    {
        if (Mathf.RoundToInt(Position.z) != 0)
        {
            var pos2d = Position;
            pos2d.z = 0;
            transform.position = pos2d;
        }
    }
    #endif
}
