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
        //nav.SetDestination(player.position);
        float distance = Vector3.Distance(transform.position, player.position);
        if (distance <= range)
        {
            transform.LookAt(player);
            if (!isChk)
            {
                ContactUI.SetActive(true);
                isChk = true;
            }
            anim.SetBool("isWalk", true);
            nav.SetDestination(player.position);
            //transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag == "Player" && !isAttack)
        {
            Debug.Log("충돌");
           
            anim.SetBool("isAttack", true);
            isAttack = true;
            Invoke("DelayDestroy", 1f);
        }
        if (collision.gameObject.tag == "Props" && isPop)
        {
            Debug.Log("과연?00");
            //isContact = true;
            Instantiate(popParticle,new Vector3(collision.gameObject.transform.position.x,collision.gameObject.transform.position.y +1,collision.gameObject.transform.position.z), collision.gameObject.transform.rotation); // 그자리에 불나오게 설정
            this.gameObject.SetActive(false);
        }
    }
   /* private void OnCollisionEnter(Collider other)
    {
       
    }*/
    void DelayDestroy()
    {
        ContactUI.SetActive(false);
        particle.SetActive(true);
        isPop = true;
        factoryplayer.AttackCnt++;
        factoryplayer.attackParticle.gameObject.SetActive(true);
        factoryplayer.attackParticle.Play();
        //nav.ResetPath();
        Destroy(this.gameObject,2f);
        //nav.isStopped = true;
      

    }
}
