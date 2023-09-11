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
    public ParticleSystem jumpParticle;

    [Header("Bool")]
    public bool isJump;
    public bool isSlide;
    public bool isEbutton;
    public bool isDie;
    public bool isStamp;

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

    public int DeathCount;

    public bool isWallChagneColor;
    public bool isClick;
    
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
   
    public GameObject SavePointTxt;
    public GameObject LoadingUI;

    [Header("Stats")]
    public GameObject eggBoxSpawnTriggerBox;
    public GameObject eggBox;
    public GameObject eggBoxSpawnPos;
    public GameObject EggPrefab;

    
    public GameObject DieParticle;
    public GameObject PickUpParticle;
    public Vector3 pos;

    public GameObject TMP;
    public GameObject StampTMP;
   
    

    public GameObject existingSlideObj;
    public GameObject changeSlideObj;

    public GameObject savePointPos_1;
    public GameObject savePointPos_2;

    public GameObject ChangeEggDoor;

    public GameObject BlockWall;
    public GameManager gameManager;
    [Header("Camera")]
    public CinemachineVirtualCamera mainCam;
    public CinemachineVirtualCamera stopConCam;
    public CinemachineVirtualCamera managerCam;
    public CinemachineVirtualCamera dieCam;
    public CinemachineVirtualCamera pickUpCam;
    public CinemachineVirtualCamera eggChangeCam;


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
        Cursor.visible = false;
        MemoryCount.memCount = 0;
    }
   
    void Update()
    {
        
        if (!isTalk && !isEgg && !isDie && !isStart &&!isPickUp &&!isStamp)
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
        DieCanvas.SetActive(true);
        Invoke("ExitCanvas", 2f);
    }
    IEnumerator Check()
    {
        yield return new WaitForSeconds(1f);


        if (Input.GetButton("E")&&!isSetEggFinish)
        {
            
            
            isEgg = false;
            isClick = false;
            changeEggCanvas.gameObject.SetActive(false);
            yield return new WaitForSeconds(.2f);
            EggPrefab.gameObject.SetActive(false);           
            thisMesh.SetActive(true);
           
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
        transform.LookAt(transform.position + moveVec); 
    }
    public void Jump()
    {
        
        if (Input.GetButtonDown("Jump"))
        {
            if (!isJump)
            {
                jumpParticle.Play();
                jumpAudio.Play();
                isJump = true;
                anim.SetTrigger("doJump");
                rigid.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);  
                
            }
        }
       
    }
  
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Sense") &&!isStamp)
        {
            StampTMP = collision.gameObject;
            PickUpParticle.SetActive(true);
            pickUpCam.Priority = 100;
            mainCam.Priority = 1;
            isSlide = false;
            isStamp = true;
            thisRealObj.gameObject.transform.localScale = new Vector3(2f,0.5f,2f);

            Invoke("PickUP", 2f);

        }
        if(collision.gameObject.CompareTag("PickUpPoc") && !isPickUp)
        {
            TMP = collision.gameObject;
            isPickUp = true;
            isSlide = false;
            pickUpCam.Priority = 100;
            mainCam.Priority = 1;
            PickUpParticle.SetActive(true);
            Invoke("PickUP", 2f);

        }
        if (collision.gameObject.CompareTag("Floor") || collision.gameObject.CompareTag("Slide") || collision.gameObject.CompareTag("EggBox"))
        {

            isJump = false;
        }
       
        if(collision.gameObject.CompareTag("Floor"))
        {
            if (!LoadingUI.activeSelf)
            {
                UpstairCanvas.SetActive(true);
                Invoke("UpstairExit", 2f);
            }
        }
        if (collision.gameObject.CompareTag("ObstacleZone1") || collision.gameObject.CompareTag("Obstacle"))
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
                    Invoke("Scene2Road", 2f);
                }
                else
                {
                    Invoke("ExitCanvas", 2f);
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

        DeadCount.count++;
        
        if (isDie)
        {
            DieCanvas.gameObject.SetActive(false);
            
            
            dieCam.Priority = -3;
            isDie = false;
            DieParticle.SetActive(false);
            anim.SetBool("isDie", false);
        }
        if(isPickUp)
        {   
           
            isPickUp = false;
            pickUpCam.Priority = -5;
            DieCanvas.SetActive(false);
            PickUpParticle.SetActive(false);
        }
        if (isStamp)
        {
        
            isStamp = false;
            thisRealObj.gameObject.transform.localScale = new Vector3(2f, 2f, 2f);
            pickUpCam.Priority = -5;    
            DieCanvas.SetActive(false);
            PickUpParticle.SetActive(false);
        }
       
        mainCam.Priority = 2;
        Pos();

    }
    public void Pos()
    {
        if (isSavePointPos)
        {
            this.gameObject.transform.position = savePointPos_1.gameObject.transform.position;
            BlockWall.SetActive(false);
        }
        else
        {
            this.gameObject.transform.position = pos;
        }
    }
    
    public void OnTriggerEnter(Collider other)
    {
       
        if(other.CompareTag("Rock"))
        {
            mainAudio.Stop();
            mainCam.Priority = -1;
            eggChangeCam.Priority = 10;
            BlockWall.SetActive(true);
            eggChangeZoneAudio.Play();
        }
       
        if (other.CompareTag("StopSlide")) 
        {

            
            mainCam.Priority = 1;
            stopConCam.Priority = 2;
            stopSlideCanvas.gameObject.SetActive(true);
            eggBoxSpawnTriggerBox.SetActive(true);
            existingSlideObj.SetActive(false);
            changeSlideObj.SetActive(true);
            isWallChagneColor = true;


        }
        if(other.CompareTag("EggBoxPos"))
        {
            
            Vector3 pos = eggBoxSpawnPos.transform.position;
            Quaternion rotate = new Quaternion(-0.0188433286f, -0.706855774f, -0.706855536f, 0.0188433584f);
          
            eggBox.SetActive(true);
            eggBox.transform.position = pos;
            eggBox.transform.rotation = rotate;
            
           
        }
        
        if(other.CompareTag("SavePoint_1"))
        {
            isSavePointPos = true;
            savePointAudio.Play();
            savePointPos_1.SetActive(false);
            SavePointTxt.SetActive(true);
            Invoke("DestroySavePointTxt", 2f);
        }
        if(other.CompareTag("SavePoint_2"))
        {
            savePointAudio.Play();
            savePointPos_2.SetActive(false);
            SavePointTxt.SetActive(true);
            UpstairCanvas.SetActive(false);
            Invoke("ReLoadScene_2", 1f);
        }
    }
    
    void ReLoadScene_2()

    {
        gameManager.isLoading = true;
        LoadingUI.SetActive(true);
        mainAudio.Stop();

        Invoke("Scene_2Load", 3f);
       
    }
    void Scene_2Load()
    {
        SceneManager.LoadScene("FactoryScene_2");
    }
    void DestroySavePointTxt()
    {
        SavePointTxt.SetActive(false);
    }

    IEnumerator Egg()
    {
        yield return new WaitForSeconds(3f);
        if (isEgg)
        {
            eggChangeCam.Priority = -100;
            ChangeEggDoor.SetActive(false);
            isSetEggFinish = true;
            eggChangeZoneAudio.Stop();
            heartBeatAudio.Play();
            turnEggCanvas.SetActive(false);
            changeEggCanvas.SetActive(false);
        }
    }
    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Slide") && !isTalk && !isStopSlide)
        {
            isSlide = true;
        }
        if (other.CompareTag("TurnPointR") && !isStopSlide)
        {
            
            this.gameObject.transform.Translate(Vector3.right * Time.deltaTime * 1f, Space.World);
        }
        if (other.CompareTag("TurnPointL") && !isStopSlide)
        {
            
            this.gameObject.transform.Translate(Vector3.left * Time.deltaTime * 1f, Space.World);
        }
        
        if (other.CompareTag("PointZone") && !isSetEggFinish &&!isClick)
        {
            turnEggCanvas.gameObject.SetActive(true);
           
            if (Input.GetButton("F") && !isSetEggFinish)
            {
                isClick = true;

               
                Vector3 pos = other.gameObject.transform.position;

                tmpBox = other.gameObject;
                thisMesh.SetActive(false);
                EggPrefab.gameObject.SetActive(true);
                EggPrefab.transform.position = pos;
                turnEggCanvas.gameObject.SetActive(false);
                changeEggCanvas.gameObject.SetActive(true);
                isEgg = true;
                StartCoroutine(Egg());

            }

          
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Slide"))
        {
            isSlide = false;
        }
        if(other.CompareTag("PointZone"))
        {
            turnEggCanvas.gameObject.SetActive(false);
        }
        if(other.CompareTag("StopSlide"))
        {
            
            mainCam.Priority = 2;
            stopConCam.Priority = 1;
            stopSlideCanvas.gameObject.SetActive(false);
            
        }
        
    }
    
}
