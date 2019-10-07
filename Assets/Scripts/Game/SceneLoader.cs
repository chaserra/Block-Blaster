using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {

    public void PlayGame() {
        AdManager.instance.DestroyBanner();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void RestartLevel() {
        AdManager.instance.DestroyBanner();
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu() {
        AdManager.instance.DestroyBanner();
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }

    public void QuitGame() {
        Application.Quit();
    }

}
