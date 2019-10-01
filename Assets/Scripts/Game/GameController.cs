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
    SaveLoad saveLoad;

    //Parameters
    [SerializeField] Canvas preStartScreenCanvas;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] Button pauseButton;
    [SerializeField] Canvas pauseCanvas;
    [SerializeField] float bonusTime = 10f;

    [Header("High Scores and Achievements")]
    [SerializeField] TextMeshProUGUI bestScoreText;
    [SerializeField] TextMeshProUGUI bestComboText;
    [SerializeField] GameObject newLabelScore;
    [SerializeField] GameObject newLabelCombo;

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
        saveLoad = FindObjectOfType<SaveLoad>();
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
        scoreHandler.AddFinalScores();
        int totalScore = scoreHandler.GetTotalScore();
        int totalCombo = scoreHandler.GetHighestComboAchieved();
        player.IsDead();
        pauseButton.gameObject.SetActive(false);
        gameOverHandler.GameOver(totalScore, totalCombo);
        CheckAchievementsAndNewBest(totalScore, totalCombo);
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
    private void CheckAchievementsAndNewBest(int score, int combo) {
        PlayerData data = saveLoad.Load();

        achievements.CheckAchievements(score, combo);

        if(score > data.highestScore) {
            newLabelScore.SetActive(true);
            Animator animator = newLabelScore.GetComponent<Animator>();
            animator.SetTrigger("Pulse");
        }
        if (combo > data.highestCombo) {
            newLabelCombo.SetActive(true);
            Animator animator = newLabelCombo.GetComponent<Animator>();
            animator.SetTrigger("Pulse");
        }
    }

    public void SaveProgress() {
        saveLoad.Save(this);
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

    public int GetHighScore() {
        return scoreHandler.GetTotalScore();
    }

    public int GetHighestCombo() {
        return scoreHandler.GetHighestComboAchieved();
    }

    //TODO: HIGH: Music = Mute or Unmute

}