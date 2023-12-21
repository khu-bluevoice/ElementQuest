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
        // 땅에서 일어나는 스킬들을 제대로 구현하기 위해선 스킬이 작동될 땅의 위치를 얻어야한다.
        // 현재 땅에서 이펙트되는 스킬들은 임의의 위치값을 가지고 있다.
    }

    public void CastSkill(SpellName spellName)
    {
        switch (spellName) {
            case SpellName.EARTH_LV1:
                EarthLv1();
                break;
            case SpellName.FIRE_LV1:
                FireLv1();
                break;
            case SpellName.WATER_LV1:
                WaterLv1();
                break;
            case SpellName.WIND_LV1:
                WindLv1();
                break;
            case SpellName.FIRE_LV2:
                FireLv2();
                break;
            case SpellName.WIND_LV2:
                WindLv2();
                break;
        }
    }
    void FireLv1()
    {
        //Quaternion currentRotation = playerhand.transform.rotation; 
        //Quaternion rotatedQuaternion = Quaternion.Euler(5, 90, 0) * currentRotation;
        Vector3 forwardmaxpos = Camera.main.transform.forward * 1000f + Camera.main.transform.position;
        Vector3 direction = forwardmaxpos - playerhand.transform.position;
        direction.Normalize();

        //Instantiate(Lv1_fire, playerhand.transform.position, playerhand.transform.rotation);
        Instantiate(Lv1_fire, playerhand.transform.position, Quaternion.LookRotation(direction));
    }
    void FireLv2()
    {
        Vector3 forwardmaxpos = Camera.main.transform.forward * 1000f + Camera.main.transform.position;
        Vector3 direction = forwardmaxpos - playerhand.transform.position;
        direction.Normalize();

        casting_clone = Instantiate(Lv2_fire, playerhand.transform.position, Quaternion.LookRotation(direction));
    }
    void FireLv3()
    {
        Vector3 newposition = playerhand.transform.position + playerhand.transform.forward * 5;
        Instantiate(Lv3_fire, new Vector3(newposition.x, newposition.y - playerheight, newposition.z), playerhand.transform.rotation);
    }
    void WaterLv1()
    {
        Vector3 forwardmaxpos = Camera.main.transform.forward * 1000f + Camera.main.transform.position;
        Vector3 direction = forwardmaxpos - playerhand.transform.position;
        direction.Normalize();

        Instantiate(Lv1_water, playerhand.transform.position, Quaternion.LookRotation(direction));
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
        Vector3 forwardmaxpos = Camera.main.transform.forward * 1000f + Camera.main.transform.position;
        Vector3 direction = forwardmaxpos - playerhand.transform.position;
        direction.Normalize();

        Instantiate(Lv1_earth, playerhand.transform.position, Quaternion.LookRotation(direction));
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
        Vector3 forwardmaxpos = Camera.main.transform.forward * 1000f + Camera.main.transform.position;
        Vector3 direction = forwardmaxpos - playerhand.transform.position;
        direction.Normalize();

        Instantiate(Lv1_wind, playerhand.transform.position, Quaternion.LookRotation(direction));
    }
    void WindLv2()
    {
        Vector3 forwardmaxpos = Camera.main.transform.forward * 1000f + Camera.main.transform.position;
        Vector3 direction = forwardmaxpos - playerhand.transform.position;
        direction.Normalize();
        Instantiate(Lv2_wind, playerhand.transform.position, Quaternion.LookRotation(direction));
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
        // 화염방사기 유지하는 거여서 없애면 안댐!
        if (casting_clone)
        {
            Vector3 forwardmaxpos = Camera.main.transform.forward * 1000f + Camera.main.transform.position;
            Vector3 direction = forwardmaxpos - playerhand.transform.position;
            direction.Normalize();
            casting_clone.transform.position = playerhand.transform.position;
            casting_clone.transform.rotation = Quaternion.LookRotation(direction);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {

            FireLv1();
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
        //else if (Input.GetKeyDown(KeyCode.Alpha1))
        //{
        //    Instantiate(Lv1_wind, playerhand.transform.position, playerhand.transform.rotation);
        //}
        //else if (Input.GetKeyDown(KeyCode.Alpha2))
        //{
        //    Instantiate(Lv2_wind, playerhand.transform.position, playerhand.transform.rotation);
        //}
        //else if (Input.GetKeyDown(KeyCode.Alpha3))
        //{
        //    Vector3 newposition = playerhand.transform.position + playerhand.transform.forward * 5;
        //    Instantiate(Lv3_wind, new Vector3(newposition.x, newposition.y - playerheight, newposition.z), Lv3_wind.transform.rotation);
        //}
    }
}
