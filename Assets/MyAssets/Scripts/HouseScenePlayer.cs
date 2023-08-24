using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;
using Cinemachine;
public class HouseScenePlayer : MonoBehaviour
{
    [SerializeField] private Transform characterBody;
    [SerializeField] public Transform cameraArm;

    public GameObject player;
    public bool isfallingObstacle;
    public bool isSense;
    public bool isDoorOpen = false;
    public bool isReadyDoorOpen = false;
    public bool pushBell = false;

    public GameObject startDoor;
    public GameObject DieImage;
    public Image NextSceneImage;

    Vector3 moveVec;
    Vector2 moveInput;

    bool wDown;
    bool isJump;
    Rigidbody rigid;

    public float speed;
    public float JumpPower;

    bool Dead;

    public float hAxis;
    public float vAxis;
    public float jumpPower = 5f;

    public ParticleSystem DiePs;
    public ParticleSystem JumpPs;

    Animator anim;

    Obstacle_House obstacle_house;

    //Dialogue
    [Header("Dialogue")]
    public GameObject startCanvas;
    public bool isTalk;
    public bool TalkEnd;

    //SavePoint
    [Header("SavePoint")]
    public GameObject SavePointImage;
    public GameObject SavePoint1Obj;
    public GameObject SavePoint2Obj;
    public GameObject SavePoint3Obj;
    public bool check_savepoint1;
    public bool check_savepoint2;
    public bool check_savepoint3;

    [Header("Camera")]
    public CinemachineVirtualCamera mainCam;
    public CinemachineVirtualCamera StartCam;

    [Header("Audio")]
    public AudioSource mainAudio;
    public AudioSource runAudio;
    public AudioSource dieAudio;
    public AudioSource jumpAudio;
    public AudioSource savePointAudio;
    public AudioSource bellAudio;

    [Header("UI")]
    public GameObject NearDoor_text;
    public GameObject OpenDoor_text;
    public GameObject PushBell_text;

    void Awake()
    {
        mainAudio.Play();
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
        isJump = false;
    }

    void Start()
    {
        obstacle_house = GameObject.FindGameObjectWithTag("Obstacle").GetComponent<Obstacle_House>();
        DiePs.gameObject.SetActive(false);
        //StartCam.Priority = 10;
        //DieImage.gameObject.SetActive(false);
        //SavePointImage.SetActive(false);
    }

    void Update()
    {
        if (!Dead)
        {
            if(!isTalk)
            {
                Move();
                GetInput();
                Jump();
                LookAround();
            }
        }

        if (Input.GetButtonDown("E") && pushBell)
        {
            bellAudio.Play();
            isReadyDoorOpen = true;
        }

        if (Input.GetButtonDown("E") && isReadyDoorOpen)
        {
            startDoor.SetActive(false);
            isDoorOpen = true;

            if (isDoorOpen)
            {
                NearDoor_text.SetActive(false);
                OpenDoor_text.SetActive(false);
            }
        }
    }

