using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour {

    [SerializeField] TargetType targetType;
    [SerializeField] GameObject explosionVFX;

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Player Bullet") {
            ProcessHit();
        }
    }

    private void ProcessHit() {
        //Get type
        //Do type special abilities
        //TODO: High-prio: Add score
        GameObject explosion = Instantiate(
            explosionVFX,
            transform.position,
            Quaternion.identity
        );
        Destroy(explosion, 7f);
        Destroy(gameObject);
    }
}
