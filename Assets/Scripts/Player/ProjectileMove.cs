using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMove : MonoBehaviour {

    //Cache
    private Rigidbody rb;

    //Parameters
    [SerializeField] float speed = 2f;

    void Start() {
        rb = GetComponent<Rigidbody>();
        ShootProjectile();
    }

    private void ShootProjectile() {
        if (speed != 0) {
            rb.AddForce(transform.forward * speed, ForceMode.Impulse);
        } else {
            Debug.LogWarning("No speed added on projectile");
        }
    }

}
