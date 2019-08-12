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

    void Start() {
        player = FindObjectOfType<Player>();
        scoreHandler = GetComponent<ScoreHandler>();
        gameTimeHandler = GetComponent<GameTimeHandler>();
        gameOverHandler = GetComponent<GameOverHandler>();
    }

    public void AddScore(int score) {
        scoreHandler.AddScore(score);
    }

    public void TimeOver() {
        player.IsDead();
        scoreHandler.AddFinalScores();
        gameOverHandler.GameOver(scoreHandler.GetTotalScore());
    }

}
