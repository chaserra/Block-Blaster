using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankBulletProjectile : MonoBehaviour {

    //Parameters
    [SerializeField] GameObject hitVFX;
    [SerializeField] int maxNumHit = 2;

    //State
    private int hitCounter = 0;

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Target") {
            hitCounter++;
            if (hitCounter >= maxNumHit) {
                Destroy(gameObject);
            }
        }
    }

}
