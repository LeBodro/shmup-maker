using UnityEngine;

public class NavPoint : MonoBehaviour
{
    public Vector3 Position { get { return transform.position; } }

    void Awake()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, 1.5f);
    }
}
