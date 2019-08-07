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
            turretBase.transform.Rotate(0, yRotate, 0);
            turretGun.transform.Rotate(-xRotate, 0, 0);

            float clampedX = turretGun.transform.localEulerAngles.x;
            clampedX = Mathf.Clamp((clampedX <= 180) ? clampedX : -(360 - clampedX), -gunMaxLookUp, -gunMaxLookDown);
            float clampedY = turretBase.transform.localEulerAngles.y;
            clampedY = Mathf.Clamp((clampedY <= 180) ? clampedY : -(360 - clampedY), -baseRotateClamp, baseRotateClamp);

            turretBase.transform.rotation = Quaternion.Euler(0, clampedY, 0);
            turretGun.transform.rotation = Quaternion.Euler(clampedX, clampedY, 0);
        }

        if(CrossPlatformInputManager.GetButtonUp("Fire1")) {
            if (Time.time >= timeToFire) {
                timeToFire = Time.time + 1 / rateOfFire;
                //TODO: Low-prio: Add cooldown bar UI
                GameObject bullet = Instantiate(tankBullet, firePoint.transform.position, Quaternion.identity);
                bullet.transform.localRotation = turretGun.rotation;
                Destroy(bullet, 10f);
                //TODO: Med-prio: Add turret retracting animation (for impact visuals)
            } else {
                Debug.Log("Can't fire yet!"); //TODO: Low-prio: Remove or add sound effect
            }
        }
    }
}
