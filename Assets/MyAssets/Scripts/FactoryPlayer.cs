using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using Cinemachine;
public class FactoryPlayer : MonoBehaviour
{
    public GameObject thisMesh;
    public Animator anim;
    public float speed = 2.5f;
    public float hAxis;
    public float vAxis;
    Vector3 moveVec;
    Rigidbody rigid;

    public bool isJump;
    public float jumpPower;
    public bool isSlide;
    public bool isEbutton;
    public bool isDie;


    //public FactoryUIManager uIManager;
    public bool isTalk;

    public GameObject EggPrefab;
    public bool isEgg;
    public GameObject turnEggCanvas;
    public GameObject changeEggCanvas;
    public GameObject stopSlideCanvas;
    public bool isStopSlide;

    public bool isContactStopSlide;
    
    public bool isContact;

    
    public GameObject eggBoxSpawnTriggerBox;
    public GameObject eggBox;
    public GameObject eggBoxSpawnPos;
    public bool isSetEggFinish;
    public GameObject tmpBox;
    public TextMeshProUGUI E;
    public TextMeshProUGUI Spacebar;

    public GameObject DieParticle;
    public GameObject DieCanvas;
    public GameObject StartCanvas;
    public bool isStart;
    public Vector3 pos;

    
    public GameObject UpstairCanvas;
    public GameObject scene2LastUI;

    public FactorySceneChangeZone changezone;

    public CinemachineVirtualCamera mainCam;
    public CinemachineVirtualCamera stopConCam;
    public CinemachineVirtualCamera managerCam;
    public CinemachineVirtualCamera dieCam;
    public bool isStopCon;

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        isTalk = false;
        changezone =  GameObject.Find("ChangeConveyorZone").GetComponent<FactorySceneChangeZone>();
        //fixUI = gameObject.GetComponent<FactoryFixUI>();
    }
    void Start()
    {

        //StartCanvas.SetActive(true);
        
        
    }
    void Update()
    {
        
        if (!isTalk && !isEgg && !isDie && !isStart && !changezone.isButton)
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
        if (isTalk)
        {
            anim.SetBool("isWalk", false);
        }
        if (changezone.isButton)
        {
            anim.SetBool("isWalk", false);
        }
       

    }
 
    IEnumerator Check()
    {
        yield return new WaitForSeconds(1f);


        if (Input.GetButton("E")&&!isSetEggFinish)
        {
            Debug.Log("존변경!");
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
    void OnCollisionEnter(Collision collision)
    {
        
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
            if (!isDie)
            {
                isDie = true;
                anim.SetTrigger("doDie");
                anim.SetBool("isDie", true);

                DieParticle.SetActive(true);
                DieCanvas.SetActive(true);
                dieCam.Priority = 2;
                mainCam.Priority = 1;
                
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
        Pos();
        Debug.Log("자리변경");
        
        DieCanvas.gameObject.SetActive(false);
        DieParticle.SetActive(false);
        dieCam.Priority = -3;
        mainCam.Priority = 2;
        isDie = false;
        
    }
    public void Pos()
    {
        anim.SetBool("isDie", false);
        this.gameObject.transform.position = pos;

    }
    
    public void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.tag == "PointZone" && !isSetEggFinish)
        {
            turnEggCanvas.gameObject.SetActive(true);
            if (Input.GetButton("E") && !isSetEggFinish){
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

            //Debug.Log("대화 종료");
            //stopSlideCanvas.gameObject.SetActive(true);
            mainCam.Priority = 1;
            stopConCam.Priority = 2;
            stopSlideCanvas.gameObject.SetActive(true);
            eggBoxSpawnTriggerBox.SetActive(true);
            

        }
        if(other.gameObject.tag == "EggBoxPos")
        {
            Debug.Log("box에 충돌");
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
            Debug.Log("알로 변신 했고 3초 지났음");
            
            isSetEggFinish = true;

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
            stopSlideCanvas.gameObject.SetActive(false);
        }
        //speed = 2.5f;
    }
    
}
