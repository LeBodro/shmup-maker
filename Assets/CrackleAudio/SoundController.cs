using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CrackleAudio
{
    public class SoundController : SceneSingleton<SoundController>
    {
        [SerializeField] SoundGroup[] soundGroups;
        [SerializeField] SoundGroup[] musics;

        IDictionary<string, SoundGroup> soundMap = new Dictionary<string, SoundGroup>();
        IDictionary<string, SoundGroup> musicMap = new Dictionary<string, SoundGroup>();
        Pool<AudioSource> sources;
        AudioSource musicSource;
        SoundEffect musicEffect;

        public static SoundEffect MusicEffect
        {
            private get { return Instance.musicEffect; }
            set { Instance.musicEffect = value; }
        }

        void Start()
        {
            sources = new Pool<AudioSource>(() => gameObject.AddComponent<AudioSource>());
            musicSource = sources.Get();
            musicSource.loop = true;
            MusicEffect = new SoundEffect();

            MapGroups(soundGroups, soundMap);
            MapGroups(musics, musicMap);
        }

        void MapGroups(SoundGroup[] groups, IDictionary<string, SoundGroup> map)
        {
            foreach (var group in groups)
            {
                map.Add(group.Name, group);
            }
        }

        public static int PlaySound(string groupName, float volume = 1f, SoundEffect effect = null)
        {
            return Instance.PlaySoundImpl(groupName, volume, effect ?? new SoundEffect());
        }

        int PlaySoundImpl(string groupName, float volume, SoundEffect effect)
        {
            var source = sources.Get();
            source.volume = volume;
            source.clip = soundMap[groupName].GetRandomSound();
            source.Play();
            StartCoroutine(ManageSource(source, effect));

            return source.GetInstanceID();
        }

        IEnumerator ManageSource(AudioSource source, SoundEffect effect)
        {
            float timeLeft = source.clip.length;

            while (timeLeft > 0)
            {
                effect.Apply(source, Time.time);
                yield return null;
                timeLeft -= Time.deltaTime;
            }

            sources.Put(source);
        }

        public static int AddEnvironmentalSound(string soundName)
        {
            throw new System.NotImplementedException();
        }

        public static void PlayMusic(string songName, float volume = 1f)
        {
            Instance.PlayMusicImpl(songName, volume);
        }

        void PlayMusicImpl(string songName, float volume)
        {
            musicSource.Stop();
            musicSource.clip = musicMap[songName].GetRandomSound();
            musicSource.volume = volume;
            musicSource.Play();
        }

        void Update()
        {
            if (musicSource.isPlaying)
            {
                MusicEffect.Apply(musicSource, Time.time);
            }
        }
    }
}