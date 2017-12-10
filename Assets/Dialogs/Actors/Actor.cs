using UnityEngine;

[CreateAssetMenu]
public class Actor : ScriptableObject
{
    [SerializeField] Sprite portrait;
    [SerializeField] string characterName;

    public Sprite Portrait { get { return portrait; } }

    public string CharacterName { get { return characterName; } }
}
