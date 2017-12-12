using UnityEngine;

namespace CrackleAudio
{
    public class SoundEffect
    {
        public virtual void Apply(AudioSource source, float time)
        {
            source.pitch = 1;
        }
    }
}