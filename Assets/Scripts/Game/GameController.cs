using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour {

    //Cache
    Player player;
    ScoreHandler scoreHandler;
    GameTimeHandler gameTimeHandler;
    GameOverHandler gameOverHandler;
    TargetSpawner targetSpawner;

    //Parameters
    [SerializeField] float bonusTime = 10f;

    void Start() {
        player = FindObjectOfType<Player>();
        scoreHandler = GetComponent<ScoreHandler>();
        gameTimeHandler = GetComponent<GameTimeHandler>();
        gameOverHandler = GetComponent<GameOverHandler>();
        targetSpawner = FindObjectOfType<TargetSpawner>();
    }

    public void AddScore(int score) {
        scoreHandler.AddScore(score);
    }

    public void TimeOver() {
        player.IsDead();
        scoreHandler.AddFinalScores();
        gameOverHandler.GameOver(scoreHandler.GetTotalScore(), scoreHandler.GetHighestComboAchieved());
    }

    public void NextPhase() {
        targetSpawner.ShiftToNextPhase();
    }

    public void AddTimer() {
        gameTimeHandler.AddTime(bonusTime);
    }

}
