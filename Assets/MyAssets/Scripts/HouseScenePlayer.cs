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
    [Header("Setting")]
    [SerializeField] private Transform characterBody;
    [SerializeField] public Transform cameraArm;
    public GameObject player;
    public ParticleSystem DiePs;
    public ParticleSystem JumpPs;
    Vector3 moveVec;
    Vector2 moveInput;
    Animator anim;
    Rigidbody rigid;
    public float speed;
    public float JumpPower;
    public float hAxis;
    public float vAxis;
    public float jumpPower = 5f;

    [Header("Bool")]
    bool wDown;
    bool isJump;
    bool Dead;
    public bool isfallingObstacle;
    public bool isSense;
    public bool isDoorOpen = false;
    public bool isReadyDoorOpen = false;
    public bool pushBell = false;
    private bool isOpeningDoor = false;
    private bool isRaisingDoor = false;
    private float doorOpenTimer = 0.0f;
    private float doorOpenDuration = 3.0f;
    private float doorRaiseSpeed = 0.8f;
    private bool shouldLookAround = false;

    public GameObject startDoor;
    public GameObject DieCanvas;
    public Image NextSceneImage;

    Obstacle_House obstacle_house;

    [Header("Dialogue")]
    public GameObject startCanvas;
    public bool isTalk;
    public bool TalkEnd;

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
    public CinemachineVirtualCamera openDoorCam;

    [Header("Audio")]
    public AudioSource mainAudio;
    public AudioSource runAudio;
    public AudioSource dieAudio;
    public AudioSource jumpAudio;
    public AudioSource savePointAudio;
    public AudioSource bellAudio;
    public AudioSource TalkAudio;
    public AudioSource ClickButtonAudio;
    public AudioSource OpenDoorAudio;

    [Header("UI")]
    public GameObject NearDoor_text;
    public GameObject OpenDoor_text;
    public GameObject PushBell_text;
    public GameObject GetUpgradeBox_text;
    public GameObject menuSet;

    // 새로운 능력얻는 효과
    private bool isRotating = false;
    private Quaternion originalCameraRotation;
    private float rotationTimer = 0.0f;
    private float rotationDuration = 5.0f;
    public GameObject GetUpgradePs;

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
        StartCam.Priority = 10;
        //DieImage.gameObject.SetActive(false);
        //SavePointImage.SetActive(false);
    }

    void Update()
    {
        Cursor.visible = false;
        if (!Dead)
        {
            if (!isTalk)
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

                    if (shouldLookAround)
                    {
                        LookAround();
                    }
                }
            }
        }

        if (shouldLookAround)
        {
            StartCoroutine(HideGetupgrade_textAfterDelay(5f));
        }


        //if (Input.GetButtonDown("Cancel"))
        //{
        //    menuSet.SetActive(true);
        //    mainAudio.Pause();
        //    runAudio.Pause();
        //    Time.timeScale = 0f;
        //    isTalk = true;
        //}

        if (!isOpeningDoor && Input.GetButtonDown("E") && isReadyDoorOpen)
        {
            Debug.Log("벨 처음 눌렀음");
            isOpeningDoor= true;
            pushBell = true;
            bellAudio.Play();
            PushBell_text.SetActive(false);
            StartCam.Priority = 0;
            openDoorCam.Priority = 10;
            isTalk = true;
            anim.SetBool("Walk", false);
            anim.SetBool("Run", false);
        }

        if (isOpeningDoor)
        {
            doorOpenTimer += Time.deltaTime;

            if (doorOpenTimer >= doorOpenDuration)
            {
                isOpeningDoor = true;
                isRaisingDoor = true;
                doorOpenTimer = 0.0f;
            }
        }

        if (isRaisingDoor)
        {
            Debug.Log("문 올라감");
            startDoor.transform.Translate(Vector3.up * doorRaiseSpeed * Time.deltaTime);
            if (startDoor.transform.position.y >= 3f)
            {
                startDoor.SetActive(false);
                isRaisingDoor = false;
                isTalk = false;
                mainCam.Priority = 10;
                openDoorCam.Priority = 0;
                pushBell = false;
                PushBell_text.SetActive(false);
                isReadyDoorOpen = false;
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
        DieCanvas.gameObject.SetActive(true);
        DiePs.gameObject.SetActive(true);
        dieAudio.Play();
        anim.SetTrigger("isDead");
        Invoke("remove_dieUI", 3f);
    }

    void NextScene()
    {
        SceneManager.LoadScene("HouseScene2");
    }

    //public void ContinueGame()
    //{
    //    menuSet.SetActive(false);
    //    mainAudio.UnPause();
    //    runAudio.UnPause();
    //    isTalk = false;
    //    Time.timeScale = 1;
    //}

    //public void GameExit()
    //{
    //    Application.Quit();
    //}

    //public void ReplayGame()
    //{
    //    Time.timeScale = 1f;
    //    SceneManager.LoadScene("HouseScene2");
    //}

    //public void ClickButtonSound()
    //{
    //    ClickButtonAudio.Play();
    //}

    private void HandleCameraRotation()
    {
        rotationTimer += Time.deltaTime;

        // 회전 각도 계산 (0에서 720도까지)
        float rotationAngle = Mathf.Lerp(0f, 720f, rotationTimer / rotationDuration); // 0부터 720도까지 두 바퀴 회전

        // 회전
        cameraArm.RotateAround(transform.position, Vector3.up, rotationAngle * Time.deltaTime);

        GetUpgradePs.SetActive(true);

        if (rotationTimer >= rotationDuration)
        {
            rotationTimer = 0.0f;
            isRotating = false;

            // 회전이 완료된 후에 원래 상태로 돌아가는 처리 추가
            cameraArm.rotation = originalCameraRotation;
            GetUpgradePs.SetActive(false);
        }
    }

    public void StartRotation()
    {
        isRotating = true;
        originalCameraRotation = cameraArm.rotation;  // 카메라 회전을 시작하기 전에 원래의 회전값 저장
    }

    IEnumerator HideGetupgrade_textAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // 지정된 시간만큼 대기

        GetUpgradeBox_text.SetActive(false); // GetUpgradeBox_text를 비활성화
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
            anim.SetBool("Walk", false);
            anim.SetBool("Run", false);
            TalkAudio.Play();
        }

        if (other.CompareTag("PushButton") && !pushBell)
        {
            Debug.Log("벨 처음 누를 준비중");
            PushBell_text.SetActive(true);
            isReadyDoorOpen = true;
        }

        if(other.CompareTag("PushButton") && isOpeningDoor)
        {
            Debug.Log("이미 벨눌렀음");
            PushBell_text.SetActive(false);
            bellAudio.Pause();
            OpenDoorAudio.Pause();
            openDoorCam.Priority = 0;
        }

        if(other.gameObject.name == "UpgradeBox")
        {
            shouldLookAround= true;
            GetUpgradeBox_text.SetActive(true);
            other.gameObject.SetActive(false);
            StartRotation();
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
            TalkAudio.Pause();
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
        DieCanvas.gameObject.SetActive(false);
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
