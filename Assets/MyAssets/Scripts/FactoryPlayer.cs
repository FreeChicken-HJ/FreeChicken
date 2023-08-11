using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using Cinemachine;
public class FactoryPlayer : MonoBehaviour
{
    [Header("Setting")]
    public GameObject thisMesh;
    public GameObject thisRealObj;
    public Animator anim;
    public float speed = 2.5f;
    public float hAxis;
    public float vAxis;
    public float jumpPower;
    Vector3 moveVec;
    Rigidbody rigid;
    
    [Header("Bool")]
    public bool isJump;
    public bool isSlide;
    public bool isEbutton;
    public bool isDie;
    public bool isStamp;

    //public FactoryUIManager uIManager;
    public bool isTalk;
    public bool isEgg;
    public bool isStopSlide;

    public bool isContactStopSlide;

    public bool isContact;
    public bool isSetEggFinish;
    public bool isStart;
    public bool isStopCon;
    public bool isPickUp;

    public bool isSavePointPos;

    public bool isPlaying = false;
   
    [Header("UI")]
    public GameObject turnEggCanvas;
    public GameObject changeEggCanvas;
    public GameObject stopSlideCanvas;
    public GameObject tmpBox;
    public TextMeshProUGUI E;
    public TextMeshProUGUI Spacebar;
    public GameObject DieCanvas;
    public GameObject UpstairCanvas;
    public GameObject scene2LastUI;
    public GameObject PickUpUI;
    public GameObject SavePointTxt;


    [Header("Stats")]
    public GameObject eggBoxSpawnTriggerBox;
    public GameObject eggBox;
    public GameObject eggBoxSpawnPos;
    public GameObject EggPrefab;

    
    public GameObject DieParticle;
    public Vector3 pos;

    public GameObject TMP;
    public GameObject StampTMP;
   
    public FactorySceneChangeZone changezone;

    public GameObject existingSlideObj;
    public GameObject changeSlideObj;

    public GameObject savePointPos_1;
    public GameObject savePointPos_2;

    [Header("Camera")]
    public CinemachineVirtualCamera mainCam;
    public CinemachineVirtualCamera stopConCam;
    public CinemachineVirtualCamera managerCam;
    public CinemachineVirtualCamera dieCam;
    public CinemachineVirtualCamera pickUpCam;



    [Header("Audio")]
    public AudioSource runAudio;
    public AudioSource dieAudio;
    public AudioSource jumpAudio;
    public AudioSource savePointAudio;
    public AudioSource eggChangeZoneAudio;
    public AudioSource mainAudio;
    public AudioSource secoundmainAudio; 
    public AudioSource heartBeatAudio;
    public AudioSource fixAudio;
    void Awake()
    {
        mainAudio.Play();
        rigid = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        isTalk = false;
        changezone =  GameObject.Find("ChangeConveyorZone").GetComponent<FactorySceneChangeZone>();
        //fixUI = gameObject.GetComponent<FactoryFixUI>();
    }
   
