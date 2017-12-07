﻿using UnityEngine;

public enum Team
{
    Friend = 8,
    Enemy = 9,
    Neutral = 10
}

public class Weapon : MonoBehaviour
{
    [SerializeField] Ammo ammoPrefab;
    [SerializeField] float cooldown;

    Team owner;
    float secondsToNextShot;

    void Start()
    {
        SetOwner((Team)gameObject.layer);
    }

    public void Fire()
    {
        if (secondsToNextShot <= 0)
        {
            var ammo = Instantiate<Ammo>(ammoPrefab, transform.position, transform.rotation);
            ammo.SetOwner(owner);
            secondsToNextShot = cooldown;
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
