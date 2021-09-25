using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public Audio[] audios;
    void Awake()
    {
        #region SINGLETON
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        #endregion
        foreach (Audio audio in audios)
        {
            audio.source = gameObject.AddComponent<AudioSource>();
            audio.source.clip = audio.clip;
            audio.source.volume = audio.volume;
            audio.source.pitch = audio.pitch;
            audio.source.loop = audio.loop;
        }
    }

    public void Play(string name)
    {
        Audio audio = Array.Find(audios, sound => sound.name == name);
        if (audio == null)
        {
            Debug.Log("Can't find audio!");
            return;
        }
        if (GameManager.isPlaySound == 1 && audio.audioType == AudioType.Sound) return;
        if (GameManager.isPlayMusic == 1 && audio.audioType == AudioType.Music) return;
        audio.source.Play();
    }
    public void Stop(string name)
    {
        Audio audio = Array.Find(audios, sound => sound.name == name);
        if (audio == null)
        {
            Debug.Log("Can't find audio!");
            return;
        }
        audio.source.Stop();
    }
}

[System.Serializable]
public class Audio
{
    public string name;
    public AudioType audioType;
    public AudioClip clip;
    [Range(0f, 1f)] public float volume = 1;
    [Range(.1f, 3f)] public float pitch = 1;
    public bool loop;
    [HideInInspector] public AudioSource source;
}