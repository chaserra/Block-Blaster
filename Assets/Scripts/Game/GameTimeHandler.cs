﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameTimeHandler : MonoBehaviour {

    //Object References
    GameController gameController;
    [SerializeField] TextMeshProUGUI hoursText;
    [SerializeField] TextMeshProUGUI minutesText;
    [SerializeField] TextMeshProUGUI secondsText;

    //Parameters
    [SerializeField] private float secondsToShiftPhase = 20f;

    //State
    [Tooltip("Timer in seconds.")]
    [SerializeField] float timeRemaining = 155f;
    private int hours = 0;
    private int minutes = 0;
    private int seconds = 0;
    private bool hasTimeRemaining = true;
    private float currentTime = 0;

    void Start() {
        gameController = GetComponent<GameController>();
    }


    void Update() {
        TimerCountdown();
        NextPhaseCheck();
    }

    private void TimerCountdown() {
        if(timeRemaining > 0 && hasTimeRemaining) {
            timeRemaining -= Time.deltaTime;
            ConvertTimeValues();
        } else {
            OutOfTime();
        }
    }

    private void ConvertTimeValues() {
        hours = Mathf.FloorToInt(timeRemaining / 3600f);
        minutes = Mathf.FloorToInt(timeRemaining / 60f);
        seconds = Mathf.FloorToInt(timeRemaining - minutes * 60);

        if(minutes >= 60) {
            minutes -= 60;
        }

        if(timeRemaining < 1) { //prevents negative number from displaying
            hours = 0;
            minutes = 0;
            seconds = 0;
        }

        DisplayTimerText();
    }

    private void DisplayTimerText() {
        string formatHours = string.Format("{0:00}", hours);
        string formatMinutes = string.Format("{0:00}", minutes);
        string formatSeconds = string.Format("{0:00}", seconds);

        if(hours <= 0) {
            hoursText.gameObject.SetActive(false);
        } else {
            hoursText.gameObject.SetActive(true);
            hoursText.SetText(formatHours);
        }
        minutesText.SetText(formatMinutes);
        secondsText.SetText(formatSeconds);
    }

    private void OutOfTime() {
        if(hasTimeRemaining) {
            hasTimeRemaining = false;
            gameController.TimeOver();
        }
    }

    private void NextPhaseCheck() {
        if(currentTime < secondsToShiftPhase) {
            currentTime += Time.deltaTime;
        } else {
            currentTime = 0;
            gameController.NextPhase();
        }
    }

}
