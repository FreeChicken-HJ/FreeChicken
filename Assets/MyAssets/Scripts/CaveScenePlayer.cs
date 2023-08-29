using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.UI;
using Cinemachine;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using UnityEngine.UIElements;
using Unity.Collections.LowLevel.Unsafe;

public class CaveScenePlayer : MonoBehaviour
{
    //[SerializeField] private Transform characterBody;
    //[SerializeField] private Transform cameraArm;
    [Header("Settings")]
    Vector3 moveVec;
    bool wDown;
    bool Dash;
    public bool Dead;
    public bool isMove;

    Rigidbody rigid;

    //프리닭 움직일때 파티클효과
    public ParticleSystem MoveParticle;
    public ParticleSystem PoisonParticle;
    public ParticleSystem DiePs;

    //private ParticleSystem fire;
    //bool isMove;
    public float speed;
    public float jumpPower = 5f;
    public float hAxis;
    public float vAxis;
    public float rhAxis;
    public float rvAxis;

    // 반대로 움직이는 로직
    private bool isReversed = false;
    private float reverseDuration =7.0f;
    

    float time;
    Animator anim;
    Obstacle_Cave obstacle;
    //GameManager_Cave manager;
    //FireTest firetest;
    //CaveItem_DebuffPotion potion;
    //ObstacleTest ObstacleTestobstacleTest;
    public int keyCount;

    [Header("Bool")]
    public bool isSense;
    public bool isSenseTest;
    public bool isfallingObstacle;
    public bool isMoveUp;
    bool isJump;
    //public ParticleSystem jumpPs;
    //public bool playJumpPs;
    public bool hasKey;
    bool iDown;
    //bool isJump;
    //public float jumpPower = 5f;
    //public int jumpCount = 2;   // 점프횟수, 2를 3으로 바꾸면 3단 점프

    
    public bool Talk_NPC1;
    public bool Talk_NPC2;
    public bool Talk_NPC3;
    public bool Talk_NPC4;
    public bool Talk_NPC5;

    public bool isChk;
    public bool isAnimChk;
    public bool isFollow;
    public bool isFinal;

    [Header("Camera")]
    //Cinemachine Camera
    public CinemachineVirtualCamera mainCam;
    public CinemachineVirtualCamera FirstCam;
    public CinemachineVirtualCamera TalkToDaddy2Cam;
    public CinemachineVirtualCamera TogetherCam;
    public CinemachineVirtualCamera FDadTalkCam;
    public CinemachineVirtualCamera NPC4Cam;
    public CinemachineVirtualCamera NPC3Cam;
    public CinemachineVirtualCamera MomCam;
    public CinemachineVirtualCamera mainCam2;
    public CinemachineVirtualCamera FinalCam;
    public CinemachineVirtualCamera DadDieCam;

    [Header("UI")]  
    CaveSceneTalkManager talkManager;
    public GameObject image1;
    public GameObject image2;
    public GameObject image3;
    public GameObject image4;
    public GameObject image5;
    public bool isTalk;
    public GameObject StopPleaseUI;
    public GameObject GetUpgradeBox_text;
    [Header("SavePoint")]
    //SavePoint
    public GameObject SavePointImage;
    public GameObject SavePoint0Obj;
    public GameObject SavePoint1Obj;
    public GameObject SavePoint2Obj;
    public GameObject SavePoint3Obj;
    public GameObject SavePoint4Obj;
    public bool check_savepoint0;
    public bool check_savepoint1;
    public bool check_savepoint2;
    public bool check_savepoint3;
    public bool check_savepoint4;

    [Header("Audio")]
    public AudioSource savePointAudio;
    public AudioSource mainAudio;
    public AudioSource runAudio;
    public AudioSource dieAudio;
    public AudioSource jumpAudio;
    public AudioSource TalkAudio;
    public AudioSource OpenDoorAudio;
    public AudioSource debuffAudio;
    public AudioSource ElevtorAudio;
    public AudioSource fallingAudio;

    // MomDown
    public GameObject Mom;
    public GameObject FollowMom;
    Animator MomDownAnim;
    bool isMomContact;

    [Header("Dad")]
    public GameObject Dad;
    public GameObject DadDieUI;
    public AudioSource BombSound;
    public GameObject BombParticle;
    public GameObject ReStartZone;
    public GameObject DadReStartZone;
    public GameObject KissZone;
    Animator DadAnim;

    Animator NpcDad;
    public GameObject KissParticle;

    private bool isRotating = false;
    private float rotationTimer = 0.0f;
    private float rotationDuration = 5.0f;
    public GameObject GetUpgradePs;

