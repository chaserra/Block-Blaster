﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerAiming : MonoBehaviour {

    //Cache
    private Player player;
    GameController gameController;
    [Header("Object References")]
    [SerializeField] Transform turretBase;
    [SerializeField] Transform turretGun;
    [SerializeField] GameObject firePoint;
    [SerializeField] GameObject tankBullet;
    [SerializeField] Image reloadingHUD;

    //Parameters
    [Header("Clamp Rotation")]
    [SerializeField] float baseRotateClamp = 65f;
    [SerializeField] float gunMaxLookUp = 45f;
    [SerializeField] float gunMaxLookDown = 3f;

    [Header("Config")]
    [SerializeField] float rotateSpeed = 15f;
    [SerializeField] float rateOfFire = 1f;

    //State
    private float timeToFire = 0;
    private bool hasReloaded = true;
    private bool isTouching = false;

    void Start() {
        player = GetComponent<Player>();
        gameController = FindObjectOfType<GameController>();
        timeToFire = rateOfFire;
    }


    void Update() {
        if(player.IsAlive() && !gameController.IsGamePaused()) {
            RotateAndShoot();
            Reload();
        }
    }

    private void RotateAndShoot() {
        if(EventSystem.current.currentSelectedGameObject == null) {
            if (CrossPlatformInputManager.GetButton("Fire1") || Input.touchCount > 0) {
                float xRotate = CrossPlatformInputManager.GetAxis("Mouse Y") * rotateSpeed * Time.deltaTime;
                float yRotate = CrossPlatformInputManager.GetAxis("Mouse X") * rotateSpeed * Time.deltaTime;
                if (Input.touchCount > 0) {
                    isTouching = true;
                    xRotate = Input.touches[0].deltaPosition.y * rotateSpeed * Time.deltaTime;
                    yRotate = Input.touches[0].deltaPosition.x * rotateSpeed * Time.deltaTime;
                }
                turretBase.transform.Rotate(0, yRotate, 0);
                turretGun.transform.Rotate(-xRotate, 0, 0);

                float clampedX = turretGun.transform.localEulerAngles.x;
                clampedX = Mathf.Clamp((clampedX <= 180) ? clampedX : -(360 - clampedX), -gunMaxLookUp, -gunMaxLookDown);
                float clampedY = turretBase.transform.localEulerAngles.y;
                clampedY = Mathf.Clamp((clampedY <= 180) ? clampedY : -(360 - clampedY), -baseRotateClamp, baseRotateClamp);

                turretBase.transform.rotation = Quaternion.Euler(0, clampedY, 0);
                turretGun.transform.rotation = Quaternion.Euler(clampedX, clampedY, 0);
            }

            if (CrossPlatformInputManager.GetButtonUp("Fire1") || isTouching) {
                if (Input.touchCount <= 0) {
                    if (hasReloaded) {
                        GameObject bullet = Instantiate(tankBullet, firePoint.transform.position, Quaternion.identity);
                        bullet.transform.localRotation = turretGun.rotation;
                        Destroy(bullet, 8f);
                        hasReloaded = false;
                        timeToFire = 0;
                        //TODO: Med-prio: Add turret retracting animation (for impact visuals)
                    } else {
                        Debug.Log("Can't fire yet!"); //TODO: Low-prio: Add sound effect and no bullet image
                    }
                    isTouching = false;
                }
            }
        }
    }

    private void Reload() {
        if(timeToFire < rateOfFire) {
            timeToFire += Time.deltaTime;
            reloadingHUD.gameObject.transform.parent.gameObject.SetActive(true);
            reloadingHUD.fillAmount = timeToFire / rateOfFire;
            if(reloadingHUD.fillAmount < .5f) {
                reloadingHUD.color = Color.red;
            } else if(reloadingHUD.fillAmount < .75f) {
                reloadingHUD.color = Color.yellow;
            } else if (reloadingHUD.fillAmount < .95f) {
                reloadingHUD.color = Color.green;
            }
        } else {
            hasReloaded = true;
            reloadingHUD.gameObject.transform.parent.gameObject.SetActive(false);
        }
    }

}
