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
    Stat _speed;
    Stat _impactDamage;

    float MaxSpeed { get { return _speed != null ? _speed.ProcessedValue : maximumSpeed; } }

    float ImpactDamage { get { return _impactDamage != null ? _impactDamage.ProcessedValue : hull.Current * 0.5f; } }

    float SqrMaxSpeed { get { return _speed != null ? Mathf.Pow(_speed.ProcessedValue, 2) : sqrMaximumSpeed; } }

    void Start()
    {
        body = GetComponent<Rigidbody>();
        sqrMaximumSpeed = maximumSpeed * maximumSpeed;
        hull = GetComponent<Life>();
        hull.OnDeath += Die;

        var stats = GetComponent<StatDictionnary>();
        if (stats != null)
        {
            _speed = stats["Speed"];
            _impactDamage = stats["ImpactDamage"];
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

    protected virtual void OnCollisionEnter(Collision coll)
    {
        Life otherLife = coll.gameObject.GetComponent<Life>();

        if (otherLife != null)
        {
            ApplyImpactDamageOn(otherLife);
        }
    }

    protected void ApplyImpactDamageOn(Life other)
    {
        other.Hurt(ImpactDamage);
    }

    void Die()
    {
        gameObject.SetActive(false);
    }
}
