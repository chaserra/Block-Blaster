using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSLimiter : MonoBehaviour {

    private static FPSLimiter _instance;
    public static FPSLimiter Instance { get { return _instance; } }

    public int targetFrame = 30;

    private void Awake() {
        if(_instance == null) {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    private void Start() {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = targetFrame;
    }

}
