using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Cinemachine;
using TMPro;
using System.IO;
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

    public GameObject StartParticle;
    public LoadingTyping Loading;
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

    public GameObject Pos0;
    public GameObject Pos1;
   
    public GameObject Pos3;

    public GameObject SavePointObj_1;
    
    public GameObject SavePointObj_3;

    public GameObject SavePosUI;
    public GameManager gameManager;
    [Header("Audio")]
    public AudioSource jumpAudio;
    public AudioSource dieAudio;
    public AudioSource SaveAudio;
    public AudioSource truckLeaveAudio;
    public AudioSource BGM;
    public AudioSource PipeMagicSound;
    public AudioSource StartSound;

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        
        NPC = GameObject.FindWithTag("NPC");
       
    }
    void Start()
    {
        Loading = GetComponent<LoadingTyping>();
        BGM.Play();
        isTalk = true;
        StartSound.Play();
        Invoke("ReStart", 3f);
    }
    void ReStart()
    {
        startUI.SetActive(false);
        StartParticle.SetActive(false);
        isTalk = false;
        StartSound.Stop();
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
            Invoke("Finish", 2f);
            
        }
        
    }
    void Finish()
    {
        GameSave.isHouse = true;
       
        PlayerPrefs.SetInt("GoHouse", GameSave.isHouse ? 1 : 0);

        GameSave.Level = 2;
        PlayerData playerData = new PlayerData();
        playerData.LevelChk = GameSave.Level;
        string json = JsonUtility.ToJson(playerData);

        File.WriteAllText("playerData.json", json);

       //playerData.isEnglish = true;

        LoadSceneInfo.is2DEnterScene = true;
        PlayerPrefs.SetInt("Scene2D", LoadSceneInfo.is2DEnterScene ? 1 : 0);
        LoadSceneInfo.LevelCnt = 2;
        SceneManager.LoadScene("LoadingScene");
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
        if (other.gameObject.CompareTag("Exit")) 
        {
            
            ExitUI.gameObject.SetActive(true);
            isEbutton = true;
           
           
        }
      
       if(other.gameObject.CompareTag("SavePoint_1"))
        {
            isSavePointChk = true;
            isSavePoint_1 = true;
           
            isSavePoint_2 = false;
            SavePointObj_1.SetActive(false);
            SaveAudio.Play();
            SavePosUI.SetActive(true);
            Invoke("ResetUI", 3f);
        }
   
       if(other.gameObject.CompareTag("SavePoint_2"))
        {
            isSavePointChk = true;
            isSavePoint_1 = false;
            
            isSavePoint_2 = true;
            SaveAudio.Play();
            SavePointObj_3.SetActive(false);
            SavePosUI.SetActive(true);
            Invoke("ResetUI", 3f);
        }
        if (other.gameObject.CompareTag("Poison"))
        {
            isPotion = true;
            mainCam.Priority = -1;
            potionCam.Priority = 100;
            PipeMagicSound.Play();
            
           
            Invoke("ResetCam", 2f);
            
        }
        if(other.gameObject.CompareTag("Item"))
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

        if (collision.gameObject.CompareTag("Floor") || collision.gameObject.CompareTag("Slide") || collision.gameObject.CompareTag("EggBox") || collision.gameObject.CompareTag("Props") || collision.gameObject.CompareTag("PickUpPoc") || collision.gameObject.CompareTag("Poison"))
        {

            isJump = false;
        }
        if (collision.gameObject.CompareTag("ObstacleZone3")|| collision.gameObject.CompareTag("Obstacle")&&!isDie)
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
        if (collision.gameObject.CompareTag("Floor"))
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
        mainCam.Priority = 2;
        dieCam.Priority = 1;
        
        anim.SetBool("isDie", false);
        this.gameObject.transform.position = Pos0.transform.position;
    }
    void ReSpawnCanvas()
    {
        DeadCount.count++;
        DieCanvas.gameObject.SetActive(false);
        isDie = false;

        DieParticle.SetActive(false);
        mainCam.Priority = 2;
        dieCam.Priority = 1;
        
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
