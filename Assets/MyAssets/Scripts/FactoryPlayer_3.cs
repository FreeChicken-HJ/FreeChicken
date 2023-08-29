using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Cinemachine;
using TMPro;
public class FactoryPlayer_3 : MonoBehaviour
{
    [Header("Settings")]
    public Animator anim;
    public float speed = 2.5f;
    public float hAxis;
    public float vAxis;
    public float jumpPower; 
    public int AttackCnt;
    
    Vector3 moveVec;
    Rigidbody rigid;
    
    public ParticleSystem attackParticle;
    public ParticleSystem jumpParticle;
    
    public GameObject DieParticle;
    
    public GameObject PotionTMP;
    public GameObject PotionTMP_2;

    public GameObject NPC;
    
    //public GameState gameState;
    [Header("Camera")]
    public CinemachineVirtualCamera mainCam;
    public CinemachineVirtualCamera changeCam;
    public CinemachineVirtualCamera dieCam;
    public CinemachineVirtualCamera potionCam;
    public CinemachineVirtualCamera potionCam_2;
   
    [Header("Bool")]
    public bool isJump;
    public bool isSlide;
    public bool isEbutton;
    public bool isDie;
    public bool isFin;
    public bool isStopSlide;
    public bool isContact;
    public bool isAttack;
    public bool isTruckGo;
    public bool isTalk;
    public bool isPickUp;

    public bool isSavePointChk;

    public bool isSavePoint_1;
    
    public bool isSavePoint_2;
    public bool isPotion;
   
    [Header("UI")]
    public GameObject startUI;
    public GameObject mainUI;
    
    public GameObject DieCanvas;
    public GameObject ExitUI;  
    public Slider OnTruck;
    float t;

    public GameObject UpstairUI;

    public GameObject LastUI;
    public GameObject truckPos;
    public GameObject Truck;
    public TextMeshProUGUI EButton;
    
    public float minValue;
    public float maxValue;
    
    public GameObject Pos1;
    public GameObject Pos2;
    public GameObject Pos3;

    public GameObject SavePointObj_1;
    
    public GameObject SavePointObj_3;

    public GameObject SavePosUI;

