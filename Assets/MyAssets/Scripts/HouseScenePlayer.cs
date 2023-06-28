using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

public class HouseScenePlayer : MonoBehaviour
{
    [SerializeField] private Transform characterBody;
    [SerializeField] public Transform cameraArm;

    public GameObject player;
    public bool isfallingObstacle;
    public bool isSense;

    public bool hide;
    bool isContact;

    public TextMeshProUGUI nearNPCText;
    public Image DieImage;
    public TextMeshProUGUI superJump;

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

    //PlayDart
    public GameObject DartBullet;
    
    void Awake()
    {
        anim = GetComponentInChildren<Animator>();

        rigid = GetComponent<Rigidbody>();
        isJump = false;
    }

    void Start()
    {
        //obstacle_house = GameObject.FindGameObjectWithTag("Obstacle").GetComponent<Obstacle_House>();
        DiePs.gameObject.SetActive(false);
        nearNPCText.gameObject.SetActive(false);
        DieImage.gameObject.SetActive(false);
        Dialogue.gameObject.SetActive(false);
    }

    void Update()
    {
        if (!Dead)
        {
            Move();
            GetInput();
            Jump();
            LookAround();
            ShootDarts();
        }

        if(isContact)
        {
            if(Input.GetMouseButton(0))
            {
                nearNPCText.gameObject.SetActive(false);
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

        //moveVec = new Vector3(hAxis, 0, vAxis).normalized;

        //transform.position += moveVec * speed * (wDown ? 0.3f : 1f) * Time.deltaTime;

        //transform.LookAt(transform.position + moveVec); // 회전
        //float percent = ((wDown) ? 0.3f : 1f) * moveInput.magnitude;
        //anim.SetFloat("Blend", percent, 0.1f, Time.deltaTime);

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
        Dead = true;
        DiePs.gameObject.SetActive(true);
        anim.SetTrigger("isDead");
        DieImage.gameObject.SetActive(true);
        Invoke("ReLoadScene", 1.5f);
    }

    void ReLoadScene()
    {
        SceneManager.LoadScene("HouseScene");
        //SceneManager.LoadScene("HouseScene2");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "DropBox")
        {
            isfallingObstacle = true;
        }

        if (other.gameObject.tag.Equals("NPC"))
        {
            nearNPCText.gameObject.SetActive(true);
            isContact = true;

        }

        if (other.gameObject.tag == "Hide")
        {
            hide = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals("NPC"))
        {
            //nearNPCText.gameObject.SetActive(false);
        }

        if (other.gameObject.tag == "Hide")
        {
            hide = false;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            DieMotion();
        }

        if (collision.gameObject.tag == "Ground")
        {
            isJump = false;
        }

        if (collision.gameObject.tag == "Slow")
        {
            Debug.Log("바나나 밟았닭");
            //rigid.velocity = new Vector3(hAxis * speed * 3f, rigid.velocity.y, vAxis * speed * 3f);
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

    void OnParticleCollision(GameObject other)
    {
        if (other.tag == "Fire")
        {
            DieMotion();
            //Instantiate(chicken);
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

    void ShootDarts()
    {
        
        if (Input.GetButtonDown("Fire1"))
        {
            Vector3 firePos = transform.position + anim.transform.forward + new Vector3(-0.00400000019f, 0.114f, 0.0810000002f);
            var Egg = Instantiate(DartBullet, firePos, Quaternion.identity).GetComponent<PlayerEgg>();
            Egg.Fire(anim.transform.forward);
        }
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

    public void NotLookAround()
    {
        
    }
}
