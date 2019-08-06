using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerAiming : MonoBehaviour {

    //Cache
    [SerializeField] Transform turret;
    [SerializeField] GameObject firePoint;

    //Parameters
    [SerializeField] GameObject tankBullet;
    [SerializeField] float rotateSpeed = 150f;
    [Tooltip("Higher value means faster cooldown.")]
    [SerializeField] float rateOfFire = .6f;

    //State
    private float timeToFire = 0;

    void Start() {
        
    }


    void Update() {
        RotateAndShoot();
    }

    private void RotateAndShoot() {
        if(CrossPlatformInputManager.GetButton("Fire1")) {
            float xRotate = CrossPlatformInputManager.GetAxis("Mouse Y") * rotateSpeed * Time.deltaTime;
            float yRotate = CrossPlatformInputManager.GetAxis("Mouse X") * rotateSpeed * Time.deltaTime;
            turret.transform.Rotate(-xRotate, yRotate, 0);

            float clampedX = turret.transform.localEulerAngles.x;
            clampedX = Mathf.Clamp((clampedX <= 180) ? clampedX : -(360 - clampedX), -45f, -3f);
            float clampedY = turret.transform.localEulerAngles.y;
            clampedY = Mathf.Clamp((clampedY <= 180) ? clampedY : -(360 - clampedY), -45f, 45f);

            turret.transform.rotation = Quaternion.Euler(clampedX, clampedY, 0);
        }

        if(CrossPlatformInputManager.GetButtonUp("Fire1")) {
            if (Time.time >= timeToFire) {
                timeToFire = Time.time + 1 / rateOfFire;
                //TODO: Low-prio: Add cooldown bar UI
                GameObject bullet = Instantiate(tankBullet, firePoint.transform.position, Quaternion.identity);
                bullet.transform.localRotation = turret.rotation;
                Destroy(bullet, 10f);
                //TODO: Med-prio: Add turret retracting animation (for impact visuals)
            } else {
                Debug.Log("Can't fire yet!"); //TODO: Low-prio: Remove or add sound effect
            }
        }
    }
}
