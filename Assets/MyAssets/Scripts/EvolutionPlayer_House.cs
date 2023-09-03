using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;
using Cinemachine;

public class EvloutionPlayer : MonoBehaviour
{
    [SerializeField] private Transform characterBody;
    [SerializeField] public Transform cameraArm;
    public GameObject player;

    public Image DieImage2;

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

    public ParticleSystem DiePs;
    public ParticleSystem JumpPs;

    Animator anim;

    public GameObject Pos;
    [Header("Camera")]
    public CinemachineVirtualCamera mainCam;
    public CinemachineVirtualCamera unicycleCam;

    [Header("Audio")]
    public AudioSource mainAudio;
    public AudioSource runAudio;
    public AudioSource dieAudio;
    public AudioSource jumpAudio;
    public AudioSource savePointAudio;

    [Header("Dialogue")]
    public GameObject ReadygoCity;
    public GameObject GoCity;
    public bool isTalk2;
    public bool TalkEnd2;

    public GameObject LoadingUI;
    // 진화효과
    private bool isRotating = false;
    private Quaternion originalCameraRotation;
    private float rotationTimer = 0.0f;
    private float rotationDuration = 2.0f;
    public GameObject EvoluPs;



    void Awake()
    {
        //mainAudio.Play();
        anim = GetComponentInChildren<Animator>();
        rigid = GetComponent<Rigidbody>();
        isJump = false;
    }

    void Start()
    {
        DiePs.gameObject.SetActive(false);
        DieImage2.gameObject.SetActive(false);
    }

    void Update()
    {
        if (!Dead)
        {
            if (!isTalk2)
            {
                if (isRotating)
                {
                    HandleCameraRotation();
                }
                else
                {
                    Move();
                    GetInput();
                    Jump();
                    LookAround();
                }
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
            rigid.AddForce(Vector3.up * JumpPower, ForceMode.Impulse);
            JumpPs.Play();
        }
    }

    void DieMotion()
    {
        Dead = true;
        DiePs.gameObject.SetActive(true);
        anim.SetBool("isDead", true);
        dieAudio.Play();
    }

    void ReLoadScene()
    {
        Dead = false;
        anim.SetBool("isDead",false);
        DiePs.gameObject.SetActive(false);
        this.gameObject.transform.position = Pos.gameObject.transform.position;
        //SceneManager.LoadScene("HouseScene2");
        DieImage2.gameObject.SetActive(false);
    }

    void NextCityScene()
    {
        //Cursor.visible = true;
        mainAudio.Stop();
        LoadingUI.SetActive(true);
        GameSave.isCity = true;
        PlayerPrefs.SetInt("GoCity", GameSave.isCity ? 1 : 0);
        Invoke("Last", 2f);
    }
    void Last()
    {
        SceneManager.LoadScene("Enter2DScene");
    }
    void OnTriggerEnter(Collider other)
    {
        //if (other.gameObject.name == "EvolutionSense2")
        //{
        //    ReadygoCity.SetActive(true);
        //}

        if (other.CompareTag("evolu")) 
        {
            StartRotation();
        }

        if (other.gameObject.name == "GoCitySense")
        {
            GoCity.SetActive(true);
            Invoke("NextCityScene", 3f);
        }
    }

    private void HandleCameraRotation()
    {
        rotationTimer += Time.deltaTime;

        // 회전 각도 계산 (0에서 720도까지)
        float rotationAngle = Mathf.Lerp(0f, 720f, rotationTimer / rotationDuration); // 0부터 720도까지 두 바퀴 회전

        // 회전
        cameraArm.RotateAround(transform.position, Vector3.up, rotationAngle * Time.deltaTime);

        EvoluPs.SetActive(true);

        if (rotationTimer >= rotationDuration)
        {
            rotationTimer = 0.0f;
            isRotating = false;

            // 회전이 완료된 후에 원래 상태로 돌아가는 처리 추가
            cameraArm.rotation = originalCameraRotation;
            EvoluPs.SetActive(false);
        }
    }

    public void StartRotation()
    {
        isRotating = true;
        originalCameraRotation = cameraArm.rotation;  // 카메라 회전을 시작하기 전에 원래의 회전값 저장
    }


    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "EvolutionSense2")
        {
            ReadygoCity.SetActive(false);
        }

        if (other.CompareTag("evolu"))
        {
            other.gameObject.SetActive(false);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("House2_Obstacle") && !Dead)
        {
            Dead = true;
            DieMotion();
            DieImage2.gameObject.SetActive(true);
            Invoke("ReLoadScene", 3.5f);
        }

        if (collision.gameObject.CompareTag("Ground"))
        {
            isJump = false;
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
}

