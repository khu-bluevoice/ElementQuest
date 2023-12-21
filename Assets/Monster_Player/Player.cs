using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject Earth1;
    public float PlayerHP = 100.0f;

    void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            Debug.Log(other.name);
         
            PlayerHP -= 10;
            StartCoroutine(PlayerDamaged());
            Debug.LogWarning(PlayerHP);
            //PlayerDamaged(10);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Instantiate(Earth1, transform.position + transform.forward, transform.rotation);
        }
    }

    IEnumerator PlayerDamaged()
    {
        //yield return new WaitForSeconds(0.2f);
        //anim.SetBool("Damaged", true);     

        yield return new WaitForSeconds(0.2f);
    }
}
