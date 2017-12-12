using UnityEngine;

namespace CrackleAudio
{
    public class SinusoidalPitchEffect : SoundEffect
    {
        float magnitude;
        float frequency;

        public SinusoidalPitchEffect(float magnitude, float frequency)
        {
            this.magnitude = magnitude;
            this.frequency = frequency;
        }

        public override void Apply(AudioSource source, float time)
        {
            source.pitch = magnitude * Mathf.Sin(time * frequency) + 1;
        }
    }
}
