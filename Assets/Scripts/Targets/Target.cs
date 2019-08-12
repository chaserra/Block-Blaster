using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour {

    //Cache
    GameController gameController;

    //References
    [SerializeField] TargetType targetType;
    [SerializeField] GameObject explosionVFX;

    //Parameters
    [SerializeField] int score = 20;
    [SerializeField] float blastRadius = 0f;
    [SerializeField] int scoreMultiplier = 1;

    //State
    private bool isHit = false;

    void OnDrawGizmosSelected() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, blastRadius);
    }

    void Start() {
        gameController = FindObjectOfType<GameController>();
    }

    //Direct Hit
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Player Bullet") {
            ProcessHit(scoreMultiplier);
        } else if(other.gameObject.tag == "Object Resetter") {
            Destroy(gameObject);
        }
    }

    private void ProcessHit(int scoreMultiplier) {
        if(!isHit) {
            isHit = true;
            if (targetType == TargetType.Exploder) {
                Explode(scoreMultiplier);
            }
            gameController.AddScore(score * scoreMultiplier);
            GameObject explosion = Instantiate(
                explosionVFX,
                transform.position,
                Quaternion.identity
            );
            Destroy(explosion, 2f);
            Destroy(gameObject);
        }
    }

    //Destroy objects around this target
    private void Explode(int scoreMultiplier) {
        Collider[] colliders = Physics.OverlapSphere(transform.position, blastRadius);
        foreach (Collider collider in colliders) {
            if (collider.gameObject.tag == "Target" && collider.gameObject != gameObject) {
                Target target = collider.gameObject.GetComponent<Target>();
                target.TriggerDestroyCoroutine(scoreMultiplier);
            }
        }
    }

    //Chain Reactions
    public void TriggerDestroyCoroutine(int scoreMultiplier) {
        StartCoroutine(TriggeredByOtherTarget(scoreMultiplier));
    }

    IEnumerator TriggeredByOtherTarget(int scoreMultiplier) {
        float randomDelay = UnityEngine.Random.Range(.2f, .45f);
        if (targetType == TargetType.Chainer) {
            Explode(scoreMultiplier);
        }
        yield return new WaitForSeconds(randomDelay);
        ProcessHit(scoreMultiplier);
    }
}
