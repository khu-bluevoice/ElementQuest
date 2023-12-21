using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill2 : MonoBehaviour
{
    protected string element;
    protected float damage;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.SendMessage("Damaged", damage);
        }
        Destroy(gameObject);


        //=======     TEST CODE - 데미지 구현    ========
        //if (collision.gameObject.tag == "Monster")
        //{
        //    monster = collision.gameObject.GetComponent<Monster>();
        //    MonsterElement = monster.GetElement();
        //    //Debug.Log(MonsterElement);

        //    if (MonsterElement - IntElement == 1 || MonsterElement - IntElement == -3)
        //    {
        //        damage *= 2;
        //    }
        //    else if (MonsterElement - IntElement == -1 || MonsterElement - IntElement == 3)
        //    {
        //        damage /= 2;
        //    }

        //    collision.gameObject.SendMessage("Damaged", damage);
        //}
        //Destroy(gameObject);
    }
}
