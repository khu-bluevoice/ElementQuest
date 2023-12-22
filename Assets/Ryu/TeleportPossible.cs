using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPossible : MonoBehaviour
{
    private GameObject TeleportManager;
    private GameObject WhiteObj;
    private GameObject RedObj;
    // Start is called before the first frame update

    bool IsSetWhite;
    void Start()
    {
        TeleportManager = GameObject.Find("TeleportManager");
        IsSetWhite = true;
        WhiteObj = transform.GetChild(0).gameObject;
        RedObj = transform.GetChild(1).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void _OnTriggerEnter()
    {
        //Debug.Log("haha");
        WhiteObj.SetActive(false);
        RedObj.SetActive(true);
        IsSetWhite = false;
    }

    void _OnTriggerExit()
    {
        if(!IsSetWhite)
        {
            //Debug.Log("dadada");
            RedObj.SetActive(false);
            WhiteObj.SetActive(true);
            IsSetWhite = true;
        }
    }
}
