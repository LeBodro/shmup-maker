using UnityEngine;

public enum Team
{
    Friend = 8,
    Enemy = 9,
    Neutral = 10
}

public class Weapon : MonoBehaviour
{
    const float DAMAGE_PER_SECOND = 5;

    [SerializeField] Ammo ammoPrefab;
    [SerializeField] [Range(0.1f, 5)] float shotsPerSecond = 1;
    [SerializeField] [Range(0.2f, 1)] float powerDamping = 1;

    float cooldown;
    int power;

    Team owner;
    float secondsToNextShot;

    void Start()
    {
        SetOwner((Team)gameObject.layer);
        cooldown = 1 / Mathf.Max(shotsPerSecond, 0.1f);
        power = (int)Mathf.Max(DAMAGE_PER_SECOND * cooldown * powerDamping, 1);
    }

    public void Fire()
    {
        if (secondsToNextShot <= 0)
        {
            var ammo = Instantiate<Ammo>(ammoPrefab, transform.position, transform.rotation);
            ammo.Setup(owner, power);
            secondsToNextShot = cooldown;
            CrackleAudio.SoundController.PlaySound("Laser");
        }
    }

    public void SetOwner(Team owner)
    {
        this.owner = owner;
        gameObject.layer = (int)owner;
    }

    void Update()
    {
        secondsToNextShot -= Time.deltaTime;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, 0.5f);
        Gizmos.color = Color.green;
        GizmoPlus.DrawArrowGizmo(transform.position, transform.eulerAngles.z * Mathf.Deg2Rad, 3);
    }
}

public static class GizmoPlus
{
    public static void DrawArrowGizmo(Vector3 position, float angle, float length)
    {
        Vector3 tip = new Vector3(-Mathf.Sin(angle), Mathf.Cos(angle)) * length;
        var pointAngle = angle + 0.75f * Mathf.PI;
        Vector3 leftLine = new Vector3(-Mathf.Sin(pointAngle), Mathf.Cos(pointAngle)) * length * 0.4f;
        pointAngle += Mathf.PI * 0.5f;
        Vector3 rightLine = new Vector3(-Mathf.Sin(pointAngle), Mathf.Cos(pointAngle)) * length * 0.4f;

        Gizmos.DrawRay(position, tip);
        Gizmos.DrawRay(position + tip, leftLine);
        Gizmos.DrawRay(position + tip, rightLine);
    }
}
