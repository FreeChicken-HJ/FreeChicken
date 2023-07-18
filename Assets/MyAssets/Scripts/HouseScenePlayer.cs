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

    bool isContact;

    public Image DieImage;
    public TextMeshProUGUI superJump;
    public Image NextSceneImage;
    public Image SavePointImage;
    
    public CinemachineVirtualCamera mainCam;
    public CinemachineVirtualCamera dieCam;

    Vector3 moveVec;
    Vector2 moveInput;

    bool wDown;
    bool isJump;
    Rigidbody rigid;

    public float speed;
    public float JumpPower;

    bool Dead;
    //bool eatPortion;

    public float hAxis;
    public float vAxis;
    public float jumpPower = 5f;

    public ParticleSystem DiePs;
    public ParticleSystem JumpPs;

    Animator anim;

    Obstacle_House obstacle_house;

    //SavePoint
    public GameObject SavePoint1Obj;
    public GameObject SavePoint2Obj;
    public GameObject SavePoint3Obj;

    public bool check_savepoint1;
    public bool check_savepoint2;
    public bool check_savepoint3;
    
    void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        rigid = GetComponent<Rigidbody>();
        isJump = false;
    }

    void Start()
    {
        obstacle_house = GameObject.FindGameObjectWithTag("Obstacle").GetComponent<Obstacle_House>();
        DiePs.gameObject.SetActive(false);
        DieImage.gameObject.SetActive(false);
    }

    void Update()
    {
        if (!Dead)
        {
            Move();
            GetInput();
            Jump();
            LookAround();
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
        }
        anim.SetBool("Run", moveInput != Vector2.zero);
        anim.SetBool("Walk", wDown);
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && !isJump && !Dead)
        {
            isJump = true;
            rigid.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            JumpPs.Play();
        }
    }

    void DieMotion()
    {
        Debug.Log("플레이어 사망");
        //Dead = true;
        DiePs.gameObject.SetActive(true);
        DieImage.gameObject.SetActive(true);
        anim.SetBool("Die", true);
        dieCam.Priority = 10;
        mainCam.Priority = 1;
        Invoke("remove_dieUI", 2f);
    }

    void NextScene()
    {
        SceneManager.LoadScene("HouseScene2");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "DropBox")
        {
            isfallingObstacle = true;
        }

        if (other.gameObject.tag == "SavePoint1")
        {
            check_savepoint1 = true;
            check_savepoint2 = false;
            check_savepoint3 = false;
            SavePointImage.gameObject.SetActive(true);
        }

        if (other.gameObject.tag == "SavePoint2")
        {
            check_savepoint2 = true;
            check_savepoint1 = false;
            check_savepoint3 = false;

        }

        if (other.gameObject.tag == "SavePoint3")
        {
            check_savepoint3 = true;
            check_savepoint1 = false;
            check_savepoint2 = false;
        }

        if (other.gameObject.name == "NextScenePoint")
        {
            NextSceneImage.gameObject.SetActive(true);
            Invoke("NextScene", 3.5f);
        }
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

    void remove_dieUI()
    {
        DiePs.gameObject.SetActive(false);
        DieImage.gameObject.SetActive(false);
        anim.SetBool("Die", false);
        dieCam.Priority = 1;
        mainCam.Priority = 10;
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Obstacle" && !Dead)
        {
            Dead = true;

            if(check_savepoint1 && Dead)
            {
                DieMotion();
                Invoke("restart_stage1", 3f);
            }

            if(check_savepoint2&& Dead)
            {
                DieMotion();
                Invoke("restart_stage2", 3f);
            }

            if (check_savepoint3 && Dead)
            {
                DieMotion();
                Invoke("restart_stage3", 3f);
            }
        }

        if (collision.gameObject.tag == "Ground")
        {
            isJump = false;
        }

        if (collision.gameObject.tag == "Slow")
        {
            speed /= 2f;
            Invoke("DoNotSlow", 2f);
        }

        if (collision.gameObject.tag == "SuperJump")
        {
            superJump.gameObject.SetActive(true);
            jumpPower *= 1.2f;
            Invoke("DonotSuperJump", 2f);
        }

        if (collision.gameObject.tag == "AI_Person_House")
        {
            DieMotion();
        }
    }

    void DonotSuperJump()
    {
        superJump.gameObject.SetActive(false);
        jumpPower /= 1.2f;
    }

    void DoNotSlow()
    {
        speed *= 2f;
    }

    public void LookAround() // 카메라
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
