using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour {

    //Cache
    Player player;
    ScoreHandler scoreHandler;
    GameTimeHandler gameTimeHandler;
    GameOverHandler gameOverHandler;
    TargetSpawner targetSpawner;

    //Parameters
    [SerializeField] Canvas preStartScreenCanvas;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] Button pauseButton;
    [SerializeField] Canvas pauseCanvas;
    [SerializeField] float bonusTime = 10f;

    [Header("High Scores and Achievements")]
    [SerializeField] TextMeshProUGUI bestScoreText;
    [SerializeField] TextMeshProUGUI bestComboText;

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
        player.IsDead();
        pauseButton.gameObject.SetActive(false);
        scoreHandler.AddFinalScores();
        gameOverHandler.GameOver(scoreHandler.GetTotalScore(), scoreHandler.GetHighestComboAchieved());
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
    public void SaveProgress() {
        SaveLoad.Save(this);
    }

    public void LoadProgress() {
        PlayerData data = SaveLoad.Load();

        bestScoreText.SetText(data.highestScore.ToString("n0"));
        bestComboText.SetText(data.highestCombo.ToString("n0"));
    }

    public int GetHighScore() {
        return scoreHandler.GetTotalScore();
    }

    public int GetHighestCombo() {
        return scoreHandler.GetHighestComboAchieved();
    }

    //TODO: HIGH: Add script for Music, and Achievement buttons
    //Music = Mute or Unmute
    //Achievements = Display achievements

}