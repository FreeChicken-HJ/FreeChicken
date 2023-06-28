using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
public class Boss : MonoBehaviour
{
    Rigidbody rb;
    PlayerController target;
    Animator bossAnim;
    CameraShake cameraShake;
  
    public float moveSpeed;
    public NavMeshAgent nav;
    public bool isChase;
    public bool isDie;
    public bool isBuff;
    public bool isAttack;
    public GameObject fireAttack;
    //public GameObject ShowRoute;
    public bool isRockFalling;
    public GameObject FireParticles;


    public bool isRandomSpace;
    public Slider bossHealthbar;

    // Start is called before the first frame update
    void Awake()
    {
        target = GameObject.Find("Character").GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody>();
        bossAnim = GetComponent<Animator>();
        nav = GetComponent<NavMeshAgent>();
        cameraShake = GameObject.Find("Main Camera").GetComponent<CameraShake>();

        Invoke("ChaseStart", 2);
        
    }
    void Start()
    {
        moveSpeed = 3f;
    }
    void ChaseStart()
    {
       
        isChase = true;
        
        bossAnim.SetBool("isWalk", true);
    }
    // Update is called once per frame
    void Update()
    {
        if (nav.enabled && !isDie)
        {
           
            nav.isStopped = !isChase;
            nav.SetDestination(target.transform.position);
            nav.speed = moveSpeed;
        }
       
    }
    IEnumerator RandomSpace()
    {
        yield return new WaitForSeconds(5f);
        isRandomSpace = true;
    }
    IEnumerator Attack()
    {
        isChase = false;
        isAttack = true;
        isBuff = false;
        bossAnim.SetBool("isAttack", true);
        int randomRange = Random.Range(0, 5);
        switch (randomRange)
        {
            case 0:
                int randomIndex = Random.Range(0,2);
                switch (randomIndex)
                {
                    case 0:
                        bossAnim.SetTrigger("doAttack1");
                        Debug.Log("플레이어 공격! 주먹발사 모션 발싸");
                       
                        yield return new WaitForSeconds(1.5f);
                        break;
                    case 1:
                        bossAnim.SetTrigger("doAttack2");
                        yield return new WaitForSeconds(1.5f);
                        break;

                }

                yield return new WaitForSeconds(1.5f);
                break;
            
            case 1:
                bossAnim.SetTrigger("doFireAttack");
                Debug.Log("플레이어 공격! doFireAttack 발싸");
                fireAttack.SetActive(true); 
                yield return new WaitForSeconds(1.5f);
                break;
            
            case 2:
                bossAnim.SetTrigger("doJump");
                
                Vector3 firePos = transform.position + bossAnim.transform.forward + new Vector3(0f, 0.8f, 0f);
                Vector3 fireDirection = transform.localEulerAngles + new Vector3(0f, -150f, 0f);
                Quaternion rotation = Quaternion.Euler(fireDirection);
                Vector3 firePos1 = transform.position + bossAnim.transform.forward + new Vector3(1f, 0.8f, -1f);
                Vector3 fireDirection1 = transform.localEulerAngles + new Vector3(0f, -175f, 0f);
                Quaternion rotation1 = Quaternion.Euler(fireDirection1);
                Vector3 firePos2 = transform.position + bossAnim.transform.forward + new Vector3(-1f, 0.8f, 1f);
                Vector3 fireDirection2 = transform.localEulerAngles + new Vector3(0f, -200f, 0f);
                Quaternion rotation2 = Quaternion.Euler(fireDirection2);
                Instantiate(FireParticles, firePos, rotation);
                Instantiate(FireParticles, firePos1, rotation1);
                Instantiate(FireParticles, firePos2, rotation2);
                yield return new WaitForSeconds(1.5f);
                break;
            
            case 3:
                bossAnim.SetTrigger("doFireShoot");
                Debug.Log("바닥 내려치기");
                isRockFalling = true;
                cameraShake.StartCoroutine(cameraShake.Shake(.5f, .3f));
                Time.timeScale = 1.5f;
                yield return new WaitForSeconds(1.5f);
                break;
         /*   case 4: // route 보여주고 그 길로 달려가는 공격 -> 플레이어에 닿으면 -5hp 감소
                //bossAnim.SetBool("isWalk",false);
               
                //ShowRoute.SetActive(true);
                Debug.Log("길 보여줘");
                moveSpeed = 7f;

               
                yield return new WaitForSeconds(3f);                
                break;*/
           

        }
        Time.timeScale = 1f;
        fireAttack.SetActive(false);
        bossAnim.SetBool("isAttack", false);
        moveSpeed = 3f;
        isAttack = false;
        isChase = true;
        
    }
  
    void FixedUpdate()
    {
        
        FreezeVelocity();
        Targeting();

    }

    void FreezeVelocity()
    {
        if (isChase)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }
    void Targeting()
    {
       
        float targetRadius = 1.5f;
        float targetRange = 3f;
        RaycastHit[] rayHits = Physics.SphereCastAll(transform.position, targetRadius, transform.forward, targetRange, LayerMask.GetMask("Player"));
        if (rayHits.Length > 0 && !isAttack && !isBuff &&!isDie)
        {
            StartCoroutine(Attack());
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "EggShoot" && !isBuff)
        {
            bossHealthbar.value -= 5f;
            StartCoroutine(Buff());
            if (bossHealthbar.value <= 0)
            {
                StartCoroutine(Die());
            }
            
        }
    }
    IEnumerator Buff()
    {
        isChase = false;
        isAttack = false;
        isBuff = true;
        bossAnim.SetBool("isHit", true);
        bossAnim.SetTrigger("doBuff");
        Time.timeScale = 0.75f;
        yield return new WaitForSeconds(1f);
        isBuff = false;
        bossAnim.SetBool("isHit", false);
        Time.timeScale = 1f;
        isChase = true;
        moveSpeed = 5f;
        
        
    }
    IEnumerator Die()
    {
        isChase = false;
        isAttack = false;
        isDie = true;
        bossAnim.SetTrigger("doDie");
        Time.timeScale = 0.5f;
        yield return new WaitForSeconds(3f);
        this.gameObject.SetActive(false);
        Time.timeScale = 1f;
    }
}                               
  