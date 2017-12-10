using UnityEngine;
using UnityEngine.UI;

public class ActorHolder : MonoBehaviour
{
    [SerializeField] Image portrait;
    [SerializeField] Text characterName;

    public void SetCharacter(Actor character)
    {
        portrait.sprite = character.Portrait;
        characterName.text = character.CharacterName;
    }
}
