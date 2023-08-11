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

    public GameObject Video;
    
    public FactoryTextBlink text;
    public bool isNear;
  
    public CinemachineVirtualCamera mainCam;
    public CinemachineVirtualCamera npcCam;
    public GameObject npc;

    public AudioSource BGM;
    public AudioSource MelodyBox;
    //public Animator animator;
    public float t;
    void Start()
    {
        
        Ebutton.SetActive(false);
        player = GameObject.FindWithTag("Player").GetComponent<FactoryPlayer>();
      
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
                Video.SetActive(true);
                BGM.Stop();
                MelodyBox.Play();
                isEbutton = false;
               
                player.isStopSlide = true;
                player.isSlide = false;
                player.isTalk = true;
                Invoke("ReStart", 6.5f);
                

            }
        }
        if (Input.GetButtonUp("E"))
        {
            E.color = Color.white;
            t = 0;
            NpcUI.value = 0;
        }
        
    }
   void ReStart()
    {
        Video.SetActive(false);
       

        Destroy(this.gameObject);
        Destroy(text.gameObject);

        Destroy(Ebutton);
        BGM.Play();
        MelodyBox.Stop();
        factoryUI.gameObject.SetActive(true); 
    }
    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            isNear = true;
            isEbutton = true;
            Ebutton.SetActive(true);
            mainCam.Priority = 1;
            npcCam.Priority=2;
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            isNear = false;
            isEbutton = false;
            Ebutton.SetActive(false);
            mainCam.Priority = 2;
            npcCam.Priority = 1;
        }
    }

}
