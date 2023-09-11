using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class FactorySceneBomb : MonoBehaviour
{
    public Transform player;
    public float speed = 5f;
    public float range = 10f;

    Animator anim;
    public GameObject particle;
    public GameObject ContactUI;
    public bool isChk;
    public bool isAttack;
    public FactoryPlayer_3 factoryplayer;
    public NavMeshAgent nav;
    public GameObject popParticle;

    public bool isPop;
    public bool isContact;
    void Awake()
    {
        anim = GetComponent<Animator>();
        factoryplayer = GameObject.Find("FactoryPlayer").GetComponent<FactoryPlayer_3>();
        nav = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
      
        if (isAttack)
        {
            nav.SetDestination(player.position);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
       
        if (collision.gameObject.tag == "Player" && !isAttack)
        {
           
            anim.SetBool("isAttack", true);
            isAttack = true;
            Invoke("DelayDestroy", 1f);
        }
        if (collision.gameObject.tag == "Props" && isPop)
        {
           
            Instantiate(popParticle,new Vector3(collision.gameObject.transform.position.x,collision.gameObject.transform.position.y +1,collision.gameObject.transform.position.z), collision.gameObject.transform.rotation); // 그자리에 불나오게 설정
            this.gameObject.SetActive(false);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            ContactUI.SetActive(true);
            transform.LookAt(player);

            anim.SetBool("isWalk", true);
           
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            ContactUI.SetActive(false);
            anim.SetBool("isWalk",false);
            nav.isStopped = true;   
        }
    }
    void DelayDestroy()
    {
        ContactUI.SetActive(false);
        particle.SetActive(true);
        isPop = true;
        factoryplayer.AttackCnt++;
        factoryplayer.attackParticle.gameObject.SetActive(true);
        factoryplayer.attackParticle.Play();
        
        Destroy(this.gameObject,2f);
        

    }
}
