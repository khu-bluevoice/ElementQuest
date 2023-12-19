using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour
{
    public enum Type { Short, Long, Boss };

    public Type monsterType;
    public Transform target;
    public bool isChase;
    public bool isDamaged;
    public bool isAttack;
    public BoxCollider meleeAttack;
    public GameObject monsterSkill;

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

        Invoke("ChaseStart", 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
