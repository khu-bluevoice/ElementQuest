using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject Earth1;

    void Start()
    {
        
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Instantiate(Earth1, transform.position + transform.forward, transform.rotation);
        }
    }
}
