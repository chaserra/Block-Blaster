using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerAiming : MonoBehaviour {

    //Cache
    [Header("Object References")]
    [SerializeField] Transform turretBase;
    [SerializeField] Transform turretGun;
    [SerializeField] GameObject firePoint;
    [SerializeField] GameObject tankBullet;

    //Parameters
    [Header("Clamp Rotation")]
    [SerializeField] float baseRotateClamp = 65f;
    [SerializeField] float gunMaxLookUp = 45f;
    [SerializeField] float gunMaxLookDown = 3f;

    [Header("Config")]
    [SerializeField] float rotateSpeed = 15f;
    [Tooltip("Higher value means faster cooldown.")]
    [SerializeField] float rateOfFire = .6f;

    //State
    private float timeToFire = 0;
    private bool isTouching = false;

    void Start() {
        
    }


    void Update() {
        RotateAndShoot();
    }

    private void RotateAndShoot() {
        if(CrossPlatformInputManager.GetButton("Fire1") || Input.touchCount > 0) {
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

        if(CrossPlatformInputManager.GetButtonUp("Fire1") || isTouching) {
            if(Input.touchCount <= 0) {
                if (Time.time >= timeToFire) {
                    timeToFire = Time.time + 1 / rateOfFire;
                    //TODO: Low-prio: Add cooldown bar UI
                    GameObject bullet = Instantiate(tankBullet, firePoint.transform.position, Quaternion.identity);
                    bullet.transform.localRotation = turretGun.rotation;
                    Destroy(bullet, 8f);
                    //TODO: Med-prio: Add turret retracting animation (for impact visuals)
                } else {
                    Debug.Log("Can't fire yet!"); //TODO: Low-prio: Remove comment or add sound effect
                }
                isTouching = false;
            }
        }
    }
}