    void GetInput()
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
        wDown = Input.GetButton("Walk");
    }

    void Move()
    {
        moveInput = new Vector2(hAxis, vAxis);
        bool isMove = moveInput.magnitude != 0;

        if (isMove)
        {
            Vector3 lookForward = new Vector3(cameraArm.forward.x, 0f, cameraArm.forward.z).normalized;
            Vector3 lookRight = new Vector3(cameraArm.right.x, 0f, cameraArm.right.z).normalized;
            moveVec = lookForward * moveInput.y + lookRight * moveInput.x;

            characterBody.forward = moveVec;
            transform.position += moveVec * speed * (wDown ? 0.3f : 1f) * Time.deltaTime;
            runAudio.Play();
        }

        anim.SetBool("Run", moveInput != Vector2.zero);
        anim.SetBool("Walk", wDown);

        
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && !isJump && !Dead)
        {
            jumpAudio.Play();
            isJump = true;
            rigid.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            JumpPs.Play(); 
        }
    }

    void DieMotion()
    {
        //Dead = true;
        DieImage.gameObject.SetActive(true);
        DiePs.Play();
        dieAudio.Play();
        anim.SetTrigger("isDead");
        Invoke("remove_dieUI", 2f);
    }

    void NextScene()
    {
        SceneManager.LoadScene("HouseScene2");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DropBox"))
        {
            isfallingObstacle = true;
        }

        if(other.CompareTag("Sense") && !isTalk && !TalkEnd)
        {
            startCanvas.SetActive(true);
        }

        if (other.CompareTag("PushButton") && !pushBell)
        {
            //bellAudio.Play();
            PushBell_text.SetActive(true);
            isReadyDoorOpen = true;
            isDoorOpen = false;
            pushBell = true;
        }

        if(other.gameObject.CompareTag("Door") && !isDoorOpen && !isReadyDoorOpen)
        {
            NearDoor_text.SetActive(true);
        }

        if(other.gameObject.CompareTag("Door") && isReadyDoorOpen)
        {
            OpenDoor_text.SetActive(true);
        }

        if(other.gameObject.name == "InHouseSense")
        {
            mainCam.Priority = 10;
            StartCam.Priority = 1;
        }

        if (other.gameObject.CompareTag("SavePoint1"))
        {
            check_savepoint1 = true;
            check_savepoint2 = false;
            check_savepoint3 = false;
            SavePointImage.gameObject.SetActive(true);
            savePointAudio.Play();
            Invoke("Destroy_SavePointObj1", 1.5f);
            Invoke("Destroy_SavePointImage", 2f);
        }

        if (other.gameObject.CompareTag("SavePoint2")) 
        {
            check_savepoint2 = true;
            check_savepoint1 = false;
            check_savepoint3 = false;
            SavePointImage.gameObject.SetActive(true);
            savePointAudio.Play();
            Invoke("Destroy_SavePointImage", 2f);
            Invoke("Destroy_SavePointObj2", 1.5f);
        }

        if (other.gameObject.CompareTag("SavePoint3"))
        {
            check_savepoint3 = true;
            check_savepoint1 = false;
            check_savepoint2 = false;
            SavePointImage.gameObject.SetActive(true);
            savePointAudio.Play();
            Invoke("Destroy_SavePointImage", 2f);
            Invoke("Destroy_SavePointObj3", 1.5f);
        }

        if (other.gameObject.name == "NextScenePoint")
        {
            NextSceneImage.gameObject.SetActive(true);
            Invoke("NextScene", 3.5f);
        }

        if (other.gameObject.CompareTag("Obstacle") && !Dead)
        {
            Dead = true;
            DeadCount.count += 1;

            if (check_savepoint1 /*&& Dead*/)
            {
                DieMotion();
                Invoke("restart_stage1", 3f);
            }

            if (check_savepoint2 /*&& Dead*/)
            {
                DieMotion();
                Invoke("restart_stage2", 3f);
            }

            if (check_savepoint3 /*&& Dead*/)
            {
                DieMotion();
                Invoke("restart_stage3", 3f);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Sense"))
        {
            TalkEnd = true;
        }

        if(other.gameObject.CompareTag("Door"))
        {
            NearDoor_text.SetActive(false);
            OpenDoor_text.SetActive(false);
        }

        if (other.CompareTag("PushButton"))
        {
            PushBell_text.SetActive(false);

        }
    }

    IEnumerator GetSavePointImage()
    {
        SavePointImage.gameObject.SetActive(true);
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

    //------------restart_stage-----------------------------------------
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

    void remove_dieUI()
    {
        DieImage.gameObject.SetActive(false);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle") && !Dead)
        {
            Dead = true;
            DeadCount.count += 1;

            if (check_savepoint1 /*&& Dead*/)
            {
                DieMotion();
                Invoke("restart_stage1", 3f);
            }

            if (check_savepoint2 /*&& Dead*/)
            {
                DieMotion();
                Invoke("restart_stage2", 3f);
            }

            if (check_savepoint3 /*&& Dead*/)
            {
                DieMotion();
                Invoke("restart_stage3", 3f);
            }
        }

        if (collision.gameObject.CompareTag("Ground"))
        {
            isJump = false;
        }
    }

    void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Fire") && !Dead)
        {
            Dead = true;
            DeadCount.count += 1;

            if (check_savepoint1 /*&& Dead*/)
            {
                DieMotion();
                Invoke("restart_stage1", 3f);
            }

            if (check_savepoint2 /*&& Dead*/)
            {
                DieMotion();
                Invoke("restart_stage2", 3f);
            }

            if (check_savepoint3 /*&& Dead*/)
            {
                DieMotion();
                Invoke("restart_stage3", 3f);
            }
        }
    }

    public void LookAround() // Ä«¸Þ¶ó
    {
        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        Vector3 camAngle = cameraArm.rotation.eulerAngles;
        float x = camAngle.x - mouseDelta.y;

        if (x < 100f)
        {
            x = Mathf.Clamp(x, -1f, 70f);
        }
        else
        {
            x = Mathf.Clamp(x, 335f, 361f);
        }
        cameraArm.rotation = Quaternion.Euler(x, camAngle.y + mouseDelta.x, camAngle.z);
    }
}
