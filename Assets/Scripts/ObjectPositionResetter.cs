using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPositionResetter : MonoBehaviour {

    //Reference Objects
    Transform spawnPoint;

    void Start() {
        if(spawnPoint == null) {
            spawnPoint = transform.parent.GetComponentInParent<Transform>();
        }
    }


    void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Object Resetter") {
            transform.position = spawnPoint.position;
        }
    }

}
