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
            GameObject hitEffect = Instantiate(
                hitVFX, 
                transform.position, 
                Quaternion.LookRotation(transform.position - other.transform.position),
                GameObject.FindGameObjectWithTag("VFX").transform
            );
            Destroy(hitEffect, 4f);
            if (hitCounter >= maxNumHit) {
                Destroy(gameObject);
            }
        }
    }

}