    void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        MomDownAnim = Mom.GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
        isJump = false;
        DadAnim = Dad.GetComponent<Animator>();
        mainAudio.Play();
    }

    void Start()
    {
        obstacle = GameObject.FindGameObjectWithTag("Obstacle").GetComponent<Obstacle_Cave>();
        DiePs.gameObject.SetActive(false);
        FirstCam.Priority =999;
    }

    void Update()
    {
        time += Time.deltaTime;

            if (!Dead &&!isReversed&&!isTalk)
            {
                Move();
                Jump();
                GetInput();

            if (isRotating)
            {
                HandleCameraRotation();
            }
        }

        //if (!Dead &&!isReversed&&!isTalk)
        //{
        //    Move();
        //    Jump();
        //    GetInput();
        //}
        else if(!Dead && isReversed && !isTalk)
        {
            ReversalMove();
            Jump();
            GetInput();
            //if (time > 5f)  // 3초동안만 좌우반전
            //{
            //    isReversed = false;
            //}
        }

        if (isTalk)
        {
            anim.SetBool("isRun", false);
        }
        
        if (isMomContact && Mom.gameObject.transform.position.x <= KissZone.transform.position.x && !isAnimChk)
        {
            
            //isMomContact = false;
            isAnimChk = true;
            Dad.SetActive(true);
            KissMovement();

        }
        
    }
    void KissMovement()
    {
        KissParticle.SetActive(true);
        MomDownAnim.SetTrigger("Kiss");
        DadAnim.SetTrigger("Kiss");
        
    }
    void GetInput()
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
        wDown = Input.GetButton("Walk");
        iDown = Input.GetButtonDown("Interaction");
        Dash = Input.GetButton("Dash");
    }

    void Move()
    {
        //potion.reversalPotion = false;

        moveVec = new Vector3(hAxis, 0, vAxis).normalized;
        
        transform.position += moveVec * speed * (wDown ? 0.3f : 1f) * Time.deltaTime;
        transform.position += moveVec * speed * (Dash ? 2.5f : 1f) * Time.deltaTime;
        runAudio.Play();
        transform.LookAt(transform.position + moveVec); // 회전

        anim.SetBool("isRun", moveVec != Vector3.zero);
        anim.SetBool("isWalk", wDown);
        anim.SetBool("isDash", Dash);

        ShowMoveParticle();
        PoisonParticle.gameObject.SetActive(false);
        
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && !isJump && !Dead)
        {
            jumpAudio.Play();
            isJump = true;
            isMove = true;
            rigid.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        }
    }

    void ReversalMove() // 방향키 좌우반전
    {
        //potion.reversalPotion = true;

        rhAxis = Input.GetAxisRaw("ReversalHorizontal");
        rvAxis = Input.GetAxisRaw("ReversalVertical");

        moveVec = new Vector3(rhAxis, 0, rvAxis).normalized;
        transform.position += moveVec * speed * (wDown ? 0.3f : 1f) * Time.deltaTime;

        anim.SetBool("isRun", moveVec != Vector3.zero);
        anim.SetBool("isWalk", wDown);

        transform.LookAt(transform.position + moveVec); // 회전

        ShowMoveParticle();
        PoisonParticle.gameObject.SetActive(true);
        runAudio.Play();
    }

    void OnCollisionEnter(Collision collision)
    {
        //if (collision.gameObject.tag == "Obstacle" && !Dead)
        //{
        //    DieMotion();
        //}

        if(collision.gameObject.name == "Basement_Var3")
        {
            ElevtorAudio.Play();
        }
    

        if (collision.gameObject.tag == "Ground")
        {
            isJump = false;
        }

        if (collision.gameObject.tag == "Obstacle" && !Dead)
        {
            
            DeadCount.count += 1;

            if (check_savepoint1 /*&& Dead*/)
            {
                DieMotion();
                Invoke("restart_stage1", 3f);
            }

            else if (check_savepoint2/* && Dead*/)
            {
                DieMotion();
                Invoke("restart_stage2", 3f);
            }

            else if (check_savepoint3 /*&& Dead*/)
            {
                DieMotion();
                Invoke("restart_stage3", 3f);
            }

            else if (check_savepoint4 /*&& Dead*/)
            {
                DieMotion();
                Invoke("restart_stage4", 3f);
            }
            else
            {
                /*if (check_savepoint0 *//*&& Dead*//*)
                {
                    DieMotion();
                    Invoke("restart_stage0", 3f);
                }*/

                DieMotion();
                Invoke("restart_stage0", 3f);
            }
        }
    }
    
    void OnParticleCollision(GameObject other)
    {
        if (other.tag == "Fire" && !Dead)
        {
            //Dead = true;
            other.gameObject.SetActive(false);
            //fire.Stop();
                DeadCount.count += 1;

            //if (check_savepoint0 /**//*&& Dead*//**/)
            //     {
            //    DieMotion();
            //    Invoke("restart_stage0", 3f);
            //}

            if (check_savepoint1 /*&& Dead*/)
                {
                    DieMotion();
                    Invoke("restart_stage1", 3f);
                }

                else if (check_savepoint2 /*&& Dead*/)
                {
                    DieMotion();
                    Invoke("restart_stage2", 3f);
                }

                else if (check_savepoint3 /*&& Dead*/)
                {
                    DieMotion();
                    Invoke("restart_stage3", 3f);
                }

                else if (check_savepoint4 /*&& Dead*/)
                {
                    DieMotion();
                    Invoke("restart_stage4", 3f);
                }
                else if (check_savepoint0)
                {
                    DieMotion();
                    Invoke("restart_stage0", 3f);
                }
        }
    }

    private void HandleCameraRotation()
    {
        rotationTimer += Time.deltaTime;
        float rotationAngle = Mathf.Lerp(0f, 720f, rotationTimer / rotationDuration);
        GetUpgradePs.SetActive(true);
        if (rotationTimer >= rotationDuration)
        {
            rotationTimer = 0.0f;
            isRotating = false;

            GetUpgradePs.SetActive(false);
        }
    }

    public void StartRotation()
    {
        isRotating = true;
    }

    void HideGetupgrade_textAfterDelay()
    {
        GetUpgradeBox_text.SetActive(false);
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "UpgradeBox")
        {
            GetUpgradeBox_text.SetActive(true);
            other.gameObject.SetActive(false);
            Invoke("HideGetupgrade_textAfterDelay", 5f);
            StartRotation();
        }

        if (other.gameObject.tag == "SenseTest")
        {
            Debug.Log("플레이어 들어왓삼");
            isSenseTest = true;
            //firetest.gameObject.SetActive(true);
        }

        if (other.tag == "DropBox")
        {
            fallingAudio.Play();
            isfallingObstacle = true;

        }
        
        if (other.gameObject.tag == "Door" && !isMoveUp)
        {
            other.gameObject.transform.position = new Vector3(other.gameObject.transform.position.x, other.gameObject.transform.position.y + 3f, other.gameObject.transform.position.z);
        }

        //if (other.gameObject.tag == "PushButton" && !isMoveUp)
        //{
        //    other.gameObject.transform.position = new Vector3(other.gameObject.transform.position.x, other.gameObject.transform.position.y + 3f, other.gameObject.transform.position.z);
        //}

        if (other.gameObject.tag == "NPC1" && !Talk_NPC1)
        {
            image1.SetActive(true);
           
            FDadTalkCam.Priority = 10;
            mainCam.Priority = 1;
            TalkAudio.Play();
            image2.SetActive(false);
            image3.SetActive(false);
            image4.SetActive(false);
            image5.SetActive(false);

            Talk_NPC1 = true;
        }

        if (other.gameObject.tag == "NPC2" && !Talk_NPC2)
        {
            image2.SetActive(true);
            TalkAudio.Play();
            TalkToDaddy2Cam.Priority = 10;
            mainCam.Priority = 1;
            //image1.SetActive(false);
            //image3.SetActive(false);
            //image4.SetActive(false);
            image5.SetActive(false);
            Talk_NPC2= true;
        }

        if (other.gameObject.tag == "NPC3" && !Talk_NPC3)
        {
            image3.SetActive(true);

            NPC3Cam.Priority = 10;
            mainCam.Priority = 1;
            TalkAudio.Play();
            //image1.SetActive(false);
            image2.SetActive(false);
            image4.SetActive(false);
            image5.SetActive(false);
            
            Talk_NPC3 = true;
        }

        if (other.gameObject.tag == "NPC4" && !Talk_NPC4)
        {
            image4.SetActive(true);
            NPC4Cam.Priority = 10;
            mainCam.Priority = 1;
            TalkAudio.Play();
            //image1.SetActive(false);
            image2.SetActive(false);
            //image3.SetActive(false);
            image5.SetActive(false);

            Talk_NPC4= true;
        }

        if (other.gameObject.tag == "NPC5" && !Talk_NPC5)
        {
            image5.SetActive(true);
            MomCam.Priority = 10;
            mainCam.Priority = 1;
            TalkAudio.Play();
            //image1.SetActive(false);
            //image2.SetActive(false);
            //image3.SetActive(false);
            //image4.SetActive(false);

            Talk_NPC5 = true;
        }

        if (other.gameObject.tag == "TogetherSense")
        {
            TogetherCam.Priority = 10;
            mainCam.Priority = 0;
        }

        if(other.gameObject.tag == "TogetherSenseOut" && !isChk)
        {
            TogetherCam.Priority = 0;
            mainCam.Priority = 10;
            isChk = true;
            Invoke("DeadCheck", 3f);
        }

        if(other.gameObject.tag == "SenseOut")
        {
            mainCam2.Priority = 0;
            mainCam.Priority = 10;
        }

        if (other.gameObject.name == "FinalStart"&& !isFinal)
        {
            mainCam.Priority = 0;
            FinalCam.Priority = 100;
            isFinal = true;
            FollowMom.SetActive(true);
            Mom.SetActive(false);

            KissParticle.SetActive(false);
        }

        if (other.gameObject.name == "StartMainCam")
        {
            mainCam.Priority = 10;
            FirstCam.Priority = 0;
        }

        // SavePoint

        if (other.gameObject.name == "SavePoint0")
        {
            check_savepoint0 = true;
            check_savepoint1 = false;
            check_savepoint2 = false;
            check_savepoint3 = false;
            check_savepoint4 = false;
            //StartCoroutine("GetSavePointImage");
            //Invoke("Destroy_SavePointObj1", 1.5f);
            //Invoke("Destroy_SavePointImage", 2f);
        }

        if (other.gameObject.tag == "SavePoint1")
        {
            check_savepoint1 = true;
            check_savepoint0 = false;
            check_savepoint2 = false;
            check_savepoint3 = false;
            check_savepoint4 = false;
            savePointAudio.Play();
            StartCoroutine("GetSavePointImage");
            Invoke("Destroy_SavePointObj1", 1.5f);
            Invoke("Destroy_SavePointImage", 2f);
        }

        if (other.gameObject.tag == "SavePoint2")
        {
            check_savepoint2 = true;
            check_savepoint0 = false;
            check_savepoint1 = false;
            check_savepoint3 = false;
            check_savepoint4 = false;
            savePointAudio.Play();
            StartCoroutine("GetSavePointImage");
            Invoke("Destroy_SavePointObj2", 1.5f);
            Invoke("Destroy_SavePointImage", 2f);
        }

        if (other.gameObject.tag == "SavePoint3")
        {
            check_savepoint3 = true;
            check_savepoint0 = false;
            check_savepoint1 = false;
            check_savepoint2 = false;
            check_savepoint4 = false;
            savePointAudio.Play();
            StartCoroutine("GetSavePointImage");
            Invoke("Destroy_SavePointObj3", 1.5f);
            Invoke("Destroy_SavePointImage", 2f);
        }

        if (other.gameObject.tag == "SavePoint4")
        {
            check_savepoint4 = true;
            check_savepoint0 = false;
            check_savepoint1 = false;
            check_savepoint2 = false;
            check_savepoint3 = false;
            savePointAudio.Play();
            StartCoroutine("GetSavePointImage");
            Invoke("Destroy_SavePointObj4", 1.5f);
            Invoke("Destroy_SavePointImage", 2f);
        }

        if(other.gameObject.name == "FinalPoint")
        {
            SceneManager.LoadScene("EndingScene");
        }
        //if (other.gameObject.CompareTag("Poison") &&!isReversal)
        //{
        //    Debug.Log("반대로..");
        //    isReversal = true;
        //    other.gameObject.SetActive(false);
        //    // PoisonSound.Play();
          
        //}

        if(other.CompareTag("Poison"))
        {
            StartCoroutine(ReversePlayerMovement());
            debuffAudio.Play();
        }

        if (other.gameObject.CompareTag("Item") &&!isFollow)
        {
            StopPleaseUI.SetActive(true);
            Invoke("StopPleaseUIEnd", 3f);
        }
    }

    private IEnumerator ReversePlayerMovement() // 플레이어 좌우반전
    {
        isReversed = true;
        //ReversalMove();
        yield return new WaitForSeconds(reverseDuration);
        isReversed = false;
    }

    void StopPleaseUIEnd()
    {
        StopPleaseUI.SetActive(false);

        isFollow = true;
       

    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "NPC1")
        {
            TalkAudio.Pause();
            FDadTalkCam.Priority = 1;
            mainCam.Priority = 10;
        }

        if (other.gameObject.tag == "NPC2" && !isTalk)
        {
            TalkToDaddy2Cam.Priority = 1;
            mainCam.Priority = 10;
            TalkAudio.Pause();
            GameObject Daddy2 = GameObject.Find("Daddy");
            NpcDad = Daddy2.GetComponent<Animator>();
            NpcDad.SetTrigger("DadDown");
            StartCoroutine(NpcDadDestroy(Daddy2));
           
        }

        if (other.gameObject.tag == "NPC3")
        {
            NPC3Cam.Priority = 1;
            mainCam.Priority = 10;
            TalkAudio.Pause();
        }

        if (other.gameObject.tag == "NPC4")
        {
            NPC4Cam.Priority = 1;
            mainCam.Priority = 10;
            TalkAudio.Pause();
        }

        if (other.gameObject.tag == "NPC5" && !isMomContact &&!isTalk)
        {

            //GameObject Mommy = GameObject.Find("Mom");
            MomDownAnim.SetTrigger("Down");
            TalkAudio.Pause();
            isMomContact = true;
            MomCam.Priority = 1;
            mainCam2.Priority = 10;
            //Mom.SetActive(false); 
        }
    }
    IEnumerator NpcDadDestroy(GameObject obj)
    {
        yield return new WaitForSeconds(1.7f);
        obj.SetActive(false);
    }
    void DeadCheck()
    {
        if (Dad.activeSelf == true)
        {
            DadDieCam.Priority = 10;
            mainCam.Priority = 0;
            DadDieUI.SetActive(true);
            BombSound.Play();
            dieAudio.Play();
            BombParticle.SetActive(true);
            
            Invoke("ReStartLastZone", 3f);
        }
    }
    void ReStartLastZone()
    {
        DadDieUI.SetActive(false);
        DadDieCam.Priority = 0;
        mainCam.Priority = 10;
        isChk = false;
        this.gameObject.transform.position = ReStartZone.transform.position;
        Dad.gameObject.transform.position = DadReStartZone.transform.position;
    }
    void ShowMoveParticle()
    {
        if (moveVec != Vector3.zero)
        {
            MoveParticle.gameObject.SetActive(true);
        }
        else if (moveVec == Vector3.zero)
        {
            MoveParticle.gameObject.SetActive(false);
        }
    }

    IEnumerator GetSavePointImage()
    {
        SavePointImage.gameObject.SetActive(true);
        savePointAudio.Play();
        yield break;
    }

    void Destroy_SavePointImage()
    {
        SavePointImage.gameObject.SetActive(false);
    }

    //------------Destroy_SavePointObj-----------------------------------------
    void Destroy_SavePointObj1()
    {
        SavePoint1Obj.gameObject.SetActive(false);
    }

    void Destroy_SavePointObj2()
    {
        SavePoint2Obj.gameObject.SetActive(false);
    }

    void Destroy_SavePointObj3()
    {
        SavePoint3Obj.gameObject.SetActive(false);
    }

    void Destroy_SavePointObj4()
    {
        SavePoint4Obj.gameObject.SetActive(false);
    }

    //------------restart_stage-----------------------------------------
    void restart_stage0()
    {
        
        FirstCam.Priority = 999;
        this.transform.position = SavePoint0Obj.transform.position;
        Dead = false;
        
    }
    void restart_stage1()
    {
        Dead = false;
        this.transform.position = SavePoint1Obj.transform.position;
    }

    void restart_stage2()
    {
        Dead = false;
        this.transform.position = SavePoint2Obj.transform.position;
    }

    void restart_stage3()
    {
        Dead = false;
        this.transform.position = SavePoint3Obj.transform.position;
    }

    void restart_stage4()
    {
        Dead = false;
        this.transform.position = SavePoint4Obj.transform.position;
    }

    void remove_dieUI()
    {
        DiePs.gameObject.SetActive(false);
        anim.SetBool("isDead",false);
       /* anim.SetBool("isDead", false);
        anim.SetBool("isWalk",false);
        anim.SetBool("isRun", false);*/
        //Dead = false;
    }

    void DieMotion()
    {
        if (!Dead )
        {
            dieAudio.Play();
            Dead = true;
            DiePs.gameObject.SetActive(true);
            anim.SetTrigger("doDead");
            //anim.SetBool("isDead", false);
            anim.SetBool("isDead", true);
            //anim.SetBool("isRun", false);
            Invoke("remove_dieUI", 3f);
        }
    }
}

