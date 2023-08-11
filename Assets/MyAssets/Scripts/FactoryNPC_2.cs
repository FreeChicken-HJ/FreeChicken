using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class FactoryNPC_2 : MonoBehaviour
{
    public Animator animator;
    public GameObject player;
    public GameObject NpcBox;
    public GameObject ThxUI;

   
    public bool isFly;

    public Slider mainSlider;
    
    public GameObject NPCHelpUI;
    public Slider HelpNPC;
    public TextMeshProUGUI E;

    public bool isShowNPCButton;
    public bool isFin;

    public bool isSet;
    public GameObject HelpParticle;
    public float t;

    public AudioSource NpcSaveAudio;
    public AudioSource NpcFlyAudio;
    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isFin)
        {
            NpcBox.SetActive(false); // 효과추가
            ThxUI.SetActive(true);
            animator.SetBool("isTalk", true);
            NPCHelpUI.gameObject.SetActive(false);
            NpcFlyAudio.Play();

            Invoke("Finish", 1.5f);
            

        }
     
        if (isFly)
        {
            this.gameObject.transform.Translate(Vector3.up * 5f*Time.deltaTime);
            ThxUI.SetActive(false);
            Invoke("Destroy", 3f);
            //Destroy(this.gameObject,5f);
        }
    }
    void Destroy()
    {
       
        this.gameObject.SetActive(false);
    }
    private void FixedUpdate()
    {
        if (isShowNPCButton)
        {
            if (Input.GetButton("E") && isShowNPCButton)
            {
                E.color = Color.red;
                if (HelpNPC.value < 100f)
                {
                    HelpParticle.SetActive(true);
                    t += Time.deltaTime;
                    HelpNPC.value = Mathf.Lerp(0, 100, t);
                }
                else
                {
                    HelpParticle.SetActive(false);
                    HelpNPC.value = 0;
                    E.color = Color.white; // 초기화
                    NpcSaveAudio.Play();
                    isFin = true;
                    isShowNPCButton = false;
                    player.GetComponent<FactoryPlayer_3>().maxValue = player.GetComponent<FactoryPlayer_3>().minValue + 12.5f;


                    mainSlider.value += 12.5f;
                    player.GetComponent<FactoryPlayer_3>().minValue = player.GetComponent<FactoryPlayer_3>().maxValue;

                    Debug.Log(player.GetComponent<FactoryPlayer_3>().minValue);
                    Debug.Log(player.GetComponent<FactoryPlayer_3>().maxValue);

                }

            }
            if (Input.GetButtonUp("E"))
            {
                E.color = Color.white;
                t = 0;
                HelpNPC.value = 0;

            }
        }
    }
    void Finish()
    {
        isFly = true;
      
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" &&!isShowNPCButton)
        {

            Debug.Log("NPC");
            NPCHelpUI.gameObject.SetActive(true);
            isShowNPCButton = true;


        }
    }
    private void OnTriggerExit(Collider other)
    {
        NPCHelpUI.gameObject.SetActive(false);
        isShowNPCButton = false;
    }
}
