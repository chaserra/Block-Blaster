using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TitleScreenController : MonoBehaviour {

    SaveLoad saveLoad;
    AudioManager audioManager;

    [Header("High Scores and Achievements")]
    [SerializeField] TextMeshProUGUI bestScoreText;
    [SerializeField] TextMeshProUGUI bestComboText;

    [SerializeField] Image achievement01Cover;
    [SerializeField] Image achievement02Cover;
    [SerializeField] Image achievement03Cover;
    [SerializeField] Image achievement04Cover;
    [SerializeField] Image achievement05Cover;
    [SerializeField] Image achievement06Cover;
    [SerializeField] Image achievement07Cover;
    [SerializeField] Image achievement08Cover;
    [SerializeField] Image achievement09Cover;
    [SerializeField] Image achievement10Cover;
    [SerializeField] Image achievement11Cover;
    [SerializeField] Image achievement12Cover;
    [SerializeField] Image achievement13Cover;
    [SerializeField] Image achievement14Cover;

    void Awake() {
        saveLoad = FindObjectOfType<SaveLoad>();
        audioManager = FindObjectOfType<AudioManager>();
    }

    void Start() {
        LoadProgress();
        audioManager.Stop("Main Music");
        audioManager.Play("Title Theme");
    }

    public void LoadProgress() {
        PlayerData data = saveLoad.Load();

        bestScoreText.SetText(data.highestScore.ToString("n0"));
        bestComboText.SetText(data.highestCombo.ToString("n0"));

        //Check if Achievements are unlocked
        if (data.ach01Obtained) {
            Color imageColor = achievement01Cover.color;
            imageColor.a = 0;
            achievement01Cover.color = imageColor;
        }
        if (data.ach02Obtained) {
            Color imageColor = achievement02Cover.color;
            imageColor.a = 0;
            achievement02Cover.color = imageColor;
        }
        if (data.ach03Obtained) {
            Color imageColor = achievement03Cover.color;
            imageColor.a = 0;
            achievement03Cover.color = imageColor;
        }
        if (data.ach04Obtained) {
            Color imageColor = achievement04Cover.color;
            imageColor.a = 0;
            achievement04Cover.color = imageColor;
        }
        if (data.ach05Obtained) {
            Color imageColor = achievement05Cover.color;
            imageColor.a = 0;
            achievement05Cover.color = imageColor;
        }
        if (data.ach06Obtained) {
            Color imageColor = achievement06Cover.color;
            imageColor.a = 0;
            achievement06Cover.color = imageColor;
        }
        if (data.ach07Obtained) {
            Color imageColor = achievement07Cover.color;
            imageColor.a = 0;
            achievement07Cover.color = imageColor;
        }
        if (data.ach08Obtained) {
            Color imageColor = achievement08Cover.color;
            imageColor.a = 0;
            achievement08Cover.color = imageColor;
        }
        if (data.ach09Obtained) {
            Color imageColor = achievement09Cover.color;
            imageColor.a = 0;
            achievement09Cover.color = imageColor;
        }
        if (data.ach10Obtained) {
            Color imageColor = achievement10Cover.color;
            imageColor.a = 0;
            achievement10Cover.color = imageColor;
        }
        if (data.ach11Obtained) {
            Color imageColor = achievement11Cover.color;
            imageColor.a = 0;
            achievement11Cover.color = imageColor;
        }
        if (data.ach12Obtained) {
            Color imageColor = achievement12Cover.color;
            imageColor.a = 0;
            achievement12Cover.color = imageColor;
        }
        if (data.ach13Obtained) {
            Color imageColor = achievement13Cover.color;
            imageColor.a = 0;
            achievement13Cover.color = imageColor;
        }
        if (data.ach14Obtained) {
            Color imageColor = achievement14Cover.color;
            imageColor.a = 0;
            achievement14Cover.color = imageColor;
        }
    }

}
