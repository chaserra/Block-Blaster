using System;
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
    Animator animator;
    [Header("Object References")]
    [SerializeField] Transform turretBase;
    [SerializeField] Transform turretGun;
    [SerializeField] GameObject firePoint;
    [SerializeField] GameObject tankBullet;
    [SerializeField] GameObject smokeBlastVFX;
    [SerializeField] Image reloadingHUD;
    [SerializeField] Animator ammoHUDanimator;

    //Parameters
    [Header("Clamp Rotation")]
    [SerializeField] float baseRotateClamp = 65f;
    [SerializeField] float gunMaxLookUp = 50f;
    [SerializeField] float gunMaxLookDown = 3f;

    [Header("Config")]
    [SerializeField] float rotateSpeed = 3.2f;
    [SerializeField] float rateOfFire = 1.7f;

    //State
    private float timeToFire = 0;
    private bool hasReloaded = true;
    private bool isTouching = false;
    private ParticleSystem[] smokeBlast;

    void Start() {
        player = GetComponent<Player>();
        gameController = GameController.gcReference;
        animator = GetComponent<Animator>();
        smokeBlast = smokeBlastVFX.GetComponentsInChildren<ParticleSystem>();
        timeToFire = rateOfFire;
    }


    void Update() {
        if(player.IsAlive() && !gameController.IsGamePaused() && !gameController.IsInMenu()) {
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
                        for(int x = 0; x < smokeBlast.Length; x++) {
                            smokeBlast[x].Play();
                        }
                        animator.SetTrigger("Retract");
                        gameController.PlayAudio("Shoot");
                        hasReloaded = false;
                        timeToFire = 0;
                    } else {
                        gameController.PlayAudio("No Ammo");
                        ammoHUDanimator.SetBool("Flash Icon", true);
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
            if(!hasReloaded) {
                gameController.PlayAudio("Reloading");
            }
            hasReloaded = true;
            ammoHUDanimator.SetBool("Flash Icon", false);
            reloadingHUD.gameObject.transform.parent.gameObject.SetActive(false);
        }
    }

}
