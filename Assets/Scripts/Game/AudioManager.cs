using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour {

    public Sound[] sounds;

    public static AudioManager instance;

    void Awake() {

        if (instance == null) {
            instance = this;
        } else {
            gameObject.SetActive(false);
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound sound in sounds) {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;

            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
            sound.source.ignoreListenerPause = sound.ignoreListener;
        }
    }

    public void Play(string name) {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s == null) { Debug.Log("No play");  return; }
        s.source.Play();
    }

    public void Stop(string name) {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) { Debug.Log("No stop"); return; }
        s.source.Stop();
    }

}
