using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public int MonsterCount;
    private bool Cleared;
    public GameObject[] LevelMoveTeleportPos;
    private UIManager UIManager;
    // Start is called before the first frame update

    void Start()
    {
        UIManager = GameObject.FindFirstObjectByType<UIManager>();

        if (!ElementQuestGameManager.instance.tutorial)
        {
            UIManager.ShowMessage("마법을 시전해 몬스터를 처치하세요!");
            UIManager.ShowMessage("왼쪽 주먹은 이동 및 상자 열기입니다.");
            ElementQuestGameManager.instance.tutorial = true;
        }
        if (!ElementQuestGameManager.instance.ClearMap.ContainsKey(SceneManager.GetActiveScene().name))
        {
            ElementQuestGameManager.instance.ClearMap.Add(SceneManager.GetActiveScene().name, false);
        }

        if (!ElementQuestGameManager.instance.ClearMap[SceneManager.GetActiveScene().name])
        {
            Cleared = false;
            GameObject EnemyObjects = GameObject.FindGameObjectWithTag("Enemy");
            for (int i = 0; i < EnemyObjects.transform.childCount; i++)
            {
                EnemyObjects.transform.GetChild(i).gameObject.SetActive(true);
                MonsterCount++;
            }
        }
        else
        {
            Cleared = true;
        }
        //UIManager.ShowMessage(SceneManager.GetActiveScene().name);
    }

    // Update is called once per frame
    void Update()
    {
        if (Cleared)
        {
            GameObject[] EndObjects = GameObject.FindGameObjectsWithTag("Finish");
            foreach (GameObject EndObject in EndObjects)
            {
                EndObject.SetActive(false);
            }
            foreach (GameObject LevelMoveTeleportObject in LevelMoveTeleportPos)
            {
                LevelMoveTeleportObject.SetActive(true);
            }
            UIManager.ShowMessage("이미 클리어 한 방입니다.");
            enabled = false;
        }
        else
        {
            if(MonsterCount == 0)
            {
                UIManager.ShowMessage("클리어! 다른 방으로 가는 문이 열립니다.");
                ElementQuestGameManager.instance.ClearMap[SceneManager.GetActiveScene().name] = true;
                GameObject[] EndObjects = GameObject.FindGameObjectsWithTag("Finish");
                foreach(GameObject EndObject in EndObjects)
                {
                    EndObject.SetActive(false);
                }
                foreach(GameObject LevelMoveTeleportObject in LevelMoveTeleportPos)
                {
                    LevelMoveTeleportObject.SetActive(true);
                }
                enabled = false;
            }
        }
    }
}
