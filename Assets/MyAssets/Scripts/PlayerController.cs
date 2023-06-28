using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
//using UnityEngine.Windows;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform characterBody;
    [SerializeField] private Transform cameraArm;

    public GameObject player;

    Rigidbody rigid;

    Renderer render;
    SkinnedMeshRenderer skinrender;


    public ParticleSystem damagePs;
    public ParticleSystem jumpPs;
    public bool playDamagePs;
    public bool playJumpPs;

    public float speed = 5f;
    public float runSpeed = 8f;
    public float finalSpeed;

    public float hAxis;
    public float vAxis;

    bool run;
    bool isJump;
    public float jumpPower = 5f;
    public int jumpCount = 2;   // 점프횟수, 2를 3으로 바꾸면 3단 점프

    Vector3 moveDir;

    Animator anim;

    MoveObstacle obstacle;
    Boss boss;

    public GameObject EggPrefab;
    int eggCnt;

    public float playerHealth;
    public Slider healthbar;

    public GameObject playerShield;
    bool isPlayerShield;

    void Awake()
    {
        anim = characterBody.GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
        isJump = false;
        jumpCount = 0;
    }

    void Start()
    {
        obstacle = GameObject.FindGameObjectWithTag("Obstacle").GetComponent<MoveObstacle>();
        boss = GameObject.Find("Boss").GetComponent<Boss>();
        playerHealth = 100f;

        playDamagePs = true;
        playJumpPs = true;

        eggCnt = 1;
        //damagePs.Play();
    }

    void Update()
    {
        LookAround();
        Move();
        GetInput();
        Jump();
        UI();
        StartCoroutine(Fire1());
        StartCoroutine(Fire2());
        StartCoroutine(Shield());
    }
    void UI()
    {
        

       /* if (healthbar.value == 0)
        {
            // 게임 오버 창 띄우기
            player.transform.position = new Vector3(0, 0, 0);
            healthbar.value = 100f;
        }*/
    }
    void GetInput()
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
    }

    void Move()
    {
        finalSpeed = (run) ? runSpeed : speed;

        Vector2 moveInput = new Vector2(hAxis, vAxis);
        bool isMove = moveInput.magnitude != 0;

        if (isMove)
        {
            obstacle.isPlayerFollow = false; // MovePlatform
            Vector3 lookForward = new Vector3(cameraArm.forward.x, 0f, cameraArm.forward.z).normalized;
            Vector3 lookRight = new Vector3(cameraArm.right.x, 0f, cameraArm.right.z).normalized;
            moveDir = lookForward * moveInput.y + lookRight * moveInput.x;

            characterBody.forward = moveDir;
            transform.position += moveDir * finalSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            run = true;
        }
        else
        {
            run = false;
        }

        float percent = ((run) ? 1 : 0.5f) * moveInput.magnitude;
        anim.SetFloat("Blend", percent, 0.1f, Time.deltaTime);

        //Debug.DrawRay(cameraArm.position,new Vector3(cameraArm.forward.x,0f,cameraArm.forward.z).normalized,Color.red);
    }

    void Jump()
    {
        if (jumpCount > 0)
        {
            if (Input.GetButtonDown("Jump") && !isJump)
            {
                //isJump = true;
                rigid.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
                --jumpCount;

                jumpPs.Play();
            }
        }
    }

    IEnumerator Shield()
    {
        // yield return new WaitForSeconds(2f);

        if (Input.GetButtonDown("Shield") && !isPlayerShield) //f키
        {
            isPlayerShield= true;
            playerShield.SetActive(true);
            yield return new WaitForSeconds(2f);
            playerShield.SetActive(false);
            isPlayerShield = false;
        }
       
    }

    IEnumerator Fire1()
    {
        if (Input.GetButtonDown("Shoot1"))
        {
            if (eggCnt > 0)
            {
                Vector3 firePos = transform.position + anim.transform.forward + new Vector3(0f, 0.8f, 0f);
                var Egg = Instantiate(EggPrefab, firePos, Quaternion.identity).GetComponent<PlayerEgg>();
                Egg.Fire(anim.transform.forward);
                --eggCnt;

                if (eggCnt <= 0)
                {
                    yield return new WaitForSeconds(2f);
                    eggCnt = 1;
                }
            }
        }
    }

    IEnumerator Fire2()
    {
        if(Input.GetButtonDown("Shoot2"))
        {

            Vector3 firePos = transform.position + anim.transform.forward + new Vector3(0f, 0.8f, 0f);
            Vector3 firePos1 = transform.position + anim.transform.forward + new Vector3(1f, 0.8f, -1f);
            Vector3 firePos2 = transform.position + anim.transform.forward + new Vector3(-1f, 0.8f, 1f);
            var Egg = Instantiate(EggPrefab, firePos, Quaternion.identity).GetComponent<PlayerEgg>();
            var Egg1 = Instantiate(EggPrefab, firePos1, Quaternion.identity).GetComponent<PlayerEgg>();
            var Egg2 = Instantiate(EggPrefab, firePos2, Quaternion.identity).GetComponent<PlayerEgg>();
            Egg.Fire(anim.transform.forward);
            Egg1.Fire(anim.transform.forward);
            Egg2.Fire(anim.transform.forward);

            yield return new WaitForSeconds(2f);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor" /*|| collision.gameObject.tag == "Obstacle"*/)
        {
            jumpCount = 2;
            isJump = false;
        }

        if (collision.gameObject.tag == "Obstacle")
        {
            // 장애물 충돌 시 튕겨져나가는? 효과주기
            //ContactPoint cp = collision.GetContact(0);
            //moveDir = player.transform.position - cp.point;
            //rigid.AddForce((moveDir).normalized * 20f, ForceMode.Impulse);

            /* playerHealth -= 10f;
             healthbar.value -= 10f;*/
            jumpCount = 1;
            isJump = false;
            //damagePs.Play();
        }
      
    }
    void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject.tag == "Fire" || other.gameObject.tag == "Rock") && !isPlayerShield)
        {
            
            healthbar.value -= 5f;
            damagePs.Play();
        }
        
       
    }
    private void LookAround() // 카메라
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
