
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;
public class FactorySceneManager : MonoBehaviour
{
    public Transform target;
    NavMeshAgent agent;
    Animator anim;

   
    public FactoryPlayer_3 player;
    public GameObject handPos;
    public bool isAllStop;
    public Transform trashcan;
    public GameObject runUI;
    public int EButtonClickCnt;
    public GameObject DieCanvas;


    public bool isCatch;
    public TextMeshProUGUI Atxt;
    public TextMeshProUGUI Dtxt;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        player = GameObject.Find("FactoryPlayer").GetComponent<FactoryPlayer_3>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("A"))
        {
            Atxt.color = Color.red;
        }
        else if (Input.GetButtonDown("D"))
        {
            Dtxt.color = Color.red;
        }
        if (Input.GetButtonUp("A"))
        {
            Atxt.color = Color.white;
        }
        else if (Input.GetButtonUp("D"))
        {
            Dtxt.color = Color.white;
        }
        if (player.AttackCnt >= 3) 
        {
            //agent.isStopped = false;
            anim.SetBool("Running", true);
            agent.SetDestination(target.position); // player 향해서 달려가기 
            if (isCatch)
            {
                //agent.ResetPath();

                player.isAttack = true;
                player.transform.position = handPos.transform.position; // 플레이어의 위치는 손으로
                anim.SetBool("Walking", true);
                agent.SetDestination(trashcan.position);
                runUI.gameObject.SetActive(true);
                if (Input.GetButtonDown("A") && !player.isDie)
                {
                    
                    EButtonClickCnt++;
                    Debug.Log(EButtonClickCnt);
                    if (EButtonClickCnt >= 5)
                    {
                        isCatch = false;
                        player.isAttack = false;
                        runUI.gameObject.SetActive(false);
                        anim.SetBool("Walking", false);
                        anim.SetBool("Running", false);

                        //agent.isStopped = true;
                       
                        player.AttackCnt = 0;
                        EButtonClickCnt = 0;

                        agent.ResetPath();



                    }
                }
               
            }

        }
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player") // 플레이어랑 충돌하면
        {
            isCatch = true;
        }
    }
}
