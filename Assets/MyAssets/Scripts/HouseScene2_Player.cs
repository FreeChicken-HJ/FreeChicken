using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;
using Cinemachine;

public class HouseScene2_Player : MonoBehaviour
{
    [SerializeField] private Transform characterBody;
    [SerializeField] public Transform cameraArm;

    public GameObject player;
    public bool isfallingObstacle;

    bool isContact;

    //public TextMeshProUGUI nearNPCText;
    public Image DieImage2;
    public TextMeshProUGUI superJump;

    public CinemachineVirtualCamera npc_cam;
    public CinemachineVirtualCamera mainCam;

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
    HouseSceneTalkManager talkeManager;

    public GameObject Dialogue;

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
        //nearNPCText.gameObject.SetActive(false);
        DieImage2.gameObject.SetActive(false);
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

        if (isContact)
        {
            if (Input.GetMouseButton(0))
            {
                //nearNPCText.gameObject.SetActive(false);
                npc_cam.gameObject.SetActive(true);
                Dialogue.gameObject.SetActive(true);
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
        anim.SetBool("Die", true);
    }

    void ReLoadScene()
    {
        Dead = false;
        SceneManager.LoadScene("HouseScene2");
        DieImage2.gameObject.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "DropBox")
        {
            isfallingObstacle = true;
        }

        if (other.gameObject.tag.Equals("NPC"))
        {
            //nearNPCText.gameObject.SetActive(true);
            isContact = true;
            npc_cam.Priority = 10;
            mainCam.Priority = 1;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals("NPC"))
        {
            //nearNPCText.gameObject.SetActive(false);
            npc_cam.Priority = 1;
            mainCam.Priority = 10;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "House2_Obstacle" && !Dead)
        {
            Dead = true;
            DieMotion();
            DieImage2.gameObject.SetActive(true);
            Invoke("ReLoadScene", 3.5f);
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
