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

    private LevelManager LevelManager;


    //for text flooting
    public GameObject hudDamageText;
    public Transform hudPos;

    [SerializeField]
    private float hp = 100f;
    [SerializeField]
    private Element monsterElement;
    private float timer = 0.0f;
    [SerializeField]
    private float waitingtime = 0.5f; // 스킬 데미지가 들어가는 간격
    // 불속성 몬스터는 물 스킬에 약하다.
    // 물속성 몬스터는 땅 스킬에 약하다.
    // 땅속성 몬스터는 바람 스킬에 약하다.
    // 바람속성 몬스터는 불 스킬에 약하다.
    private bool damaging = false;
    private bool waitingend = false;
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
        player = GetComponent<GameObject>();

        LevelManager = GameObject.FindFirstObjectByType<LevelManager>();

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

            case Type.Boss:
                targetRadius = 0.7f;
                targetRange = 1f;
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
            case Type.Boss:
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
                rigidSkill.velocity = transform.forward * 20;

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

    public Element GetElement()
    {
        return monsterElement;
    }
    // 遺덉냽??紐ъ뒪?곕뒗 臾??ㅽ궗???쏀븯??
    // 臾쇱냽??紐ъ뒪?곕뒗 ???ㅽ궗???쏀븯??
    // ?낆냽??紐ъ뒪?곕뒗 諛붾엺 ?ㅽ궗???쏀븯??
    // 諛붾엺?띿꽦 紐ъ뒪?곕뒗 遺??ㅽ궗???쏀븯??
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
                //Debug.Log(gameObject.name + " 죽었습니다.!");
                anim.SetBool("attack", false);
                anim.SetBool("die", true);
                Destroy(gameObject, 2);
            }
            else
            {
                StartCoroutine(KnockBack());
                Debug.Log(gameObject.name + "공격받음 : " + damage + "남은체력 : " + hp + "입니다.");
            }
            damaging = true;
        }

    }

    IEnumerator KnockBack()
    {
        //transform.position += player.transform.forward * 1;
        //yield return new WaitForSeconds(0.5f);
        anim.SetBool("damaged", true);
        //nav.speed = 0;
        yield return new WaitForSeconds(1f);
        nav.speed = 3;
        anim.SetBool("damaged", false);
    }

    void OnDestroy()
    {
        LevelManager.MonsterCount--;
    }
}
