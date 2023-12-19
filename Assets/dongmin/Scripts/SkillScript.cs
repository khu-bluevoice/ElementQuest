using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class SkillScript : MonoBehaviour
{
    public GameObject playerhand;
    public GameObject Lv1_fire;
    public GameObject Lv2_fire;
    public GameObject Lv3_fire;
    public GameObject Lv1_earth;
    public GameObject Lv2_earth;
    public GameObject Lv3_earth;
    public GameObject Lv1_water;
    public GameObject Lv2_water;
    public GameObject Lv3_water;
    public GameObject Lv1_wind;
    public GameObject Lv2_wind;
    public GameObject Lv3_wind;
    private Vector3 effectpos = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)){
            
            Instantiate(Lv1_fire, new Vector3(playerhand.transform.position.x, playerhand.transform.position.y, playerhand.transform.position.z), playerhand.transform.rotation);
            Lv1_fire.transform.forward = playerhand.transform.forward;
            //Lv1_fire.transform.position = player.transform.position;
            //Lv1_fire.GetComponent<ParticleSystem>().Play();
        }
    }
}
