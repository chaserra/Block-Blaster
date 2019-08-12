using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    //State
    private bool isAlive = true;
    
    //Getters and Setters
    public void IsDead() {
        isAlive = false;
    }

    public bool IsAlive() {
        return isAlive;
    }

}
