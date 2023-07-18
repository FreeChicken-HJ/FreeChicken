using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Cinemachine;
public class FactoryNPC : MonoBehaviour
{
    public Slider NpcUI;
    public GameObject factoryUI;
    public FactoryPlayer player;
    public bool isEbutton;
    public GameObject Ebutton;
    public TextMeshProUGUI E;

    public GameObject Dieparticle;
    public FactoryTextBlink text;
    public bool isNear;
  
    public CinemachineVirtualCamera mainCam;
    public CinemachineVirtualCamera npcCam;
    public GameObject npc;

    //public Animator animator;
    public float t;
    void Start()
    {
        
        Ebutton.SetActive(false);
        player = GameObject.Find("FactoryPlayer").GetComponent<FactoryPlayer>();
        t = 0;
    }
    void Update()
    {
        //Check();
        if (Input.GetButton("E") && isEbutton)
        {
           
            E.color = Color.red;
            Debug.Log("E");
            if (NpcUI.value <100f)
            {
                t += Time.deltaTime;
                NpcUI.value = Mathf.Lerp(0,100,t);
            }
            else
            {
                isEbutton = false;
                
                npcCam.Priority = 2;
                mainCam.Priority = 1;
                player.transform.LookAt(npc.transform.position);
                Dieparticle.SetActive(false);
                player.isTalk = true;
                player.hAxis = 0;
                player.vAxis = 0;
                //player.transform.LookAt(npc);
                player.isStopSlide = true;
                player.isSlide = false;
                Destroy(this.gameObject);
                Destroy(text.gameObject);

                Destroy(Ebutton);
                
                factoryUI.gameObject.SetActive(true);
                
            }
        }
        if (Input.GetButtonUp("E"))
        {
            E.color = Color.white;
            t = 0;
            NpcUI.value = 0;
        }
        
    }
   /* public void Check()
    {
        if (Physics.Raycast(this.transform.position, this.transform.forward, 2f, LayerMask.GetMask("Player")))
        {
           
           
        }
        
        *//*else if(Physics.Raycast(this.transform.position, this.transform.forward, 10f, LayerMask.GetMask("Player")))
        {
            isEbutton = false;
            isNear = false;
            Ebutton.SetActive(false);
            
        }*//*


    }*/
    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            isNear = true;
            isEbutton = true;
            Ebutton.SetActive(true);
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            isNear = false;
            isEbutton = false;
            Ebutton.SetActive(false);
        }
    }

}
