using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class Skill : MonoBehaviour
{
    public GameObject player;
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
            Instantiate(Lv1_fire, new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z + 5), player.transform.rotation);
            Debug.Log(player.transform.forward);
            //Lv1_fire.transform.position = player.transform.position;
            //Lv1_fire.GetComponent<ParticleSystem>().Play();
        }
    }
}
