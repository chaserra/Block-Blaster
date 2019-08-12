using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreHandler : MonoBehaviour {

    //References
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI comboCounterText;
    [SerializeField] TextMeshProUGUI comboScoreText;

    //Parameters
    [SerializeField] float chainTimer = 3f;

    //State
    private int totalScore = 0;
    private int scoreMultiplier = 0;
    private int scoreToAdd = 0;
    private float timer = 3f;

    void Start() {
        scoreText.SetText(totalScore.ToString());
        comboCounterText.SetText(scoreMultiplier.ToString());
        comboScoreText.SetText(scoreToAdd.ToString());
    }


    void Update() {
        DisplayComboText();
        ChainScore();
    }

    private void DisplayComboText() {
        if(scoreToAdd <= 0) {
            comboCounterText.gameObject.transform.parent.gameObject.SetActive(false);
            comboScoreText.enabled = false;
        } else {
            comboCounterText.gameObject.transform.parent.gameObject.SetActive(true);
            comboScoreText.enabled = true;
        }
    }

    private void ChainScore() {
        if(timer < chainTimer) {
            timer += Time.deltaTime;
            comboScoreText.SetText(scoreToAdd.ToString());
            comboCounterText.SetText(scoreMultiplier.ToString());
        } else {
            totalScore += scoreToAdd * scoreMultiplier;
            scoreMultiplier = 0;
            scoreToAdd = 0;
            comboScoreText.SetText(scoreToAdd.ToString());
            scoreText.SetText(totalScore.ToString());
        }
    }

    //Getters and Setters
    public void AddScore(int score) {
        timer = 0f;
        scoreMultiplier++;
        scoreToAdd += score;
    }

    public void AddFinalScores() {
        totalScore += scoreToAdd * scoreMultiplier;
    }

    public int GetTotalScore() {
        return totalScore;
    }
}
