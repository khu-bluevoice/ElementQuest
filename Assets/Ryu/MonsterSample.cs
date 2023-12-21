using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSample : MonoBehaviour
{
    private LevelManager LevelManager;
    // Start is called before the first frame update
    void Start()
    {
        LevelManager = GameObject.FindFirstObjectByType<LevelManager>();
        Destroy(gameObject, 10f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDestroy()
    {
        LevelManager.MonsterCount--;
    }
}
