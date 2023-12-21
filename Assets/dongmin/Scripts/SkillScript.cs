using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;



public class SkillScript : MonoBehaviour
{
    [SerializeField]
    private GameObject playerhand;
    [SerializeField]
    private GameObject Lv1_fire;
    [SerializeField]
    private GameObject Lv2_fire;
    [SerializeField]
    private GameObject Lv3_fire;
    [SerializeField]
    private GameObject Lv1_water;
    [SerializeField]
    private GameObject Lv2_water;
    [SerializeField]
    private GameObject Lv3_water;
    [SerializeField]
    private GameObject Lv1_earth;
    [SerializeField]
    private GameObject Lv2_earth;
    [SerializeField]
    private GameObject Lv3_earth;
    [SerializeField]
    private GameObject Lv1_wind;
    [SerializeField]
    private GameObject Lv2_wind;
    [SerializeField]
    private GameObject Lv3_wind;
    [SerializeField]
    private float playerheight = 1.7f;

    private GameObject casting_clone;
    private GameObject flooring_clone;

    bool isfiring = false;
    void getgroundposition(Vector3 groundpos)
    {
        // TO-DO
        // ������ �Ͼ�� ��ų���� ����� �����ϱ� ���ؼ� ��ų�� �۵��� ���� ��ġ�� �����Ѵ�.
        // ���� ������ ����Ʈ�Ǵ� ��ų���� ������ ��ġ���� ������ �ִ�.
    }
    void FireLv1()
    {
        Instantiate(Lv1_fire, playerhand.transform.position, playerhand.transform.rotation);
    }
    void FireLv2()
    {
        casting_clone = Instantiate(Lv2_fire, playerhand.transform.position, playerhand.transform.rotation);
    }
    void FireLv3()
    {
        Vector3 newposition = playerhand.transform.position + playerhand.transform.forward * 5;
        Instantiate(Lv3_fire, new Vector3(newposition.x, newposition.y - playerheight, newposition.z), playerhand.transform.rotation);
    }
    void WaterLv1()
    {
        Instantiate(Lv1_water, playerhand.transform.position, playerhand.transform.rotation);
    }
    void WaterLv2()
    {
        Instantiate(Lv2_water, new Vector3(playerhand.transform.position.x, playerhand.transform.position.y - playerheight, playerhand.transform.position.z), playerhand.transform.rotation);
    }
    void WaterLv3()
    {
        Vector3 newposition = playerhand.transform.position + playerhand.transform.forward * 5;
        Instantiate(Lv3_water, new Vector3(newposition.x, newposition.y - playerheight, newposition.z), Lv3_water.transform.rotation);
    }
    void EarthLv1()
    {
        Instantiate(Lv1_earth, playerhand.transform.position, playerhand.transform.rotation);
    }
    void EarthLv2()
    {
        Vector3 newposition = playerhand.transform.position + playerhand.transform.forward * 5;
        Instantiate(Lv2_earth, new Vector3(newposition.x, newposition.y - playerheight, newposition.z), Lv3_water.transform.rotation);
    }
    void EarthLv3()
    {
        Instantiate(Lv3_earth, new Vector3(playerhand.transform.position.x, playerhand.transform.position.y - playerheight, playerhand.transform.position.z), playerhand.transform.rotation);
    }
    void WindLv1()
    {
        Instantiate(Lv1_wind, playerhand.transform.position, playerhand.transform.rotation);
    }
    void WindLv2()
    {
        Instantiate(Lv2_wind, playerhand.transform.position, playerhand.transform.rotation);
    }
    void WindLv3()
    {
        Vector3 newposition = playerhand.transform.position + playerhand.transform.forward * 5;
        Instantiate(Lv3_wind, new Vector3(newposition.x, newposition.y - playerheight, newposition.z), Lv3_wind.transform.rotation);
    }
    // Start is called before the first frame update
    void Start()
    {

    }
    void Update()
    {
        if (casting_clone)
        {
            casting_clone.transform.position = playerhand.transform.position;
            casting_clone.transform.rotation = playerhand.transform.rotation;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {

            Instantiate(Lv1_fire, playerhand.transform.position, playerhand.transform.rotation);
            //Lv1_fire.transform.position = player.transform.position;
            //Lv1_fire.GetComponent<ParticleSystem>().Play();
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            casting_clone = Instantiate(Lv2_fire, playerhand.transform.position, playerhand.transform.rotation);

        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            Vector3 newposition = playerhand.transform.position + playerhand.transform.forward * 5;
            Instantiate(Lv3_fire, new Vector3(newposition.x, newposition.y - playerheight, newposition.z), playerhand.transform.rotation);
        }
        else if (Input.GetKeyDown(KeyCode.Z))
        {
            Instantiate(Lv1_water, playerhand.transform.position, playerhand.transform.rotation);
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            Instantiate(Lv2_water, new Vector3(playerhand.transform.position.x, playerhand.transform.position.y - playerheight, playerhand.transform.position.z), playerhand.transform.rotation);
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            Vector3 newposition = playerhand.transform.position + playerhand.transform.forward * 5;
            Instantiate(Lv3_water, new Vector3(newposition.x, newposition.y - playerheight, newposition.z), Lv3_water.transform.rotation);
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            Instantiate(Lv1_earth, playerhand.transform.position, playerhand.transform.rotation);
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            Vector3 newposition = playerhand.transform.position + playerhand.transform.forward * 5;
            Instantiate(Lv2_earth, new Vector3(newposition.x, newposition.y - playerheight, newposition.z), Lv3_water.transform.rotation);
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            Instantiate(Lv3_earth, new Vector3(playerhand.transform.position.x, playerhand.transform.position.y - playerheight, playerhand.transform.position.z), playerhand.transform.rotation);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Instantiate(Lv1_wind, playerhand.transform.position, playerhand.transform.rotation);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Instantiate(Lv2_wind, playerhand.transform.position, playerhand.transform.rotation);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Vector3 newposition = playerhand.transform.position + playerhand.transform.forward * 5;
            Instantiate(Lv3_wind, new Vector3(newposition.x, newposition.y - playerheight, newposition.z), Lv3_wind.transform.rotation);
        }
    }
}
