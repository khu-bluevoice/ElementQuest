using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class testmonster : MonoBehaviour
{
    [SerializeField]
    private float hp = 100f;
    [SerializeField]
    private Element monsterElement;
    private float timer = 0.0f;
    [SerializeField]
    private float waitingtime = 0.5f; // ��ų �������� ���� ����
    // �ҼӼ� ���ʹ� �� ��ų�� ���ϴ�.
    // ���Ӽ� ���ʹ� �� ��ų�� ���ϴ�.
    // ���Ӽ� ���ʹ� �ٶ� ��ų�� ���ϴ�.
    // �ٶ��Ӽ� ���ʹ� �� ��ų�� ���ϴ�.
    private bool damaging = false;
    private bool waitingend = false;
    private void Update()
    {
        if (damaging)
        {
            timer += Time.deltaTime;
            if (timer > waitingtime)
            {
                waitingend = true;
                damaging = false;
            }
        }
        if (waitingend)
        {
            timer = 0.0f;
            waitingend = false;
        }
        
    }
    public virtual void Damaged(object[] skillprops)
    {
        Element skillElement = (Element)skillprops[0];
        float damage = (float)skillprops[1];
        if (!damaging)
        {
            if (monsterElement == Element.Fire)
            {
                if (skillElement == Element.Water)
                {
                    hp -= damage;
                }
                else
                {
                    damage *= 0.5f;
                    hp -= damage;
                }
            }
            else if (monsterElement == Element.Water)
            {
                if (skillElement == Element.Earth)
                {
                    hp -= damage;
                }
                else
                {
                    damage *= 0.5f;
                    hp -= damage;
                }
            }
            else if (monsterElement == Element.Earth)
            {
                if (skillElement == Element.Wind)
                {
                    hp -= damage;
                }
                else
                {
                    damage *= 0.5f;
                    hp -= damage;
                }
            }
            else //if (monsterElement == Element.Wind)
            {
                if (skillElement == Element.Fire)
                {
                    hp -= damage;
                }
                else
                {
                    damage *= 0.5f;
                    hp -= damage;
                }
            }

            if (hp <= 0)
            {
                Debug.Log(gameObject.name + " �׾����ϴ�.!");
                Destroy(gameObject, 4);
            }
            else
            {
                Debug.Log(gameObject.name + "���ݹ��� : " + damage + "����ü�� : " + hp + "�Դϴ�.");
            }
            damaging = true;
        }
       
    }
}
