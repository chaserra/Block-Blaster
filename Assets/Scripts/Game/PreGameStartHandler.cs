﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityStandardAssets.CrossPlatformInput;
using TMPro;

public class PreGameStartHandler : MonoBehaviour {

    //Cache
    GameController gameController;

    //Object References
    [SerializeField] TextMeshProUGUI dragText;
    [SerializeField] TextMeshProUGUI releaseText;
    [SerializeField] TextMeshProUGUI highScoreField; //TODO: High: display on separate canvas
    [SerializeField] TextMeshProUGUI bestComboField; //TODO: High: display on separate canvas

    //Parameters
    float animSpeedInSec = 1f;

    //State
    private bool hasStarted = false;
    private bool isTouching = false;

    void Start() {
        gameController = GetComponent<GameController>();
        //releaseText.gameObject.SetActive(false);
    }


    void Update() {
        StartInstructions();
    }

    private void StartInstructions() {
        if(!hasStarted 
            && EventSystem.current.currentSelectedGameObject == null 
            && !gameController.IsInMenu()) 
        {
            if (CrossPlatformInputManager.GetButtonDown("Fire1") || Input.touchCount > 0) {
                if (Input.touchCount > 0) {
                    isTouching = true;
                }
                StartCoroutine(TextFade(dragText, 0));
                StartCoroutine(TextFade(releaseText, 1));
            }

            if (CrossPlatformInputManager.GetButtonUp("Fire1") || isTouching) {
                gameController.StartGame();
                StartCoroutine(TextFade(releaseText, 0));
                hasStarted = true;
            }
        }
    }

    IEnumerator TextFade(TextMeshProUGUI text, float alphaValue) {
        Color currentColor = text.color;
        Color colorAlpha = text.color;
        colorAlpha.a = alphaValue;

        float oldAnimSpeedInSec = animSpeedInSec;
        float counter = 0;

        while (counter < oldAnimSpeedInSec) {
            counter += Time.deltaTime;
            text.color = Color.Lerp(currentColor, colorAlpha, counter / oldAnimSpeedInSec);
            yield return null;
        }
        text.color = colorAlpha;
        yield return null;
    }

}