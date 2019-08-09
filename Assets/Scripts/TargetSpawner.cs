using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSpawner : MonoBehaviour {

    //Object References
    [Header("Spawn Points")]
    [SerializeField] GameObject InitialSpawn;
    [SerializeField] GameObject[] spawnPoints;

    //Parameters
    [Header("Target Spawn Pool")]
    [SerializeField] GameObject[] targets;
    [Header("Spawn Parameters")]
    [SerializeField] float minSpawnTime = 3f;
    [SerializeField] float maxSpawnTime = 5f;

    //State
    [SerializeField] float spawnTime = 5f; //TODO: HIGH -- Serialized for debugging
    [SerializeField] float timer = 0; //TODO: HIGH -- Serialized for debugging

    void Start() {
        ResetSpawnTime();
    }


    void Update() {
        if(timer < spawnTime) {
            timer += Time.deltaTime;
        } else {
            SpawnTargets();
            ResetSpawnTime();
        }
    }

    private void SpawnTargets() {
        int randomSpawnPoint = Random.Range(0, spawnPoints.Length);
        int randomTargetFormation = Random.Range(0, targets.Length);

        Instantiate(
            targets[randomTargetFormation], 
            spawnPoints[randomSpawnPoint].transform.position, 
            Quaternion.identity,
            spawnPoints[randomSpawnPoint].gameObject.transform
        );
    }

    private void ResetSpawnTime() {
        timer = 0;
        spawnTime = Random.Range(minSpawnTime, maxSpawnTime);
    }

}