    [Header("Audio")]
    public AudioSource jumpAudio;
    public AudioSource dieAudio;
    public AudioSource SaveAudio;
    public AudioSource truckLeaveAudio;
    public AudioSource BGM;
    public AudioSource PipeMagicSound;


    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        
        NPC = GameObject.FindWithTag("NPC");
       
    }
    void Start()
    {
        
        BGM.Play();
    }
 
    void Update()
    {

        if (!isTalk && !isDie && !isAttack&& !isTruckGo && !isPotion)
        {
            Move();
            GetInput();
            Turn();
            Jump();
            
        }
      
        if (isTruckGo)
        {
            Truck.gameObject.transform.Translate(Vector3.forward * Time.deltaTime * 4f);
            this.gameObject.transform.position = truckPos.transform.position;
            LastUI.SetActive(true);
            Invoke("Finish", 3f);
            
        }
        
    }
    void Finish()
    {
        GameSave.isHouse = true;
        PlayerPrefs.SetInt("GoHouse", GameSave.isHouse? 1:0);
        SceneManager.LoadScene("Enter2DScene"); 
    }
    private void FixedUpdate()
    {
        if (isEbutton)
        {
            if (Input.GetButton("E") && isEbutton)
            {

                if (OnTruck.value < 100f)
                {
                    t += Time.deltaTime;
                    OnTruck.value = Mathf.Lerp(0, 100, t);
                    EButton.color = Color.red;
                }
                else
                {
                    this.gameObject.transform.position = truckPos.transform.position;

                    ExitUI.gameObject.SetActive(false);
                    changeCam.Priority = 5;
                    mainCam.Priority = -5;
                    
                    truckLeaveAudio.Play();
                    isEbutton = false;
                    
                    isTruckGo = true;
                }
                
            }

            if (Input.GetButtonUp("E"))
            {
                t = 0;
                OnTruck.value = 0;
                EButton.color = Color.white;
            }
        }
       
        
    }

    public void GetInput()
    {

        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");

    }

    void Move()
    {
        

        if (!(hAxis == 0 && vAxis == 0))
        {
            moveVec = new Vector3(hAxis, 0, vAxis).normalized;

            transform.position += moveVec * speed * Time.deltaTime * 1f;
            anim.SetBool("isWalk", true);


        }
        else if (hAxis == 0 && vAxis == 0)
        {
            anim.SetBool("isWalk", false);
        }



    }
    void Turn()
    {
        transform.LookAt(transform.position + moveVec); 
    }
    public void Jump()
    {

        if (Input.GetButtonDown("Jump"))
        {
            if (!isJump)
            {

                isJump = true;
                jumpParticle.Play();
                jumpAudio.Play();
                anim.SetTrigger("doJump");
                rigid.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);

            }

        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Exit") 
        {
            Debug.Log("�ⱸ");
            ExitUI.gameObject.SetActive(true);
            isEbutton = true;
           
           
        }
      
       if(other.gameObject.tag == "SavePoint_1")
        {
            isSavePointChk = true;
            isSavePoint_1 = true;
           
            isSavePoint_2 = false;
            SavePointObj_1.SetActive(false);
            SaveAudio.Play();
            SavePosUI.SetActive(true);
            Invoke("ResetUI", 3f);
        }
   
       if(other.gameObject.tag == "SavePoint_2")
        {
            isSavePointChk = true;
            isSavePoint_1 = false;
            
            isSavePoint_2 = true;
            SaveAudio.Play();
            SavePointObj_3.SetActive(false);
            SavePosUI.SetActive(true);
            Invoke("ResetUI", 3f);
        }
        if (other.gameObject.tag == "Poison")
        {
            isPotion = true;
            mainCam.Priority = -1;
            potionCam.Priority = 100;
            PipeMagicSound.Play();
            
           
            Invoke("ResetCam", 2f);
            
        }
        if(other.gameObject.tag == "Item")
        {
            isPotion = true;
            mainCam.Priority = -1;
            potionCam_2.Priority = 100;
            PipeMagicSound.Play();
            Invoke("ResetCam_1", 2f);
        }
    }
    void ResetUI()
    {
        SavePosUI.SetActive(false);
    }
    void ResetCam()
    {
        this.gameObject.transform.position = PotionTMP.transform.position;
        
        potionCam.Priority = -5;
        mainCam.Priority = 100;
        isPotion = false;
        PipeMagicSound.Stop();
    }
    void ResetCam_1()
    {
        this.gameObject.transform.position = PotionTMP_2.transform.position;

        potionCam_2.Priority = -5;
        mainCam.Priority = 100;
        isPotion = false;
        PipeMagicSound.Stop();
    }
    void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "Floor" || collision.gameObject.tag == "Slide" || collision.gameObject.tag == "EggBox" || collision.gameObject.tag == "Props" || collision.gameObject.tag == "PickUpPoc" || collision.gameObject.tag == "Poison")
        {

            isJump = false;
        }
        if (collision.gameObject.tag == "ObstacleZone3"|| collision.gameObject.tag == "Obstacle"&&!isDie)
        {
            
            isDie = true;
            anim.SetTrigger("doDie");
            anim.SetBool("isDie",true);
            mainCam.Priority = 1;
            dieCam.Priority = 2;
            dieAudio.Play();
            changeCam.Priority = -1;
            DieParticle.SetActive(true);
            DieCanvas.SetActive(true);
            if (!isSavePointChk)
            {
                Invoke("ExitCanvas", 1.5f);
            }
            else if (isSavePointChk)
            {
                Invoke("ReSpawnCanvas", 2f);
                
            }
        }
        if (collision.gameObject.tag == "Floor")
        {
            UpstairUI.gameObject.SetActive(true);
            Invoke("UpstairExit", 1f);
        }
        
    }
   
    void UpstairExit()
    {
        UpstairUI.SetActive(false);
    }

    void ExitCanvas()
    {
        DeadCount.count++;
        DieCanvas.gameObject.SetActive(false);
        isDie = false;
       
        DieParticle.SetActive(false);
        SceneManager.LoadScene("FactoryScene_3");
        
    }
    void ReSpawnCanvas()
    {
        DeadCount.count++;
        DieCanvas.gameObject.SetActive(false);
        isDie = false;

        DieParticle.SetActive(false);
        mainCam.Priority = 2;
        dieCam.Priority = 1;
        //isSavePointChk = false;
        anim.SetBool("isDie", false);
        if (isSavePoint_1)
        {
            this.gameObject.transform.position = Pos1.transform.position;
        }
        
        if (isSavePoint_2)
        {
            this.gameObject.transform.position = Pos3.transform.position;
        }
    }
}
