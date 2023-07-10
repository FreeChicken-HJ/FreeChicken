using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using static UnityEditor.PlayerSettings;

public class AI_Cave : MonoBehaviour
{
    [SerializeField] private string AIName;
    [SerializeField] private float walkSpeed;

    public LayerMask MoveGround, Player;
    public bool isAttacked;

    public Transform target;
    //private Vector3 destination;

    public Vector3 pos;
    bool wayPointSet;
    public float wayPointRange;

    //private bool isAction;
    private bool isWalking;
    private bool isRun;
    private bool isAttack;

    bool isDie;

    public bool playerInSight, playerInAttack;

    public float timeBetweenAttacks;

    public CaveScenePlayer player;

    [SerializeField] private float walkTime;
    [SerializeField] private float waitTime;
    private float currentTime;

    public GameObject small_potion;

    [SerializeField] private Animator anim;
    [SerializeField] Rigidbody rigid;
    [SerializeField] private CapsuleCollider capsuleCol;

    NavMeshAgent nav;
    //public bool isAllStop;

    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        currentTime = waitTime;
        //isAction = true;
        player = GameObject.Find("CaveCharacter").GetComponent<CaveScenePlayer>();
    }

    void Update()
    {
        if (/*!isAllStop &&*/ !isDie)
        {
            nav.isStopped = false;
            playerInSight = Physics.CheckSphere(transform.position, 4f, Player);
            playerInAttack = Physics.CheckSphere(transform.position, 1f, Player);
            if (!playerInSight && !playerInAttack) MoveRandom();
            if (playerInSight && !playerInAttack) Targeting();
            if (playerInSight && playerInAttack) Attack();
        }


        if (!isWalking && !isRun && isAttack)
        {

            anim.SetTrigger("doAttack");
            anim.SetBool("isAttack", true);
            anim.SetBool("Running", false);
        }
        if (!isWalking && isRun && !isAttack)
        {
            anim.SetBool("Walking", false);
            anim.SetBool("isAttack", false);
            anim.SetBool("Running", true);
        }
        if (isWalking && !isRun && !isAttack)
        {
            anim.SetBool("Running", false);
            anim.SetBool("Walking", true);
            anim.SetBool("isAttack", false);

        }
    }

    void Targeting() // 캐릭터 발견 & 따라가기 
    {

        isAttack = false;
        isRun = true;
        isWalking = false;
        nav.SetDestination(target.position);
    }
    void MoveRandom() //랜덤이동 하다가 
    {
        isAttack = false;
        isRun = false;
        isWalking = true;
        if (!wayPointSet) SearchWalkPoint();
        if (wayPointSet)
        {

            nav.SetDestination(pos);
        }

        Vector3 distanceToWalkPoint = transform.position - pos;
        if (distanceToWalkPoint.magnitude < 1f)
        {
            wayPointSet = false;
        }
    }

    void SearchWalkPoint()
    {
        float randomZ = Random.Range(-wayPointRange, wayPointRange);
        float randomX = Random.Range(-wayPointRange, wayPointRange);
        pos = new Vector3(transform.position.x * walkSpeed*Time.deltaTime + randomX, transform.position.y, transform.position.z * walkSpeed * Time.deltaTime + randomZ);

        if (Physics.Raycast(pos, -transform.up, 1f, MoveGround))
        {
            wayPointSet = true;
        }
    }

    void Attack()
    {

        isWalking = false;
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

        isAttacked = false;
    }

    //private void Move()
    //{
    //    if (isWalking)
    //        //rigid.MovePosition(transform.position + transform.forward * walkSpeed * Time.deltaTime);
    //        nav.SetDestination(transform.position + destination);
    //}

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Obstacle" &&!isDie)
        {
            isDie = true;
           
            anim.SetTrigger("isDead");
            Invoke("DestroyAI_Cave", 2f);
        }

        if (collision.gameObject.tag == "Small_Potion")
        {
            Debug.Log("작아지는 물약충돌!");
            small_potion.SetActive(false);
            this.gameObject.transform.localScale = Vector3.one;
        }
    }

    void DestroyAI_Cave()
    {
        this.gameObject.SetActive(false);
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "FootPos" && !isDie)
        {
            isDie = true;
            
            Debug.Log("죽어락");
            anim.SetTrigger("isDead");
            Invoke("DestroyAI_Cave", 2f);
        }

        //if (other.gameObject.tag=="Small_Potion")
        //{
        //    Debug.Log("작아지는 물약충돌!");
        //    small_potion.SetActive(false);
        //    this.gameObject.transform.localScale = Vector3.one;
        //}
    }

    //private void ElapseTime()
    //{
    //    if (isAction)
    //    {
    //        currentTime -= Time.deltaTime;
    //        if (currentTime <= 0)  // 랜덤하게 다음 행동을 개시
    //            ReSet();
    //    }
    //}

    //private void ReSet()  // 다음 행동 준비
    //{
    //    isWalking = false;
    //    isAction = true;
    //    nav.ResetPath();
    //    anim.SetBool("Walking", isWalking);

    //    destination.Set(Random.Range(-0.2f,0.2f),0f,Random.Range(0.5f,1f));

    //    RandomAction();
    //}

    //private void RandomAction()
    //{
    //    int _random = Random.Range(0, 2); // 대기, 걷기

    //    if (_random == 0)
    //        Wait();
    //    else if (_random == 1)
    //        TryWalk();
    //}

    //private void Wait()  // 대기
    //{
    //    currentTime = waitTime;
    //    Debug.Log("대기");
    //}

    //private void TryWalk()  // 걷기
    //{
    //    currentTime = walkTime;
    //    isWalking = true;
    //    anim.SetBool("Walking", isWalking);
    //    Debug.Log("걷기");
    //}
}
