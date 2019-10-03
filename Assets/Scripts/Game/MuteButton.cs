using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MuteButton : MonoBehaviour {

    //Cache
    SaveLoad saveLoad;
    Button button;

    //Parameters
    [SerializeField] Sprite unmuted;
    [SerializeField] Sprite muted;

    //State
    bool isMuted;

    void Awake() {
        saveLoad = FindObjectOfType<SaveLoad>();
        button = GetComponent<Button>();
    }

    void Start() {
        MuteButtonInitialize();
    }

    public void MuteButtonInitialize() {
        PlayerPreferences data = saveLoad.LoadPreferences();
        
        if(data.muted) {
            button.image.sprite = muted;
            isMuted = true;
        } else {
            button.image.sprite = unmuted;
            isMuted = false;
        }
    }

    public void Mute() {
        if(isMuted) {
            AudioListener.pause = false;
            AudioListener.volume = 1f;
            button.image.sprite = unmuted;
            isMuted = false;
        } else {
            AudioListener.pause = true;
            AudioListener.volume = 0f;
            button.image.sprite = muted;
            isMuted = true;
        }

        saveLoad.SavePreferences(isMuted);

    }

}
