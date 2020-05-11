using UnityEngine.Audio;
using UnityEngine;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public enum GlobalSound
    {
        Click,
        BirdsLoop,
        Countdown,
        TickingLoop,
        Last10Seconds,
        Success,
        Spawn,
        GameOver
    }

    [SerializeField] private Sound[] GlobalSounds;
    [SerializeField] private AudioClip[] ImpactSounds;

    private List<Sound> PausedSounds = new List<Sound>();

    // Start is called before the first frame update
    void Awake()
    {
        SetGlobalAudioSources();
    }

    public void Play(GlobalSound name)
    {
        Sound s = FindSound(name);
        s?.source.Play();
    }

    public void PlayOnce(GlobalSound name)
    {
        Sound s = FindSound(name);

        if (s != null && !s.source.isPlaying)
        {
            s.source.Play();
        } 
    }

    public void Stop(GlobalSound name)
    {
        Sound s = FindSound(name);
        s?.source.Stop();
    }

    public void PauseIfPlaying(GlobalSound name)
    {
        Sound s = FindSound(name);

        if (s != null && s.source.isPlaying)
        {
            s.source.Pause();
            PausedSounds.Add(s);
        }
    }

    public void ResumeIfPaused(GlobalSound name)
    {
        Sound s = FindSound(name);

        if (s != null && PausedSounds.Contains(s))
        {
            s.source.Play();
            PausedSounds.Remove(s);
        }
    }

    public void PlayImpactSound(AudioSource source, float velocity)
    {
        float volume = Mathf.Min(velocity / 3.0f, 1f);
        float pitch = Random.Range(0.95f, 1.05f);
        AudioClip clip = ImpactSounds[Random.Range(0, ImpactSounds.Length)];

        source.pitch = pitch;
        source.PlayOneShot(clip, volume);

        // maybe: play sound only on one brick if two bricks collide
    }

    private void SetGlobalAudioSources()
    {
        foreach (Sound s in GlobalSounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    private Sound FindSound(GlobalSound name)
    {
        Sound s = System.Array.Find(GlobalSounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound '" + name + "' does not exist in GlobalSounds Array!");
            return null;
        }
        return s;
    }
}