    void Update()
    {
        
        if (!isTalk && !isEgg && !isDie && !isStart /*&& !changezone.isButton*/&&!isPickUp &&!isStamp)
        {
            Move();
            GetInput();
            Turn();
            Jump();
            
        }
        if (!isTalk && isEgg)
        {
            StartCoroutine("Check");
        }
        if (isTalk && isStamp)
        {
            anim.SetBool("isWalk", false);
        }
        if (changezone.isButton)
        {
            anim.SetBool("isWalk", false);
        }

        if (isPickUp)
        {
            this.gameObject.transform.position = TMP.transform.position;
            
        }
        if (isStamp)
        {
            this.gameObject.transform.position = StampTMP.transform.position;
            
        }
    }
    void PickUP()
    {
        if (!isPlaying)
        {
            isPlaying = true;
            dieAudio.Play();
        }
        PickUpUI.SetActive(true);
        Invoke("ExitCanvas", 2f);
    }
    IEnumerator Check()
    {
        yield return new WaitForSeconds(1f);


        if (Input.GetButton("E")&&!isSetEggFinish)
        {
            Debug.Log("������!");
            E.color = Color.red;
            isEgg = false;
            changeEggCanvas.gameObject.SetActive(false);
            yield return new WaitForSeconds(.5f);
            EggPrefab.gameObject.SetActive(false);
            
            thisMesh.SetActive(true);
           
        }
        E.color = Color.black;
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

            runAudio.Play();

            


        }
        else if (hAxis == 0 && vAxis == 0)
        {
            anim.SetBool("isWalk", false);
            runAudio.Stop();
        }


    }
    void Turn()
    {
        transform.LookAt(transform.position + moveVec); // LookAt(): ������ ���͸� ���ؼ� ȸ�������ִ� �Լ�
    }
    public void Jump()
    {
        
        if (Input.GetButtonDown("Jump"))
        {
            if (!isJump)
            {
                jumpAudio.Play();
                isJump = true;
                anim.SetTrigger("doJump");
                rigid.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);  
                
            }
        }
       
    }
    /*void MoveWithPoc(Collision collision)
    {
        isPickUp = true;
       
    }*/
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Sense" &&!isStamp)
        {
            StampTMP = collision.gameObject;
            pickUpCam.Priority = 100;
            mainCam.Priority = 1;
            isSlide = false;
            isStamp = true;
            thisRealObj.gameObject.transform.localScale = new Vector3(2f,0.5f,2f);

            Invoke("PickUP", 2.5f);

        }
        if(collision.gameObject.tag == "PickUpPoc" && !isPickUp)
        {
            TMP = collision.gameObject;
            isPickUp = true;
            isSlide = false;
            pickUpCam.Priority = 100;
            mainCam.Priority = 1;
            Invoke("PickUP", 2.5f);

        }
        if (collision.gameObject.tag == "Floor" || collision.gameObject.tag == "Slide" || collision.gameObject.tag == "EggBox")
        {

            isJump = false;
        }
       
        if(collision.gameObject.tag == "Floor")
        {
            UpstairCanvas.SetActive(true);
            Invoke("UpstairExit",2f);
        }
        if (collision.gameObject.tag == "ObstacleZone1" || collision.gameObject.tag == "Obstacle")
        {
            if (!isDie && !isPickUp)
            {
                isDie = true;
                anim.SetTrigger("doDie");
                anim.SetBool("isDie", true);

                DieParticle.SetActive(true);
                DieCanvas.SetActive(true);
                dieCam.Priority = 2;
                mainCam.Priority = 1;

                dieAudio.Play();
                
                if (isSetEggFinish)
                {
                    Invoke("Scene2Road", 2.5f);
                }
                else
                {
                    Invoke("ExitCanvas", 2.5f);
                }
            }
            
        }
        
    }
    void Scene2Road()
    {
        SceneManager.LoadScene("FactoryScene_2");
    }
    void UpstairExit()
    {
        UpstairCanvas.SetActive(false);
    }
    void ExitCanvas()
    {
       
        Debug.Log("�ڸ�����");
        if (isDie)
        {
            DieCanvas.gameObject.SetActive(false);
            
            DieParticle.SetActive(false);
            dieCam.Priority = -3;
            isDie = false;
            anim.SetBool("isDie", false);
        }
        if(isPickUp)
        {   
            PickUpUI.SetActive(false);
            isPickUp = false;
            pickUpCam.Priority = -5;
        }
        if (isStamp)
        {
            PickUpUI.SetActive(false);
            isStamp = false;
            thisRealObj.gameObject.transform.localScale = new Vector3(2f, 2f, 2f);
            pickUpCam.Priority = -5;
        }
       
        mainCam.Priority = 2;
        Pos();

    }
    public void Pos()
    {
        if (isSavePointPos)
        {
            this.gameObject.transform.position = savePointPos_1.gameObject.transform.position;
        }
        else
        {
            this.gameObject.transform.position = pos;
        }
    }
    
    public void OnTriggerEnter(Collider other)
    {
       
        if(other.gameObject.tag == "Rock")//eggchangezone
        {
            mainAudio.Stop();
            eggChangeZoneAudio.Play();
        }
        if (other.gameObject.tag == "PointZone"&& !isSetEggFinish)
        {
           
          
               
                
                
                turnEggCanvas.gameObject.SetActive(true);
                
                if (Input.GetButton("E") && !isSetEggFinish)
                {
                    Debug.Log("E");
                    Spacebar.color = Color.red;
                    Vector3 pos = other.gameObject.transform.position;

                    tmpBox = other.gameObject;
                    thisMesh.SetActive(false);
                    EggPrefab.gameObject.SetActive(true);
                    EggPrefab.transform.position = pos;
                    turnEggCanvas.gameObject.SetActive(false);

                    changeEggCanvas.gameObject.SetActive(true);
                    //StartCoroutine("Check");
                    isEgg = true;

                    StartCoroutine(Egg());

                }
            
            Spacebar.color = Color.black;
        }
        if (other.gameObject.tag == "StopSlide") 
        {

            //Debug.Log("��ȭ ����");
            //stopSlideCanvas.gameObject.SetActive(true);
            fixAudio.Stop();
            mainCam.Priority = 1;
            stopConCam.Priority = 2;
            stopSlideCanvas.gameObject.SetActive(true);
            eggBoxSpawnTriggerBox.SetActive(true);
            existingSlideObj.SetActive(false);
            changeSlideObj.SetActive(true);
            


        }
        if(other.gameObject.tag == "EggBoxPos")
        {
            Debug.Log("box�� �浹");
            Vector3 pos = eggBoxSpawnPos.transform.position;
            Quaternion rotate = new Quaternion(-0.0188433286f, -0.706855774f, -0.706855536f, 0.0188433584f);
            /*isSetEggFinish = false;
            eggBox.GetComponent<FactoryMoveEggBox>().isChk = false;*/
            eggBox.SetActive(true);
            eggBox.transform.position = pos;
            eggBox.transform.rotation = rotate;
           
        }
        if (other.gameObject.name == "Rail")
        {
            scene2LastUI.gameObject.SetActive(true);
            Invoke("RoadScene", 5f);
        }
        if(other.gameObject.tag == "SavePoint_1")
        {
            isSavePointPos = true;
            savePointAudio.Play();
            savePointPos_1.SetActive(false);
            SavePointTxt.SetActive(true);
            Invoke("DestroySavePointTxt", 2f);
        }
        if(other.gameObject.tag == "SavePoint_2")
        {
            savePointAudio.Play();
            savePointPos_2.SetActive(false);
            SavePointTxt.SetActive(true);
            Invoke("DestroySavePointTxt", 2f);
        }
    }
    void DestroySavePointTxt()
    {
        SavePointTxt.SetActive(false);
    }
    void RoadScene()
    {
        scene2LastUI.gameObject.SetActive(false);
        SceneManager.LoadScene("FactoryScene_3");
    }
    IEnumerator Egg()
    {
        yield return new WaitForSeconds(3f);
        if (isEgg)
        {
            Debug.Log("�˷� ���� �߰� 3�� ������");
            
            isSetEggFinish = true;
            eggChangeZoneAudio.Stop();
            heartBeatAudio.Play();
            turnEggCanvas.SetActive(false);
            changeEggCanvas.SetActive(false);
        }
    }
    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Slide" && !isTalk && !isStopSlide)
        {
            isSlide = true;
        }
        if (other.tag == "TurnPointR" && !isStopSlide)
        {
            
            this.gameObject.transform.Translate(Vector3.right * Time.deltaTime * 1f, Space.World);
        }
        if (other.tag == "TurnPointL" && !isStopSlide)
        {
            
            this.gameObject.transform.Translate(Vector3.left * Time.deltaTime * 1f, Space.World);
        }
        if (other.tag == "TurnPointD" && !isStopSlide)
        {
            
            this.gameObject.transform.Translate(Vector3.back * Time.deltaTime * 1f, Space.World);

        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Slide")
        {
            isSlide = false;
        }
        if(other.tag == "PointZone")
        {
            turnEggCanvas.gameObject.SetActive(false);
        }
        if(other.tag == "StopSlide")
        {
            mainCam.Priority = 2;
            stopConCam.Priority = 1;
            stopSlideCanvas.gameObject.SetActive(false);
        }
        //speed = 2.5f;
    }
    
}
