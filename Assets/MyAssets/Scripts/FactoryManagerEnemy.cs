using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class FactoryManagerEnemy : MonoBehaviour
{
   
    public Transform target;
    public GameObject ContactUI;
    NavMeshAgent agent;
    Animator anim;
   
    public LayerMask Ground, Player;
    public bool isAttacked;
    
    public bool playerInSight, playerInAttack;

    public Vector3 pos;
    bool wayPointSet;
    public float wayPointRange;

    public float timeBetweenAttacks;

    public bool isWalk;
    public bool isRun;
    public bool isAttack;

    public FactoryPlayer_3 player;
    public GameObject handPos;
    public bool isAllStop;
    public Transform trashcan;
    public GameObject runUI;
    public int EButtonClickCnt;
    public GameObject DieCanvas;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        player = GameObject.Find("FactoryPlayer").GetComponent<FactoryPlayer_3>();
    }
   
    void  Update()
    {
        if (!isAllStop)
        {
            agent.isStopped = false;
            playerInSight = Physics.CheckSphere(transform.position, 3.5f, Player);
            playerInAttack = Physics.CheckSphere(transform.position, 2f, Player);
            if (!playerInSight && !playerInAttack) MoveRandom();
            if (playerInSight && !playerInAttack) Targeting();
            if (playerInSight && playerInAttack) Attack();
        }
        

        if(!isWalk && !isRun && isAttack)
        {
            
            anim.SetTrigger("doAttack");
            anim.SetBool("isAttack", true);
            anim.SetBool("Running", false);
        }
        if(!isWalk && isRun && !isAttack)
        {
            anim.SetBool("Walking", false);
            anim.SetBool("isAttack", false);
            anim.SetBool("Running",true);
        }
        if(isWalk && !isRun && !isAttack)
        {
            anim.SetBool("Running", false);
            anim.SetBool("Walking", true);
            anim.SetBool("isAttack", false);

        }
        if(player.AttackCnt >= 3)
        {
            player.isAttack = true;
            isAllStop = true;
            ContactUI.SetActive(false);
            runUI.gameObject.SetActive(true);
            if (isAllStop)
            {
                player.transform.position = handPos.transform.position;
                agent.SetDestination(trashcan.position);
               
            }
            if (Input.GetButtonDown("A") && !player.isDie)
            {
                EButtonClickCnt++;
                Debug.Log(EButtonClickCnt);
                if(EButtonClickCnt >= 10)
                {
                    player.isAttack = false;
                    runUI.gameObject.SetActive(false);
                    player.AttackCnt = 0;
                    EButtonClickCnt = 0;
                    agent.isStopped = true;
                    Invoke("Begin", 4f);
                    
                }
            }
        }
    }
    void Begin()
    {
        isAllStop = false;
    }
    void Targeting() // 캐릭터 발견 & 따라가기 
    {
        
        isAttack = false;
        isRun = true;
        isWalk = false;
        ContactUI.SetActive(true);
        agent.SetDestination(target.position);
        // 심장 두근 소리 추가 6.11
       
    }
    void MoveRandom() //랜덤이동 하다가 
    {
        ContactUI.SetActive(false);
        isAttack = false;
        isRun = false;
        isWalk = true;
        if (!wayPointSet) SearchWalkPoint();

        if (wayPointSet)
        {
         
            agent.SetDestination(pos);
        }

        Vector3 distanceToWalkPoint = transform.position - pos;
        if(distanceToWalkPoint.magnitude < 1f)
        {
            wayPointSet = false;
        }
       
       
    }
 
    void SearchWalkPoint()
    {
        float randomZ = Random.Range(-wayPointRange,wayPointRange);
        float randomX = Random.Range(-wayPointRange,wayPointRange);
        pos = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(pos, -transform.up, 2f, Ground))
        {
            wayPointSet = true;
        }
    }
    void Attack()
    {
        
        isWalk = false;
        isRun = false;
        isAttack = true;
        
        if (!isAttacked)
        {
            
            isAttacked = true;
            Invoke("ResetAttack", timeBetweenAttacks);
        }
    }
    void ResetAttack()
    {
        
        isAttacked=false;
    }

}
