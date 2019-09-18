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
    [SerializeField] float minExplosionDelay = .2f;
    [SerializeField] float maxExplosionDelay = .45f;
    float randomDelay;

    //State
    private WaitForSeconds cachedDelay;
    private bool isHit = false;
    private bool triggeredByExplosion = false;

    void OnDrawGizmosSelected() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, blastRadius);
    }

    void Start() {
        gameController = FindObjectOfType<GameController>();
        randomDelay = UnityEngine.Random.Range(minExplosionDelay, maxExplosionDelay);
        cachedDelay = new WaitForSeconds(randomDelay);
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
                Quaternion.identity,
                GameObject.FindGameObjectWithTag("VFX").transform
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
                if(!target.GetTriggeredByExplosionState()) {
                    target.SetTriggeredByExplosion();
                    target.TriggerDestroyCoroutine(scoreMultiplier);
                }
            }
        }
    }

    //Chain Reactions
    public void TriggerDestroyCoroutine(int scoreMultiplier) {
        StartCoroutine(TriggeredByOtherTarget(scoreMultiplier));
    }

    IEnumerator TriggeredByOtherTarget(int scoreMultiplier) {
        if (targetType == TargetType.Chainer) {
            Explode(scoreMultiplier);
        }
        yield return cachedDelay;
        ProcessHit(scoreMultiplier);
    }

    //Getters and Setters
    public bool GetTriggeredByExplosionState() {
        return triggeredByExplosion;
    }

    public void SetTriggeredByExplosion() {
        triggeredByExplosion = true;
    }

}
