using System.Collections;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class ParticleTrail : MonoBehaviour
{
    Vector3 offset;
    Transform target;
    ParticleSystem emitter;
    bool following = true;

    void Start()
    {
        emitter = GetComponent<ParticleSystem>();
        target = transform.parent;
        transform.SetParent(transform.parent.parent, true);
        offset = transform.position - target.position;
        var life = target.GetComponent<Life>();
        if (life != null)
        {
            life.OnDeath += () => StartCoroutine(EndParticles());
        }
    }

    void FixedUpdate()
    {
        if (following && target != null)
            transform.position = target.position + offset;
    }

    IEnumerator EndParticles()
    {
        following = false;
        emitter.Stop(true);
        yield return new WaitForSeconds(emitter.main.startLifetime.constant);
        Destroy(gameObject);
    }
}
