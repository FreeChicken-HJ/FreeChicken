using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class FactoryNPC_2 : MonoBehaviour
{
    public Animator animator;
    public Slider NpcUI;
    public GameObject thxUI;
    public FactoryPlayer_3 player;
    public bool isEbutton;
    public GameObject Ebutton;
    public FactoryTextBlink text;
    public bool isNear;
    public GameObject particle;
   
    public GameObject npc;
    float t = 0;
    public GameObject conImage;
    public GameObject changeImage;
    public bool isLastNpc;
    public GameObject[] exitParticle;
    public bool isFinish;
    public GameObject pos;
    public bool isChk;

    public GameObject LastUI;
    public GameObject mainUI;

    void Awake()
    {
        
        Ebutton.SetActive(false);
        player = GameObject.Find("FactoryPlayer").GetComponent<FactoryPlayer_3>();
        animator = GetComponent<Animator>();
        
    }
    private void Start()
    {
        if (isLastNpc)
        {
            player.isLastNPC = true;
        }
    }
    void Update()
    {
        if (player.isTalk)
        {
            animator.SetBool("isTalk", true);

        }
        else if (!player.isTalk)
        {
            animator.SetBool("isTalk",false);
        }
       
        if (Input.GetButton("E") && isEbutton)
        {
            Debug.Log("E");
            if (NpcUI.value < 100f)
            {
                t += Time.deltaTime;
                NpcUI.value = Mathf.Lerp(0, 100, t);
            }
            else
            {
                isEbutton = false;
                player.isTalk = true;
                text.gameObject.SetActive(false);
                thxUI.SetActive(true);

                
                Destroy(Ebutton);
                particle.SetActive(false);
                Invoke("Exit", 2f);
            }
        }
        if (Input.GetButtonUp("E"))
        {
            t = 0;
            NpcUI.value = 0;
        }
        if (isLastNpc && isFinish)
        {
            if (!isChk)
            {
                LastUI.SetActive(true);
            }
            mainUI.SetActive(false);
            Invoke("LastUIExit", 2f);
            for (int i = 0; i < exitParticle.Length; ++i)
            {
                exitParticle[i].gameObject.SetActive(true);
            }
           
            
        }
        if (isFinish)
        {
            this.gameObject.transform.Translate(pos.transform.position * Time.deltaTime * .5f, Space.Self);
            conImage.SetActive(false);
            changeImage.SetActive(true);
        }
    }
    void LastUIExit()
    {
        isChk = true;
        LastUI.SetActive(false);
        
    }
    void Exit()
    {
        player.isTalk = false;
        thxUI.SetActive(false);
        text.gameObject.SetActive(false);
        isFinish = true;
        
        
        Destroy(this.gameObject,3f);

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            text.gameObject.SetActive(true);
            isNear = true;
            isEbutton = true;
            particle.SetActive(true);
            Ebutton.SetActive(true);
        }        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player" && isEbutton)
        {

            isEbutton = false;
            isNear = false;
            Ebutton.SetActive(false);
        }
    }
}
