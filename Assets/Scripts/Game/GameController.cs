using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class GameController : MonoBehaviour {

    //Cache
    Player player;
    ScoreHandler scoreHandler;
    GameTimeHandler gameTimeHandler;
    GameOverHandler gameOverHandler;
    TargetSpawner targetSpawner;
    Achievements achievements;

    //Parameters
    [SerializeField] Canvas preStartScreenCanvas;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] Button pauseButton;
    [SerializeField] Canvas pauseCanvas;
    [SerializeField] float bonusTime = 10f;

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

    //State
    private bool gameStarted = false;
    private bool gamePaused = false;
    private bool isInMenus = false;

    void Awake() {
        player = FindObjectOfType<Player>();
        scoreHandler = GetComponent<ScoreHandler>();
        gameTimeHandler = GetComponent<GameTimeHandler>();
        gameOverHandler = GetComponent<GameOverHandler>();
        targetSpawner = FindObjectOfType<TargetSpawner>();
        achievements = GetComponent<Achievements>();
    }

    void Start() {
        LoadProgress();
        scoreText.gameObject.SetActive(false);
        pauseCanvas.gameObject.SetActive(false);
        pauseButton.gameObject.SetActive(false);
    }

    public void PauseGame() {
        if(gamePaused) {
            //Unpause
            Time.timeScale = 1f;
            //StartCoroutine(UnpauseGame());
            pauseButton.gameObject.SetActive(true);
            pauseCanvas.gameObject.SetActive(false);
            gamePaused = false;
        } else {
            //Pause
            pauseCanvas.gameObject.SetActive(true);
            pauseButton.gameObject.SetActive(false);
            gamePaused = true;
            Time.timeScale = 0f;
        }
    }

    //TODO: High: Remove this if pause works okay in mobile
    //IEnumerator UnpauseGame() {
    //    pauseButton.gameObject.SetActive(true);
    //    pauseCanvas.gameObject.SetActive(false);
    //    yield return new WaitForSeconds(.2f);
    //    gamePaused = false;
    //}

    //GAME START
    public void StartGame() {
        pauseButton.gameObject.SetActive(true);
        preStartScreenCanvas.gameObject.SetActive(false);
        StartCoroutine(StartGameWithDelay());
    }

    IEnumerator StartGameWithDelay() {
        scoreText.gameObject.SetActive(true);
        yield return new WaitForSeconds(3f);
        gameStarted = true;
    }

    public bool GameHasStarted() {
        return gameStarted;
    }


    //IN-GAME
    public void AddScore(int score) {
        scoreHandler.AddScore(score);
    }

    public void NextPhase() {
        targetSpawner.ShiftToNextPhase();
    }

    public void AddTimer() {
        gameTimeHandler.AddTime(bonusTime);
    }

    public void TimeOver() {
        int totalScore = scoreHandler.GetTotalScore();
        int totalCombo = scoreHandler.GetHighestComboAchieved();
        player.IsDead();
        pauseButton.gameObject.SetActive(false);
        scoreHandler.AddFinalScores();
        gameOverHandler.GameOver(totalScore, totalCombo);
        CheckAchievements(totalScore, totalCombo);
        SaveProgress();
        LoadProgress();
    }

    //GETTERS AND SETTERS
    public bool IsGamePaused() {
        return gamePaused;
    }

    public void SetIsInMenu(bool expression) {
        isInMenus = expression;
    }

    public bool IsInMenu() {
        return isInMenus;
    }

    //Save and Load
    private void CheckAchievements(int score, int combo) {
        achievements.CheckAchievements(score, combo);
    }

    public void SaveProgress() {
        SaveLoad.Save(this);
    }

    public void LoadProgress() {
        PlayerData data = SaveLoad.Load();

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

    public int GetHighScore() {
        return scoreHandler.GetTotalScore();
    }

    public int GetHighestCombo() {
        return scoreHandler.GetHighestComboAchieved();
    }

    //TODO: HIGH: Add script for Music, and Achievement buttons
    //Music = Mute or Unmute

}