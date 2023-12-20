using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill2 : MonoBehaviour
{
    protected string element;
    protected float damage;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Monster")
        {
            collision.gameObject.SendMessage("Damaged", damage);
        }
        Destroy(gameObject);
    }
}
