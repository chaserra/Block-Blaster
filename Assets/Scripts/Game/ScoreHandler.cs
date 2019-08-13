using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ScoreHandler : MonoBehaviour {

    //Cache
    GameController gameController;

    //References
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI comboCounterText;
    [SerializeField] TextMeshProUGUI comboScoreText;

    //Parameters
    [SerializeField] float chainTimerInitial = 5f;
    [SerializeField] int comboAdjustTreshold = 100;
    [SerializeField] int timeAdjustComboNeededInitial = 100;

    //State
    private int totalScore = 0;
    private int comboMultiplier = 0;
    private int scoreToAdd = 0;

    private float comboTimer = 0;
    private float timer = 0; //TODO: Med-prio add timer bar UI
    private bool comboTimerAdjusted = false;

    private int timeAdjustComboNeeded = 0;
    private bool timeAdjusted = false;

    private int highestComboAchieved = 0;

    void Start() {
        gameController = GetComponent<GameController>();
        scoreText.SetText(totalScore.ToString());
        comboCounterText.SetText(comboMultiplier.ToString());
        comboScoreText.SetText(scoreToAdd.ToString());
        comboTimer = chainTimerInitial;
        timeAdjustComboNeeded = timeAdjustComboNeededInitial;
    }


    void Update() {
        DisplayComboText();
        ChainScore();
        CheckForBonusTime();
        CheckHighestComboAchieved();
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
        if (timer < comboTimer) {
            timer += Time.deltaTime;
            comboScoreText.SetText(scoreToAdd.ToString());
            comboCounterText.SetText(comboMultiplier.ToString());
        } else {
            totalScore += scoreToAdd * comboMultiplier;
            comboMultiplier = 0;
            scoreToAdd = 0;
            comboTimerAdjusted = false;
            comboTimer = chainTimerInitial;
            timeAdjusted = false;
            timeAdjustComboNeeded = timeAdjustComboNeededInitial;
            comboScoreText.SetText(scoreToAdd.ToString());
            scoreText.SetText(totalScore.ToString());
        }
    }

    private void AdjustChainTimer() {
        if (comboMultiplier > comboAdjustTreshold && !comboTimerAdjusted) {
            comboTimerAdjusted = true;
            comboTimer -= 2;
        }
    }

    private void CheckForBonusTime() {
        if(comboMultiplier >= timeAdjustComboNeeded && !timeAdjusted) {
            timeAdjusted = true; //Failsafe. Prevents double time adding.
            gameController.AddTimer();
            timeAdjustComboNeeded += 50;
            timeAdjusted = false; //Failsafe. Prevents double time adding.
        }
    }

    private void CheckHighestComboAchieved() {
        if(comboMultiplier > highestComboAchieved) {
            highestComboAchieved = comboMultiplier;
        }
    }

    //Getters and Setters
    public void AddScore(int score) {
        timer = 0f;
        comboMultiplier++;
        scoreToAdd += score;
    }

    public void AddFinalScores() {
        totalScore += scoreToAdd * comboMultiplier;
    }

    public int GetTotalScore() {
        return totalScore;
    }

    public int GetHighestComboAchieved() {
        return highestComboAchieved;
    }
}
