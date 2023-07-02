using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using UnityEngine.UIElements;

public class CaveScenePlayer : MonoBehaviour
{
    //[SerializeField] private Transform characterBody;
    //[SerializeField] private Transform cameraArm;

    Vector3 moveVec;
    bool wDown;
    bool Dead;

    Rigidbody rigid;

    //������ �����϶� ��ƼŬȿ��
    public ParticleSystem MoveParticle;

    public ParticleSystem PoisonParticle;
    //bool isMove;

    public bool isSense;
    public bool isSenseTest;
    public bool isfallingObstacle;

    //public ParticleSystem jumpPs;
    //public bool playJumpPs;

    public ParticleSystem DiePs;

    public float speed;
    bool isJump;
    public float jumpPower = 5f;

    public float hAxis;
    public float vAxis;

    public float rhAxis;
    public float rvAxis;

    bool reversal;

    float time;

    //bool isJump;
    //public float jumpPower = 5f;
    //public int jumpCount = 2;   // ����Ƚ��, 2�� 3���� �ٲٸ� 3�� ����

    Animator anim;
    Obstacle_Cave obstacle;
    //GameManager_Cave manager;
    //FireTest firetest;
    CaveItem_DebuffPotion portion;
    //ObstacleTest ObstacleTestobstacleTest;

    public bool hasKey;
    public int keyCount;

    bool iDown;

    void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        rigid = GetComponent<Rigidbody>();
        isJump = false;
    }

    void Start()
    {
        obstacle = GameObject.FindGameObjectWithTag("Obstacle").GetComponent<Obstacle_Cave>();
        DiePs.gameObject.SetActive(false);
        portion = GameObject.FindGameObjectWithTag("Poison").GetComponent<CaveItem_DebuffPotion>();
        //firetest = GameObject.FindGameObjectWithTag("Fire").GetComponentInChildren<FireTest>();
        //ObstacleTestobstacleTest = GameObject.FindGameObjectWithTag("e").GetComponent<ObstacleTest>();
    }

    void Update()
    {

        time += Time.deltaTime;
        if (!Dead && !portion.reversalPotion)
        {
            Move();
            Jump();
            GetInput();
        }

        if(portion.reversalPotion)
        {
            ReversalMove();
            Jump();
            
            if (time > 10f)  // 3�ʵ��ȸ� �¿����
            {
                portion.reversalPotion = false;
            }
        }
    }

    void GetInput()
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
        wDown = Input.GetButton("Walk");
        iDown = Input.GetButtonDown("Interaction");
    }

    void Move()
    {
        //portion.reversalPortion = false;

        moveVec = new Vector3(hAxis, 0, vAxis).normalized;

        transform.position += moveVec * speed * (wDown ? 0.3f : 1f) * Time.deltaTime;

        transform.LookAt(transform.position + moveVec); // ȸ��

        anim.SetBool("isRun", moveVec != Vector3.zero);
        anim.SetBool("isWalk", wDown);

        ShowMoveParticle();
        EatPoisonParticle();
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && !isJump && !Dead)
        {
            isJump = true;
            rigid.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        }
    }

    void ReversalMove() // ����Ű �¿����
    {
        portion.reversalPotion = true;

        rhAxis = Input.GetAxisRaw("ReversalHorizontal");
        rvAxis = Input.GetAxisRaw("ReversalVertical");

        moveVec = new Vector3(rhAxis, 0, rvAxis).normalized;
        transform.position += moveVec * speed * (wDown ? 0.3f : 1f) * Time.deltaTime;

        anim.SetBool("isRun", moveVec != Vector3.zero);
        anim.SetBool("isWalk", wDown);

        transform.LookAt(transform.position + moveVec); // ȸ��

        ShowMoveParticle();
        EatPoisonParticle();
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
    }

    void OnParticleCollision(GameObject other)
    {
        if (other.tag == "Fire")
        {
            DieMotion();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "testtest")
        {
            Debug.Log("�÷��̾� �������� �� ������");
            isSense = true;
            // �÷��̾ ��ֹ����� Ʈ���Ŷ� ���˵Ǹ�, ��ֹ� �׶� �ٷ� �����̰Բ�
            //ObstacleTestobstacleTest.transform.position += new Vector3(0, 0, -1) * 1f * Time.deltaTime;
            //ObstacleTestobstacleTest.transform.Rotate(0, 0, -50 / 50);

        }

        if (other.gameObject.tag == "SenseTest")
        {
            Debug.Log("�÷��̾� ���ӻ�");
            isSenseTest = true;
            //firetest.gameObject.SetActive(true);
        }

        if (other.tag == "DropBox")
        {
            isfallingObstacle = true;

        }
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

    void EatPoisonParticle()
    {
        if(portion.reversalPotion)
        {
            PoisonParticle.gameObject.SetActive(true);
        }
        else if(!portion.reversalPotion)
        {
            PoisonParticle.gameObject.SetActive(false);
        }
    }

    void DieMotion()
    {
        Debug.Log("�÷��̾� ���");
        Dead = true;
        DiePs.gameObject.SetActive(true);
        anim.SetTrigger("isDead");
        Invoke("ReLoadScene", 1.5f);
    }

    void ReLoadScene()
    {
        SceneManager.LoadScene("CaveScene");
    }

}

