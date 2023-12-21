using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public ElementQuestGameManager GameManager;
    public int MonsterCount;
    private bool Cleared;
    public GameObject[] LevelMoveTeleportPos;
    // Start is called before the first frame update

    void Awake()
    {
        GameManager = GameObject.FindFirstObjectByType<ElementQuestGameManager>();
        Debug.Log("1");

        if(!GameManager.ClearMap.ContainsKey(SceneManager.GetActiveScene().name))
        {
            GameManager.ClearMap.Add(SceneManager.GetActiveScene().name, false);
        }

        if(!GameManager.ClearMap[SceneManager.GetActiveScene().name])
        {
            GameObject EnemyObjects = GameObject.FindGameObjectWithTag("Enemy");
            for(int i = 0; i < EnemyObjects.transform.childCount; i++)
            {
                EnemyObjects.transform.GetChild(i).gameObject.SetActive(true);
                MonsterCount++;
            }
        }
    }

    void Start()
    {
        Cleared = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!Cleared)
        {
            if(MonsterCount == 0)
            {
                Cleared = true;
                GameManager.ClearMap[SceneManager.GetActiveScene().name] = true;
                GameObject[] EndObjects = GameObject.FindGameObjectsWithTag("Finish");
                foreach(GameObject EndObject in EndObjects)
                {
                    EndObject.SetActive(false);
                }
                foreach(GameObject LevelMoveTeleportObject in LevelMoveTeleportPos)
                {
                    LevelMoveTeleportObject.SetActive(true);
                }
            }
        }
    }
}
