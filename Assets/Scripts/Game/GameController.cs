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

    //State
    private bool gameStarted = false;
    private bool gamePaused = false;

    void Start() {
        player = FindObjectOfType<Player>();
        scoreHandler = GetComponent<ScoreHandler>();
        gameTimeHandler = GetComponent<GameTimeHandler>();
        gameOverHandler = GetComponent<GameOverHandler>();
        targetSpawner = FindObjectOfType<TargetSpawner>();
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

    //IEnumerator UnpauseGame() {
    //    pauseButton.gameObject.SetActive(true);
    //    pauseCanvas.gameObject.SetActive(false);
    //    yield return new WaitForSeconds(.2f);
    //    gamePaused = false;
    //}

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

    public void AddScore(int score) {
        scoreHandler.AddScore(score);
    }

    public void TimeOver() {
        player.IsDead();
        pauseButton.gameObject.SetActive(false);
        scoreHandler.AddFinalScores();
        gameOverHandler.GameOver(scoreHandler.GetTotalScore(), scoreHandler.GetHighestComboAchieved());
    }

    public void NextPhase() {
        targetSpawner.ShiftToNextPhase();
    }

    public void AddTimer() {
        gameTimeHandler.AddTime(bonusTime);
    }

    public bool IsGamePaused() {
        return gamePaused;
    }

    //TODO: HIGH: Add script for Music, High Score, and Achievement buttons
    //Display on a new canvas

}
