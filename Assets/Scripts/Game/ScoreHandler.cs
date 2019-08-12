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
    [SerializeField] float chainTimerInitial = 5f;
    [SerializeField] int comboAdjustTreshold = 100;

    //State
    private int totalScore = 0;
    private int scoreMultiplier = 0;
    private int scoreToAdd = 0;
    private float chainTimer = 0;
    private float timer = 0; //TODO: Med-prio add timer bar UI
    private bool comboTimerAdjusted = false;

    void Start() {
        scoreText.SetText(totalScore.ToString());
        comboCounterText.SetText(scoreMultiplier.ToString());
        comboScoreText.SetText(scoreToAdd.ToString());
        chainTimer = chainTimerInitial;
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
        AdjustChainTimer();
        if (timer < chainTimer) {
            timer += Time.deltaTime;
            comboScoreText.SetText(scoreToAdd.ToString());
            comboCounterText.SetText(scoreMultiplier.ToString());
        } else {
            totalScore += scoreToAdd * scoreMultiplier;
            scoreMultiplier = 0;
            scoreToAdd = 0;
            comboTimerAdjusted = false;
            chainTimer = chainTimerInitial;
            comboScoreText.SetText(scoreToAdd.ToString());
            scoreText.SetText(totalScore.ToString());
        }
    }

    private void AdjustChainTimer() {
        if (scoreMultiplier > comboAdjustTreshold && !comboTimerAdjusted) {
            comboTimerAdjusted = true;
            chainTimer -= 2;
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
