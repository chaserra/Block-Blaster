using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMover : MonoBehaviour {

    //Cache
    Rigidbody rb;

    //Parameters
    [SerializeField] float moveSpeed = 5f;

    void Start() {
        rb = GetComponent<Rigidbody>();
        MoveObject();
    }

    void Update() {

    }

    private void MoveObject() {
        Vector3 moveObject = new Vector3(0, 0, -moveSpeed);
        rb.AddForce(moveObject, ForceMode.Impulse);
    }

    public void SetTargetMovementSpeed(float speedToAdd) {
        moveSpeed += speedToAdd;
    }

}
