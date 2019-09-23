using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ScoreHandler : MonoBehaviour {

    //Cache
    GameController gameController;
    Image comboTimerBG;
    Animator comboCounterAnimator;

    //References
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI comboCounterText;
    [SerializeField] TextMeshProUGUI comboScoreText;
    [SerializeField] Image comboTimerUI;

    //Parameters
    [SerializeField] float chainTimerInitial = 5f;
    [SerializeField] int comboAdjustTreshold = 100;
    [SerializeField] int timeAdjustComboNeededInitial = 50;
    [SerializeField] int addToComboNeeded = 50;

    //State
    private int totalScore = 0;
    private int comboMultiplier = 0;
    private int scoreToAdd = 0;

    private float comboTimer = 0;
    private float timer = 0;
    private bool comboTimerAdjusted = false;

    private int timeAdjustComboNeeded = 0;
    private bool timeAdjusted = false;

    private int highestComboAchieved = 0;

    void Start() {
        gameController = GetComponent<GameController>();
        comboTimerBG = comboTimerUI.gameObject.transform.parent.gameObject.GetComponent<Image>();
        comboCounterAnimator = comboCounterText.gameObject.GetComponent<Animator>();
        scoreText.SetText(totalScore.ToString());
        comboCounterText.SetText(comboMultiplier.ToString());
        comboScoreText.SetText(scoreToAdd.ToString());
        comboTimer = chainTimerInitial;
        timer = comboTimer;
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
            comboTimerUI.gameObject.transform.parent.gameObject.SetActive(true);
            comboTimerUI.fillAmount = timer / comboTimer;
            if (comboTimerUI.fillAmount > .75f) {
                comboTimerBG.color = Color.red;
            } else if (comboTimerUI.fillAmount >= .4f) {
                comboTimerBG.color = Color.yellow;
            } else if (comboTimerUI.fillAmount < .4f) {
                comboTimerBG.color = Color.green;
            }

        } else {
            totalScore += scoreToAdd * comboMultiplier;
            comboMultiplier = 0;
            scoreToAdd = 0;
            comboTimerAdjusted = false;
            comboTimer = chainTimerInitial;
            timer = comboTimer;
            timeAdjusted = false;
            timeAdjustComboNeeded = timeAdjustComboNeededInitial;
            comboTimerUI.gameObject.transform.parent.gameObject.SetActive(false);
            comboScoreText.SetText(scoreToAdd.ToString());
            scoreText.SetText(totalScore.ToString("n0"));
        }
    }

    private void AdjustChainTimer() {
        if (comboMultiplier > comboAdjustTreshold && !comboTimerAdjusted) {
            comboTimerAdjusted = true;
            comboTimer -= 1f;
        }
    }

    private void CheckForBonusTime() {
        if(comboMultiplier >= timeAdjustComboNeeded && !timeAdjusted) {
            timeAdjusted = true; //Failsafe. Prevents double time adding.
            gameController.AddTimer();
            timeAdjustComboNeeded += addToComboNeeded;
            timeAdjusted = false; //Failsafe exit. Prevents double time adding.
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
        comboCounterAnimator.SetTrigger("Quick Drop");
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
