using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportManager : MonoBehaviour
{
    public GameObject GameCharacter;
    public GameObject CenterEye;

    private RaycastHit hit;
    private GameObject HitColliderSave;

    private int layerMask;
    // Start is called before the first frame update
    void Start()
    {
        layerMask = 1 << 8;
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(CenterEye.transform.position, CenterEye.transform.forward, out hit, Mathf.Infinity, layerMask))
        {
            HitColliderSave = hit.collider.gameObject;
            Debug.Log("Hit TeleportPoint " + hit.collider.gameObject.name);
            Debug.DrawRay(CenterEye.transform.position, CenterEye.transform.forward * hit.distance, Color.red);

            hit.collider.gameObject.SendMessage("_OnTriggerEnter");

            Debug.Log(hit.collider.gameObject.name);
        }
        else
        {
            if(HitColliderSave != null)
            {
                HitColliderSave.SendMessage("_OnTriggerExit");
            }
            Debug.DrawRay(CenterEye.transform.position, CenterEye.transform.forward * 1000f, Color.red);
        }    
    }

    void Teleport()
    {
        if (Physics.Raycast(CenterEye.transform.position, CenterEye.transform.forward, out hit, Mathf.Infinity, layerMask))
        {
            GameCharacter.transform.position = hit.collider.gameObject.transform.position;
        }
    }
        

}
