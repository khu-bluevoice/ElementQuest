using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MapManager : MonoBehaviour
{
    public GameObject player;
    public int mapindex = 0;
    public GameObject FirstStageEnemy;
    public GameObject SecondStageEnemy;
    public GameObject ThirdStageEnemy;
    public GameObject FourthStageEnemy;
    public GameObject FifthStageEnemy;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        switch(mapindex)
        {
            case 0:
                // UI관련?
                break;

            case 1:
                player.transform.position = new Vector3(-54.601f, -4.52f, 42.176f); // 나중에 y축과 rotation 바꿔주기
                // 몹 활성화
                FirstStageEnemy.SetActive(true);
                break;
            case 2:
                player.transform.position = new Vector3(2.19f, -4.17f, 29.46f);
                SecondStageEnemy.SetActive(true);
                break;
            case 3:
                player.transform.position = new Vector3(2.58f, 0.82f, 12.32f);
                ThirdStageEnemy.SetActive(true);
                break;
            case 4:
                player.transform.position = new Vector3(0.23f, 5.6f, -31.65f);
                FourthStageEnemy.SetActive(true);
                break;
            case 5:
                player.transform.position = new Vector3(0.23f, 5.6f, -31.65f);
                FifthStageEnemy.SetActive(true);
                break;
        }
    }
}
