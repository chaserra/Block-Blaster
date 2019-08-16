using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {

    public void RestartLevel() {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu() {
        //TODO: HIGH: Load Main Menu scene OR high scores if main menu is removed from the game
        //Time.timeScale = 1;
    }

    public void QuitGame() {
        Application.Quit();
    }

}
