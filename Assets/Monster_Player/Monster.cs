using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour
{
    public enum Type { Short, Long, Boss };

    public Type monsterType;

    public bool isChase;
    public bool isDamaged;
    public bool isAttack;
    public BoxCollider meleeAttack;
    public GameObject monsterSkillPos;

    public GameObject monsterSkill;

    protected GameObject player;

    public Transform target;
    protected NavMeshAgent nav;
    protected Animator anim;
    protected Rigidbody rigid;

    public float hp = 100f;
    public int MonsterElement;

    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
        player = GetComponent<GameObject>();

        Invoke("ChaseStart", 1);
    }

    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        target = player.transform;
        //Debug.Log(target.position);

        if (nav.enabled)
        {
            //nav.SetDestination(target.position);
            nav.destination = target.position;
            nav.isStopped = !isChase;
        }
    }

    private void FixedUpdate()  
    {
        Targeting();
        FreezeVelocity();
    }

    void FreezeVelocity()
    {
        if (isChase)
        {
            rigid.velocity = Vector3.zero;
            rigid.angularVelocity = Vector3.zero;
        }
    }
    void Targeting()
    {
        float targetRadius = 0;
        float targetRange = 0;

        switch (monsterType)
        {
            case Type.Short:
                targetRadius = 0.7f;
                targetRange = 1.2f;
                break;

            case Type.Long:
                targetRadius = 3f;
                targetRange = 5f;
                break;
        }

        RaycastHit[] rayHits = Physics.SphereCastAll(transform.position, targetRadius, transform.forward, targetRange, LayerMask.GetMask("Player"));

        if (rayHits.Length > 0 && !isAttack)
        {           
            StartCoroutine(Attack());
        }
    }

    IEnumerator Attack()
    {
        isChase = false;
        isAttack = true;
        anim.SetBool("attack", true);

        switch (monsterType)
        {
            case Type.Short:
                yield return new WaitForSeconds(0.2f);
                meleeAttack.enabled = true;

                yield return new WaitForSeconds(1f);
                meleeAttack.enabled = false;

                yield return new WaitForSeconds(1f);
                break;
            case Type.Long:
                yield return new WaitForSeconds(1f);
                GameObject instantBullet = Instantiate(monsterSkill, monsterSkillPos.transform.position, monsterSkillPos.transform.rotation);
                Rigidbody rigidSkill = instantBullet.GetComponent<Rigidbody>();                  
                rigidSkill.velocity = transform.forward * 50;

                yield return new WaitForSeconds(2f);
                break;
        }

        isChase = true;
        isAttack = false;
        anim.SetBool("attack", false);
    }

    
    void ChaseStart()
    {
        isChase = true;
        anim.SetBool("move", true);
    }

    public int GetElement()
    {
        return MonsterElement;
    }

    public virtual void Damaged(float damage)
    {
        hp -= damage;

        if (hp <= 0)
        {
            Debug.Log(gameObject.name + " 죽었습니다.!");
            anim.SetBool("die", true);
            Destroy(gameObject, 1.5f);
        }
        else
        {
            StartCoroutine(KnockBack());
            Debug.Log(gameObject.name + "공격받음 : " + damage + "남은체력 : " + hp + "입니다.");
        }
    }

    IEnumerator KnockBack()
    {
        transform.position += player.transform.forward * 2;
        anim.SetBool("damaged", true);
        nav.speed = 0;
        yield return new WaitForSeconds(2f);
        nav.speed = 3;
        anim.SetBool("damaged", false);
    }
}
