using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]
public class SimpleColorScheme : ScriptableObject
{
    [SerializeField] Color[] swatches;

    public void Paint(int swatch, SpriteRenderer renderer)
    {
        renderer.color = TryGetColor(swatch);
    }

    public void Paint(int swatch, TextMesh renderer)
    {
        renderer.color = TryGetColor(swatch);
    }

    public void Paint(int swatch, Graphic uiElement)
    {
        uiElement.color = TryGetColor(swatch);
    }

    Color TryGetColor(int i)
    {
        i = Mathf.Clamp(i, 0, swatches.Length);
        return swatches[i];
    }
}
