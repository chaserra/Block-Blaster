using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityStandardAssets.CrossPlatformInput;

public class CameraMovement : MonoBehaviour {

    //Cache
    GameController gameController;
    [Header("Object References")]
    [SerializeField] GameObject cameraPivot;
    [SerializeField] GameObject turretBase;

    //Parameters
    [Header("Camera Rotation")]
    [SerializeField] float rotateSpeed = 90f;
    [SerializeField] float cameraRotationAngle = 35f;
    [SerializeField] float turretRotationTreshold = 28f;


    [Header("Camera Panning")]
    [SerializeField] float xPanLimit = .8f;
    [SerializeField] float panSpeed = 1f;

    //State
    float rotationTreshold = 0;

    void Start() {
        gameController = GameController.gcReference;
        rotationTreshold = turretRotationTreshold;
    }

    void Update() {
        if(EventSystem.current.currentSelectedGameObject == null && !gameController.IsInMenu()) {
            CheckAngleAndRotate();
            PanCamera();
        }
    }

    private void CheckAngleAndRotate() {

        float yAngle = turretBase.transform.eulerAngles.y;
        var angleModifier = (yAngle <= 180) ? yAngle : -(360 - yAngle);

        if (angleModifier > -rotationTreshold && angleModifier < rotationTreshold) {
            rotationTreshold = turretRotationTreshold;
            RotateCamera(0f, -1f);
        }

        if (angleModifier > turretRotationTreshold) {
            rotationTreshold = turretRotationTreshold - 9f;
            RotateCamera(cameraRotationAngle, 1f);
        }

        if(angleModifier < -turretRotationTreshold) {
            rotationTreshold = turretRotationTreshold - 9f;
            RotateCamera(cameraRotationAngle, -1f);
        }
    }

    private void RotateCamera(float rotationAngle, float multiplier) {
        Vector3 rotatedCamera = new Vector3(0, rotationAngle * multiplier, 0);
        var rotatedCameraQ = Quaternion.Euler(rotatedCamera);
        var rotateTime = Time.deltaTime * rotateSpeed;
        var newRotation = Quaternion.RotateTowards(
            cameraPivot.transform.rotation, 
            rotatedCameraQ, 
            rotateTime
        );

        cameraPivot.transform.rotation = newRotation;
    }

    private void PanCamera() {
        if (CrossPlatformInputManager.GetButton("Fire1") || Input.touchCount > 0) {
            float xPan = CrossPlatformInputManager.GetAxis("Mouse X") * panSpeed * Time.deltaTime;
            if(Input.touchCount > 0) {
                xPan = Input.touches[0].deltaPosition.x * panSpeed * Time.deltaTime;
            }
            cameraPivot.transform.position = Vector3.Lerp(
                cameraPivot.transform.position, 
                new Vector3(cameraPivot.transform.position.x + xPan, cameraPivot.transform.position.y, cameraPivot.transform.position.z), 
                Time.deltaTime
            );

            float clampedX = Mathf.Clamp(cameraPivot.transform.localPosition.x, -xPanLimit, xPanLimit);

            cameraPivot.transform.localPosition = new Vector3(
                clampedX, 
                cameraPivot.transform.localPosition.y,
                cameraPivot.transform.localPosition.z
            );
        }
    }

}
