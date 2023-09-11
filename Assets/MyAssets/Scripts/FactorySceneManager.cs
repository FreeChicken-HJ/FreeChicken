using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;
using Cinemachine;
using UnityEngine.SceneManagement;
public class FactorySceneManager : MonoBehaviour
{
    
    public Transform target;
    NavMeshAgent agent;
    Animator anim;

   
    public FactoryPlayer_3 player;
    public GameObject handPos;
    public bool isAllStop;
    
    public GameObject runUI;
    public int EButtonClickCnt;
    public GameObject DieCanvas;


    public bool isCatch;
    public bool isChk;
    public CinemachineVirtualCamera mainCam;
    public CinemachineVirtualCamera catchCam;
  
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        player = GameObject.Find("FactoryPlayer").GetComponent<FactoryPlayer_3>();

    }

    void Update()
    {

        if (player.AttackCnt >= 3)
        {
            isChk = true;
            anim.SetBool("Walking", true);
            agent.SetDestination(target.position); 
            

        }
        if (isCatch)
        {
            anim.SetBool("Walking", false);

            player.isAttack = true;
            player.transform.position = handPos.transform.position; 
            agent.ResetPath();
            mainCam.Priority = -5;
            catchCam.Priority = 3;

            Invoke("GameEnd", 5f);
        }
    }
    void GameEnd()
    {
        DieCanvas.SetActive(true);
        Invoke("ReStart", 2);
    }
    void ReStart()
    {
        SceneManager.LoadScene("FactoryScene_3");
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player") 
        {
            isCatch = true;
        }
    }
}
