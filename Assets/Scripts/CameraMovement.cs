using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    //Cache
    [Header("Object References")]
    [SerializeField] GameObject cameraPivot;
    [SerializeField] GameObject turretBase;

    //Parameters
    [Header("Camera Rotation")]
    [SerializeField] float rotateSpeed = 90f;
    [SerializeField] float cameraRotationAngle = 35f;
    [SerializeField] float turretRotationTreshold = 35f;


    [Header("Camera Panning")]
    [SerializeField] float panX = .45f;
    [SerializeField] float panSpeed = 5f;

    void Start() {
        
    }

    void Update() {
        CheckAngleAndRotate();
    }

    private void CheckAngleAndRotate() {

        float yAngle = turretBase.transform.eulerAngles.y;
        var angleModifier = (yAngle <= 180) ? yAngle : -(360 - yAngle);

        if (angleModifier > -turretRotationTreshold && angleModifier < turretRotationTreshold) {
            RotateCamera(0f, -1f);
            PanCamera(0f, -1f);
        }

        if (angleModifier > turretRotationTreshold) {
            RotateCamera(cameraRotationAngle, 1f);
            PanCamera(panX, 1f);
        }

        if(angleModifier < -turretRotationTreshold) {
            RotateCamera(cameraRotationAngle, -1f);
            PanCamera(panX, -1f);
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

    private void PanCamera(float xValue, float multiplier) {
        Vector3 pannedCamera = new Vector3(xValue * multiplier, 0, 0);
        var panTime = Time.deltaTime * panSpeed;
        var newCameraPos = Vector3.Lerp(cameraPivot.transform.localPosition, pannedCamera, panTime);

        cameraPivot.transform.localPosition = newCameraPos;
    }

}
