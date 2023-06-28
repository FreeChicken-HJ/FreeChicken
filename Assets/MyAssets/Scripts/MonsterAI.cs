using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterAI : MonoBehaviour
{
    public Transform player;
    public float speed = 5f;
    public float range = 10f;

    Animator anim;

    void Awake()
    {
        anim= GetComponent<Animator>();
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);
        if (distance <= range)
        {
            transform.LookAt(player);

            transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            anim.SetBool("isWalk", true);
        }
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag=="Player")
        {
            anim.SetBool("isAttack", true);
            Invoke("DelayDestroy", 3f);
        }
    }

    void DelayDestroy()
    {
        this.gameObject.SetActive(false);
    }
}
