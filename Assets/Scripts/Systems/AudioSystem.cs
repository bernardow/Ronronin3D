using System;
using UnityEngine;

namespace Systems
{
    public class AudioSystem : MonoBehaviour
    {
        public static AudioSystem Instance;
        [SerializeField] private Sound[] _sounds;
    
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            foreach (Sound sound in _sounds)
            {
                sound.Source = gameObject.AddComponent<AudioSource>();
                sound.Source.clip = sound.clip;
                sound.Source.volume = sound.Volume;
                sound.Source.pitch = sound.Pitch;
                sound.Source.playOnAwake = sound.PlayOnAwake;
                sound.Source.loop = sound.Loop;
            }
        }

        public void Play(string audioName)
        {
            Sound s = Array.Find(_sounds, sound => sound.name == name);
            s!.Source.Play();
        }
    }
}
