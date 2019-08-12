using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSpawner : MonoBehaviour {

    //Object References
    [Header("Spawn Points")]
    [SerializeField] GameObject initialSpawn;
    [SerializeField] GameObject[] spawnPoints;

    //Parameters
    [Header("Spawn Parameters")]
    [SerializeField] int initialMaxSpawnLimit = 8;
    [SerializeField] float minSpawnTime = 3f;
    [SerializeField] float maxSpawnTime = 5f;
    [Header("Target Spawn Pool")]
    [SerializeField] GameObject[] initialTargets;

    [System.Serializable]
    private class TargetList {
        public GameObject[] targetFormation;
    }
    [SerializeField] TargetList[] targetList;

    //State
    float spawnTime = 5f;
    float timer = 0;
    int spawnCounter = 0;
    int currentPhase = 0;

    private TargetList GetTargetList(int phase) {
        return targetList[phase];
    }

    void Start() {
        SpawnInitialTargets();
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

    private void SpawnInitialTargets() {
        Transform[] childSpawnPoints = initialSpawn.GetComponentsInChildren<Transform>();

        foreach(Transform spawnPoint in childSpawnPoints) {
            if(spawnPoint != initialSpawn.transform) {
                float randomRoll = Random.Range(1, 100f);
                if (spawnCounter <= initialMaxSpawnLimit && randomRoll >= 50f) {
                    int randomTargetFormation = Random.Range(0, initialTargets.Length);

                    Instantiate(
                        initialTargets[randomTargetFormation],
                        spawnPoint.transform.position,
                        Quaternion.identity,
                        spawnPoint.transform.parent
                    );
                    spawnPoint.gameObject.SetActive(false);
                    spawnCounter++;
                }
            }
        }

        if(spawnCounter < initialMaxSpawnLimit) {
            SpawnInitialTargets();
        }
    }

    private void SpawnTargets() {
        int randomSpawnPoint = Random.Range(0, spawnPoints.Length);
        int randomTargetFormation = Random.Range(0, GetTargetList(currentPhase).targetFormation.Length);
        Debug.Log(targetList.Length);
        Instantiate(
            GetTargetList(currentPhase).targetFormation[randomTargetFormation],
            spawnPoints[randomSpawnPoint].transform.position,
            Quaternion.identity,
            spawnPoints[randomSpawnPoint].gameObject.transform
        );
    }

    private void ResetSpawnTime() {
        timer = 0;
        spawnTime = Random.Range(minSpawnTime, maxSpawnTime);
    }

    //Getters and Setters
    public void ShiftToNextPhase() {
        if(currentPhase < targetList.Length - 1) {
            currentPhase++;
        } else {
            currentPhase = targetList.Length - 1;
        }
    }

}
