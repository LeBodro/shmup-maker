using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Life))]
public class Vehicle : MonoBehaviour
{
    [SerializeField] float maximumSpeed;
    [SerializeField] float thrusterStrength;

    protected Life hull;
    Rigidbody body;
    float sqrMaximumSpeed;
    Stat speed;

    float MaxSpeed { get { return speed != null ? speed.ProcessedValue : maximumSpeed; } }

    float SqrMaxSpeed { get { return speed != null ? Mathf.Pow(speed.ProcessedValue, 2) : sqrMaximumSpeed; } }

    void Start()
    {
        body = GetComponent<Rigidbody>();
        sqrMaximumSpeed = maximumSpeed * maximumSpeed;
        hull = GetComponent<Life>();
        hull.OnDeath += Die;

        var stats = GetComponent<StatDictionnary>();
        if (stats != null)
        {
            speed = stats["Speed"];
        }
    }

    public void MoveToward(float x, float y)
    {
        Vector3 target = (new Vector3(x, y, body.position.z) - body.position).normalized;
        body.AddForce(target * thrusterStrength);
    }

    public void MoveToward(Vector3 target)
    {
        body.AddForce((target - body.position).normalized * thrusterStrength);
    }

    public void MoveTowardRelative(float x, float y)
    {
        Vector3 direction = (new Vector3(x, y)).normalized;
        body.AddForce(direction * thrusterStrength);
    }

    protected virtual void FixedUpdate()
    {
        if (body.velocity.sqrMagnitude > SqrMaxSpeed)
            body.velocity = body.velocity.normalized * MaxSpeed;

        var baseAngle = transform.eulerAngles;
        baseAngle.y = 0;
        transform.eulerAngles = baseAngle + new Vector3(0, -60f / MaxSpeed, 0) * body.velocity.x;
    }

    void Die()
    {
        gameObject.SetActive(false);
    }
}
