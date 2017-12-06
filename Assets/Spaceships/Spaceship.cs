using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Life))]
public class Spaceship : MonoBehaviour
{
    [SerializeField] float maximumSpeed;
    [SerializeField] float thrusterStrength;

    Rigidbody body;
    Life hull;
    float sqrMaximumSpeed;

    void Start()
    {
        body = GetComponent<Rigidbody>();
        sqrMaximumSpeed = maximumSpeed * maximumSpeed;
        hull = GetComponent<Life>();
        hull.OnDeath += () => Destroy(gameObject);
    }

    public void MoveToward(float x, float y)
    {
        Vector3 target = (new Vector3(x, y, body.position.z) - body.position).normalized;
        body.AddForce(target * thrusterStrength);
    }

    public void MoveToward(Vector3 target)
    {
        body.AddForce(target * thrusterStrength);
    }

    public void MoveTowardRelative(float x, float y)
    {
        Vector3 direction = (new Vector3(x, y)).normalized;
        body.AddForce(direction * thrusterStrength);
    }

    public void FixedUpdate()
    {
        if (body.velocity.sqrMagnitude > sqrMaximumSpeed)
            body.velocity = body.velocity.normalized * maximumSpeed;

        transform.eulerAngles = new Vector3(0, -6, 0) * body.velocity.x;
    }
}
