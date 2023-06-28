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

    void Awake()
    {
        anim = GetComponent<Animator>();
        factoryplayer = GameObject.Find("FactoryPlayer").GetComponent<FactoryPlayer_3>();
        nav = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);
        if (distance <= range)
        {
            transform.LookAt(player);
            if (!isChk)
            {
                ContactUI.SetActive(true);
                isChk = true;
            }
            nav.SetDestination(player.position);
            //transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            anim.SetBool("isWalk", true);
        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Ãæµ¹");

            anim.SetBool("isAttack", true);

            Invoke("DelayDestroy", 1f);
        }
    }

    void DelayDestroy()
    {
        ContactUI.SetActive(false);
        particle.SetActive(true);

        factoryplayer.AttackCnt++;
        factoryplayer.attackParticle.gameObject.SetActive(true);
        factoryplayer.attackParticle.Play();
        Destroy(this.gameObject, .5f);
    }
}
