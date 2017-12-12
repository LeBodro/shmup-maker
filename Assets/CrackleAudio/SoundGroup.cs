using UnityEngine;

namespace CrackleAudio
{
    [System.Serializable]
    public class SoundGroup
    {
        [SerializeField] string name;
        [SerializeField] AudioClip[] sounds;

        public string Name { get { return name; } }

        public AudioClip GetRandomSound()
        {
            return sounds[Random.Range(0, sounds.Length)];
        }
    }
}
