using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using TMPro;

public class PreGameStartHandler : MonoBehaviour {

    //Cache
    GameController gameController;

    //Object References
    [SerializeField] TextMeshProUGUI dragText;
    [SerializeField] TextMeshProUGUI releaseText;

    //State
    private bool hasStarted = false;
    private bool isTouching = false;

    void Start() {
        gameController = GetComponent<GameController>();
        releaseText.gameObject.SetActive(false);
    }


    void Update() {
        StartInstructions();
    }

    private void StartInstructions() {
        if(!hasStarted) {
            if (CrossPlatformInputManager.GetButtonDown("Fire1") || Input.touchCount > 0) {
                if (Input.touchCount > 0) {
                    isTouching = true;
                }
                dragText.gameObject.SetActive(false);
                releaseText.gameObject.SetActive(true);
            }

            if (CrossPlatformInputManager.GetButtonUp("Fire1") || isTouching) {
                gameController.StartGame();
                releaseText.gameObject.SetActive(false);
                hasStarted = true;
            }
        }
    }

}
