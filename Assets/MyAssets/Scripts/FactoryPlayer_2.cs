using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Cinemachine;

public class FactoryPlayer_2 : MonoBehaviour
{
    [Header("Settings")]
    public Animator anim;
    public float speed = 2.5f;
    public float hAxis;
    public float vAxis;
    public float jumpPower;
    Vector3 moveVec;
    Rigidbody rigid;
    public GameObject thisRealObj;

    [Header("Bool")]
    public bool isJump;
    public bool isSlide;
    public bool isEbutton;
    public bool isDie;
    public bool isStamp;
    public bool isTalk;
    public bool isStopSlide; 
    public bool isContact;
    
    [Header("Stats")]
    public GameObject StampTMP;
    public GameObject DieCanvas;
    public GameObject DieParticle;
    public FactorySceneChangeZone changeZone;
    
    [Header("UI")]
    public GameObject scene2LastUI;
    public GameObject PickUpUI;

   
    [Header("Camera")]
    public CinemachineVirtualCamera mainCam;
    public CinemachineVirtualCamera DieCam;
    public CinemachineVirtualCamera pickUpCam;

    [Header("Audio")]
    public AudioSource jumpAudio;
    public AudioSource BGM;
    public AudioSource dieAudio;
    public AudioSource changeConAudio;
    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        isTalk = false;
        changeZone = GameObject.Find("ChangeConveyorZone").GetComponent<FactorySceneChangeZone>();
        BGM.Play();
        //fixUI = gameObject.GetComponent<FactoryFixUI>();
    }
   
    void Update()
    { 

        if (!isTalk && !isDie /*&& !changeZone.isButton*/)
        {
            Move();
            GetInput();
            Turn();
            Jump();
            
        }
        if (changeZone.isButton)
        {
            anim.SetBool("isWalk", false);
        }
        if (isStamp)
        {
            this.gameObject.transform.position = StampTMP.transform.position;
        }
    }
    void PickUP()
    {

        PickUpUI.SetActive(true);
        dieAudio.Play();
        Invoke("ExitCanvas", 2f);
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
                jumpAudio.Play();
                anim.SetTrigger("doJump");
                rigid.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);

            }

        }

    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Sense" && !isStamp)
        {
            StampTMP = collision.gameObject;
            pickUpCam.Priority = 100;
            mainCam.Priority = 1;
            isSlide = false;
            isStamp = true;
            thisRealObj.gameObject.transform.localScale = new Vector3(2f, 0.5f, 2f);

            Invoke("PickUP", 4f);

        }

        if (collision.gameObject.tag == "Floor" || collision.gameObject.tag == "Slide" || collision.gameObject.tag == "EggBox")
        {

            isJump = false;
        }
        if (collision.gameObject.tag == "ObstacleZone2" || collision.gameObject.tag == "Obstacle")
        {
            isDie = true;
            DieParticle.SetActive(true);
            anim.SetTrigger("doDie");
            dieAudio.Play();
            anim.SetBool("isDie", true);
            DieCanvas.SetActive(true);
            mainCam.Priority = 1;
            DieCam.Priority = 2;
            Invoke("ExitCanvas", 2f);
        }
        
    }
    void ExitCanvas()
    {
        if (isDie)
        {
            DieCanvas.gameObject.SetActive(false);
            isDie = false;
            DieParticle.SetActive(false);
        }
        /*if (isStamp)
        {
            PickUpUI.SetActive(false);
            isStamp = false;
            //thisRealObj.gameObject.transform.localScale = new Vector3(2f, 2f, 2f);
            //pickUpCam.Priority = -5;
        }*/
        SceneManager.LoadScene("FactoryScene_2");

    }
    private void OnTriggerEnter(Collider other)
    {
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
    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Slide")
        {
            
            this.gameObject.transform.Translate(Vector3.forward * Time.deltaTime * 2f, Space.World);
           
            
            // 이동하는 방향 쳐다보게 설정
        }
        if (other.tag == "TurnPointR")
        {

            this.gameObject.transform.Translate(Vector3.right * Time.deltaTime * 1f, Space.World);
           
        }
        if (other.tag == "TurnPointL")
        {

            this.gameObject.transform.Translate(Vector3.left * Time.deltaTime * 1f, Space.World);
            
        }
        if (other.tag == "TurnPointD")
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
       
        //speed = 2.5f;
    }


}
