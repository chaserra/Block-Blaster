using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverHandler : MonoBehaviour {

    //Object References
    [SerializeField] Canvas mainGameCanvas;
    [SerializeField] Canvas gameOverCanvas;
    [SerializeField] TextMeshProUGUI totalScoreText;
    [SerializeField] TextMeshProUGUI highestComboText;

    void Awake() {
        gameOverCanvas.enabled = false;
    }

    public void GameOver(int totalScore, int highestCombo) {
        mainGameCanvas.enabled = false;
        gameOverCanvas.enabled = true;
        totalScoreText.SetText(totalScore.ToString("n0"));
        highestComboText.SetText(highestCombo.ToString("n0"));
        Time.timeScale = 0;

        //PC ONLY
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

}
