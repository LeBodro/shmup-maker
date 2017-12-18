using UnityEngine;

[RequireComponent(typeof(Life))]
[RequireComponent(typeof(MeshRenderer))]
public class DamageFeedback : MonoBehaviour
{
    new MeshRenderer renderer;
    Color baseColor;
    float intensity;

    void Start()
    {
        var life = GetComponent<Life>();
        life.OnChange += CheckForFeedback;

        renderer = GetComponent<MeshRenderer>();
        baseColor = renderer.materials[1].color;
    }

    void CheckForFeedback(float oldLife, float newLife)
    {
        if (newLife < oldLife)
            intensity = 1;
    }

    void Update()
    {
        baseColor.a = intensity;
        renderer.materials[1].color = baseColor;
        intensity -= Time.deltaTime;
    }
}
