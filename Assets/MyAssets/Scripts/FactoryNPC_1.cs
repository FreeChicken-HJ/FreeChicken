using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Cinemachine;
public class FactoryNPC_1 : MonoBehaviour
{
    public Slider NpcUI;
    
    public FactoryPlayer_3 player;
    public bool isEbutton;
    public GameObject Ebutton;
    public TextMeshProUGUI E;

    public bool isNear;
    
    public CinemachineVirtualCamera npccam;
    public CinemachineVirtualCamera maincam;
    
    public GameObject npc;
    public GameObject Video;

    public AudioSource BGM;
    public AudioSource Memory;
    //public Animator animator;
    float t = 0;
    void Start()
    {
        Ebutton.SetActive(false);
        player = GameObject.Find("FactoryPlayer").GetComponent<FactoryPlayer_3>();
       
    }
    void Update()
    {
        
        if (Input.GetButton("E") && isEbutton)
        {
            Debug.Log("E");
            E.color = Color.red;
            if (NpcUI.value <100f)
            {
                t += Time.deltaTime;
                NpcUI.value = Mathf.Lerp(0,100,t);
            }
            else
            {
                isEbutton = false;
                Video.SetActive(true);

                E.color = Color.white;
                player.isTalk = true;
                Destroy(Ebutton);
                BGM.Stop();
                Memory.Play();
                Invoke("ReStart", 14f);
                
            }
        }
        if (Input.GetButtonUp("E"))
        {
            t = 0;
            E.color = Color.white;
            NpcUI.value = 0;
        }
        
    }
    void ReStart()
    {
        Video.SetActive(false);
        maincam.Priority = 2;
        npccam.Priority = -5;
        Destroy(this.gameObject);
        BGM.Play();
        Memory.Stop();
        player.isTalk = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            npccam.Priority = 10;
            maincam.Priority = 1;
            Ebutton.SetActive(true);
            isEbutton = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            npccam.Priority = 1;
            maincam.Priority = 10;
            Ebutton.SetActive(false);
            isEbutton = false;
        }
    }

}
