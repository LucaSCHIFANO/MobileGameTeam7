using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    void Start()
    {
        Play("BattleMap1");
    }
    void Awake()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.outputAudioMixerGroup = s.audioMixerGroup;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Stop();
    }
}

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
    public AudioMixerGroup audioMixerGroup;
    [Range(0f, 1f)]
    public float volume = 0.1f;
    [Range(-3f, 3f)]
    public float pitch = 1.0f;
    public bool mute = false;
    public bool loop = false;

    [HideInInspector]
    public AudioSource source;
}
