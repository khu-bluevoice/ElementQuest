using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stage
{
    public string stageName;
    public int NumEnemies;
    public GameObject[] typeOfEnemies;
    public float spawnInterval;
}

public class MonsterSpawner : MonoBehaviour
{
    public Stage[] stages;
    public Transform[] spawnPoints;

    private Stage currentStage;
    private int currentStageNumber;

    private bool canSpawn = true;
    private void Update()
    {
        currentStage = stages[currentStageNumber];
        SpawnStage();
        GameObject[] totalEnemies = GameObject.FindGameObjectsWithTag("Enemy");

        if(totalEnemies.Length ==0 &!canSpawn &&currentStageNumber+1 != stages.Length)
        {
            currentStageNumber++;
            canSpawn = true;
        }
    }

    void SpawnStage()
    {
        if (canSpawn)
        {
            GameObject randomEnemy = currentStage.typeOfEnemies[Random.Range(0, currentStage.typeOfEnemies.Length)];
            Transform randomPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            Instantiate(randomEnemy, randomPoint.position, Quaternion.identity);

            currentStage.NumEnemies--;
            if(currentStage.NumEnemies==0)
            {
                canSpawn = false;
            }
        }
    }
}
