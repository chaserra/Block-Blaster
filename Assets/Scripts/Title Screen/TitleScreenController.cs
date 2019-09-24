using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TitleScreenController : MonoBehaviour {

    SaveLoad saveLoad;

    [Header("High Scores and Achievements")]
    [SerializeField] TextMeshProUGUI bestScoreText;
    [SerializeField] TextMeshProUGUI bestComboText;

    //TODO: High - Change these to image badges. Transparent if not obtained, solid if obtained
    [SerializeField] TextMeshProUGUI ach01Text;
    [SerializeField] TextMeshProUGUI ach02Text;
    [SerializeField] TextMeshProUGUI ach03Text;
    [SerializeField] TextMeshProUGUI ach04Text;
    [SerializeField] TextMeshProUGUI ach05Text;
    [SerializeField] TextMeshProUGUI ach06Text;
    [SerializeField] TextMeshProUGUI ach07Text;
    [SerializeField] TextMeshProUGUI ach08Text;
    [SerializeField] TextMeshProUGUI ach09Text;
    [SerializeField] TextMeshProUGUI ach10Text;
    [SerializeField] TextMeshProUGUI ach11Text;
    [SerializeField] TextMeshProUGUI ach12Text;
    [SerializeField] TextMeshProUGUI ach13Text;
    [SerializeField] TextMeshProUGUI ach14Text;

    void Awake() {
        saveLoad = FindObjectOfType<SaveLoad>();
    }

    void Start() {
        LoadProgress();
    }

    public void LoadProgress() {
        PlayerData data = saveLoad.Load();

        bestScoreText.SetText(data.highestScore.ToString("n0"));
        bestComboText.SetText(data.highestCombo.ToString("n0"));

        //TODO: High - Change these to image badges. Transparent if not obtained, solid if obtained
        ach01Text.SetText("01: " + data.ach01Obtained.ToString());
        ach02Text.SetText("02: " + data.ach02Obtained.ToString());
        ach03Text.SetText("03: " + data.ach03Obtained.ToString());
        ach04Text.SetText("04: " + data.ach04Obtained.ToString());
        ach05Text.SetText("05: " + data.ach05Obtained.ToString());
        ach06Text.SetText("06: " + data.ach06Obtained.ToString());
        ach07Text.SetText("07: " + data.ach07Obtained.ToString());
        ach08Text.SetText("08: " + data.ach08Obtained.ToString());
        ach09Text.SetText("09: " + data.ach09Obtained.ToString());
        ach10Text.SetText("10: " + data.ach10Obtained.ToString());
        ach11Text.SetText("11: " + data.ach11Obtained.ToString());
        ach12Text.SetText("12: " + data.ach12Obtained.ToString());
        ach13Text.SetText("13: " + data.ach13Obtained.ToString());
        ach14Text.SetText("14: " + data.ach14Obtained.ToString());
    }

}
