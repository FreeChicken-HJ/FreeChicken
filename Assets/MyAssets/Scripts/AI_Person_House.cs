using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class AI_Person_House : MonoBehaviour
{
    [SerializeField] private string AIName;
    public GameObject AI_Person;

    //private bool isAction;
    private bool isRunning;

    [SerializeField] private Animator anim;
    //[SerializeField] Rigidbody rigid;
    //[SerializeField] private CapsuleCollider capsuleCol;

    protected NavMeshAgent nav;
    private Vector3 destination;

    //---------------------------------------------------

    public Transform target;

    [Header("추격 속도")]
    [SerializeField]
    [Range(1f, 7f)] float moveSpeed = 3f;

    [Header("근접 거리")]
    [SerializeField]
    [Range(0f, 10f)] float contactDistance = 1f;

    HouseScenePlayer player;

    //float dist;

    bool follow;
    bool Dead;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<HouseScenePlayer>();
        //isAction= true;
        //nav = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        //dist = Vector3.Distance(target.transform.position, transform.position);

        //if (dist < 2.5f)
        //{
        //    Debug.Log("타겟이 범위안에 있습니다.");
        //}
        if(!Dead)
        {
            FollowTarget();
        }
    }

    void FollowTarget()
    {
        if (Vector3.Distance(transform.position, target.position) < contactDistance && follow)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
            //nav.SetDestination(transform.position + destination);
            transform.LookAt(target);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            follow = true;
            Debug.Log("들어왔다");
            isRunning = true;
            anim.SetBool("Running", isRunning);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            follow = true;
            Debug.Log("들어와있다");
            isRunning = true;
            anim.SetBool("Running", isRunning);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            follow = false;
            Debug.Log("빠져나왔다");
            isRunning = false;
            anim.SetBool("Running", isRunning);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Obstacle")
        {
            DieMotion();
        }
    }

    void OnParticleCollision(GameObject other)
    {
        if (other.tag == "Fire")
        {
            DieMotion();
            //Instantiate(chicken);
        }
    }

    void DieMotion()
    {
        Debug.Log("적 사망");
        Dead = true;
        anim.SetTrigger("isDead");
        Invoke("DestroyAI_Person", 2f);
    }

    void DestroyAI_Person()
    {
        AI_Person.SetActive(false);
    }
}
