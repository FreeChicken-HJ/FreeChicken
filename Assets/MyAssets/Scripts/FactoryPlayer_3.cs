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
    public FactorySceneBomb Bomb;
    public GameObject DieParticle;
    public GameObject TMP;
    public GameObject PotionTMP;

    public GameObject NPC;
    [Header("Camera")]
    public CinemachineVirtualCamera mainCam;
    public CinemachineVirtualCamera changeCam;
    public CinemachineVirtualCamera dieCam;
    public CinemachineVirtualCamera potionCam;
   
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
    
    public bool isPickUp;

    public bool isSavePointChk;

    public bool isSavePoint_1;
    public bool isSavePoint_2;  
    public bool isSavePoint_3;
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

    
    public float minValue;
    public float maxValue;
    
    public GameObject Pos1;
    public GameObject Pos2;
    public GameObject Pos3;

    public GameObject SavePointObj_1;
    public GameObject SavePointObj_2;
    public GameObject SavePointObj_3;

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        
        NPC = GameObject.FindWithTag("NPC");
    }
    void Start()
    {
        startUI.SetActive(true);
        mainUI.SetActive(false); // 치킨으로 성장하는 퍼센트
        Invoke("NewStart", 2f);
    }
    void NewStart()
    {
        startUI.SetActive(false);
        mainUI.SetActive(true);
        mainCam.Priority = 3;
        changeCam.Priority = 1;
        dieCam.Priority = 2;
    }
    void Update()
    {

        if (/*!isTalk && */!isDie && !isAttack&& !isTruckGo && !isPotion)
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
            //SceneManager.LoadScene() // 가정집
        }
        
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
                }
                else
                {
                    this.gameObject.transform.position = truckPos.transform.position;

                    ExitUI.gameObject.SetActive(false);
                    changeCam.Priority = 5;
                    mainCam.Priority = -5;
                    Debug.Log("트럭출동");
                    
                    // 검은 화면 3초
                    // 다음씬으로 이어지게
                    isEbutton = false;
                    //.LoadScene("CityScene");
                    isTruckGo = true;
                }
                
            }

            if (Input.GetButtonUp("E"))
            {
                t = 0;
                OnTruck.value = 0;
            }
        }
       
        
    }
    // Update is called once per frame
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
        transform.LookAt(transform.position + moveVec); // LookAt(): 지정된 벡터를 향해서 회전시켜주는 함수
    }
    public void Jump()
    {

        if (Input.GetButtonDown("Jump"))
        {
            if (!isJump)
            {
                isJump = true;
                anim.SetTrigger("doJump");
                rigid.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);

            }

        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Exit" /*&& isLastNPC*/) // 마지막 npc 구해야만 트럭 UI 뜨게 설정
        {
            Debug.Log("출구");
            ExitUI.gameObject.SetActive(true);
            isEbutton = true;
           
           
        }
      
       if(other.gameObject.tag == "SavePoint_1")
        {
            isSavePointChk = true;
            isSavePoint_1 = true;
            isSavePoint_2 = false;
            isSavePoint_3 = false;
            SavePointObj_1.SetActive(false);
        }
       if(other.gameObject.tag == "SavePoint_2")
        {
            isSavePointChk = true;
            isSavePoint_1 = false;
            isSavePoint_2 = true;
            isSavePoint_3 = false;
            SavePointObj_2.SetActive(false);
        }
       if(other.gameObject.tag == "SavePoint_3")
        {
            isSavePointChk = true;
            isSavePoint_1 = false;
            isSavePoint_2 = false;
            isSavePoint_3 = true;
            SavePointObj_3.SetActive(false);
        }
        if (other.gameObject.tag == "Poison")
        {
            isPotion = true;
            mainCam.Priority = -1;
            potionCam.Priority = 100;
            //위로 올라가는 소리 추가 필요 7.21
            //PotionTMP = this.gameObject.transform.position;
            Invoke("ResetCam", 3f);
            
        }
    }
    void ResetCam()
    {
        this.gameObject.transform.position = PotionTMP.transform.position;
        
        potionCam.Priority = -5;
        mainCam.Priority = 100;
        isPotion = false;
    }
    void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "Floor" || collision.gameObject.tag == "Slide" || collision.gameObject.tag == "EggBox" || collision.gameObject.tag == "Props" || collision.gameObject.tag == "PickUpPoc" || collision.gameObject.tag == "Poison")
        {

            isJump = false;
        }
        if (collision.gameObject.tag == "ObstacleZone3"|| collision.gameObject.tag == "Obstacle")
        {
            isDie = true;
            anim.SetTrigger("doDie");
            anim.SetBool("isDie",true);
            mainCam.Priority = 1;
            dieCam.Priority = 2;
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
            Invoke("UpstairExit", 2f);
        }
        if (collision.gameObject.tag == "PickUpPoc" && !isPickUp)
        {
            TMP = collision.gameObject;
            isPickUp = true;


            //transform.Translate()
            //

            
            /*pickUpCam.Priority = 100;
            mainCam.Priority = 1;
            Invoke("PickUP", 4f);*/

        }
    }
   
    void UpstairExit()
    {
        UpstairUI.SetActive(false);
    }

    void ExitCanvas()
    {

        DieCanvas.gameObject.SetActive(false);
        isDie = false;
       
        DieParticle.SetActive(false);
        SceneManager.LoadScene("FactoryScene_3");
        
    }
    void ReSpawnCanvas()
    {
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
            this.gameObject.transform.position = Pos2.transform.position;
        }
        if (isSavePoint_3)
        {
            this.gameObject.transform.position = Pos3.transform.position;
        }
    }
}
