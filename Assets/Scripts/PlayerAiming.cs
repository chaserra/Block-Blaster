using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerAiming : MonoBehaviour {

    //Cache
    [SerializeField] Transform turret;

    //Parameters
    [SerializeField] float speed = 100f;

    void Start() {
        
    }


    void Update() {
        RotateTurret();
    }

    private void RotateTurret() {
        if(CrossPlatformInputManager.GetButton("Fire1")) {
            float xRotate = CrossPlatformInputManager.GetAxis("Mouse Y") * speed * Time.deltaTime;
            float yRotate = CrossPlatformInputManager.GetAxis("Mouse X") * speed * Time.deltaTime;
            turret.transform.Rotate(-xRotate, yRotate, 0);

            float clampedX = turret.transform.localEulerAngles.x;
            clampedX = Mathf.Clamp((clampedX <= 180) ? clampedX : -(360 - clampedX), -45f, -11f);
            float clampedY = turret.transform.localEulerAngles.y;
            clampedY = Mathf.Clamp((clampedY <= 180) ? clampedY : -(360 - clampedY), -30f, 30f);

            turret.transform.rotation = Quaternion.Euler(clampedX, clampedY, 0);
        }
    }
}
