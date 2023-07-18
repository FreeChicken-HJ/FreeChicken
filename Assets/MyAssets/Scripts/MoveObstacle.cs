using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
//using static UnityEditor.PlayerSettings;

public class MoveObstacle : MonoBehaviour
{
    public float delayTime = 1f;
    public float repeatTime = 5f;

    public enum MoveObstacleType { A, B, C, D, E, F ,G,H,I,J,K,L,M};
    public MoveObstacleType Type;
    //PlayerController player;
    GameObject player;

    //UD_Floor
    float initPositionY;
    float initPositionX;
    float initPositionZ;
    public float distance;
    public float turningPoint;
    //UD_Floor & LR_Floor
    public bool turnSwitch;
    public float moveSpeed;

    //MovePlatform
    public bool isMove;
    public bool isPlayerFollow;

    //RT_Floor
    public float rotateSpeed;
    public int angle_z = 50;

    //Big Jump
    public bool isBigJump;
    public float BigJumpPower;
    //Drop
    public float dropSpeed;
    public bool isDropObj;
    //Swing
    public float angle = 0;
    private float lerpTime = 0;
    private float speed = 2f;

    //Fire
    public ParticleSystem firePs;
    public bool playerFirePs;

    //Attack
    public bool isPlayerAttack;

    public float circleR; // 반지름
    public float deg; // 각도
    public float objSpeed; // 원운동 속도

    public Transform Circletarget;
    public float orbitSpeed;
    Vector3 offSet;

    // ChangeSmallObj
    public bool isContact;
    public bool isChk;
    private Vector3 pos;
    private Vector3 scale;
    public ParticleSystem ChangeParticle;

    //Down & Destory Obj
    public bool isDown;
    public bool isDownandDestroy;
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        isPlayerFollow = false;
    }
    
    void Awake()
    {
        if (Type == MoveObstacleType.A) // Up & Down
        {
            initPositionY = transform.position.y;
            turningPoint = initPositionY - distance;

        }
        if (Type == MoveObstacleType.B) // Right & Left
        {
            initPositionX = transform.position.x;
            turningPoint = initPositionX - distance;

        }
       
        if (Type == MoveObstacleType.H)
        {
            initPositionZ = transform.position.z;
            turningPoint = initPositionZ - distance;
        }
        

        // Case C == Rotate
        // Case D == Big Jump

        // Case E == Delay & Drop
        // Case F == Swing

    }
    void upDown()
    {
        float currentPositionY = transform.position.y;

        if (currentPositionY >= initPositionY)
        {
            turnSwitch = false;
        }
        else if (currentPositionY <= turningPoint)
        {
            turnSwitch = true;
        }

        if (turnSwitch)
        {
            transform.position = transform.position + new Vector3(0, 1, 0) * moveSpeed * Time.deltaTime;
            /*if (isPlayerFollow)
            {
                player.gameObject.transform.position = player.gameObject.transform.position + new Vector3(0, 1, 0) * moveSpeed * Time.deltaTime;
            }
        }
        else
        {
            transform.position = transform.position + new Vector3(0, -1, 0) * moveSpeed * Time.deltaTime;
            /*if (isPlayerFollow)
            {
                player.gameObject.transform.position = player.gameObject.transform.position + new Vector3(0, -1, 0) * moveSpeed * Time.deltaTime;
            }*/
        }
    }

    void rotate()
    {
        transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
        if (isPlayerFollow)
        {
            player.gameObject.transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
        }
        
    }

    //void rotate_z()
    //{
    //    transform.Rotate(0, 0, -angle_z / 50);
    //}
    void leftRightZ()
    {
        float currentPositionZ = transform.position.z;

        if (currentPositionZ >= initPositionZ + distance)
        {

            turnSwitch = false;
        }
        else if (currentPositionZ <= turningPoint)
        {
            //yield return new WaitForSeconds(1.5f);
            turnSwitch = true;
        }

        if (turnSwitch)
        {
            //yield return new WaitForSeconds(1.5f);
            transform.position = transform.position + new Vector3(0, 0, 1) * moveSpeed * Time.deltaTime;
            if (isPlayerFollow)
            {
                player.gameObject.transform.position = player.gameObject.transform.position + new Vector3(0, 0, 1) * moveSpeed * Time.deltaTime;
            }
        }
        else
        {
            //yield return new WaitForSeconds(1.5f);
            transform.position = transform.position + new Vector3(0, 0, -1) * moveSpeed * Time.deltaTime;
            if (isPlayerFollow)
            {
                player.gameObject.transform.position = player.gameObject.transform.position + new Vector3(0, 0, -1) * moveSpeed * Time.deltaTime;
            }
        }
    }
    void leftRight()
    {

        float currentPositionX = transform.position.x;

        if (currentPositionX >= initPositionX + distance)
        {
            
            turnSwitch = false;
        }
        else if (currentPositionX <= turningPoint)
        {
            //yield return new WaitForSeconds(1.5f);
            turnSwitch = true;
        }

        if (turnSwitch)
        {
            //yield return new WaitForSeconds(1.5f);
            transform.position = transform.position + new Vector3(1, 0, 0) * moveSpeed * Time.deltaTime;
            if (isPlayerFollow)
            {
                player.gameObject.transform.position = player.gameObject.transform.position + new Vector3(1, 0, 0) * moveSpeed * Time.deltaTime;
            }
        }
        else
        {
            //yield return new WaitForSeconds(1.5f);
            transform.position = transform.position + new Vector3(-1, 0, 0) * moveSpeed * Time.deltaTime;
            if (isPlayerFollow)
            {
                player.gameObject.transform.position = player.gameObject.transform.position + new Vector3(-1, 0, 0) * moveSpeed * Time.deltaTime;
            }
        }

    }


    void OnTriggerEnter(Collider other) // Case E == Delay & Drop
    {
        if(other.gameObject.tag == "Player" && isDropObj)
        {
           
            transform.position = Vector3.Lerp(transform.position, other.transform.position, dropSpeed);
        }
     
    }
    void OnCollisionEnter(Collision collision)
    {  
        if (collision.gameObject.tag == "Player" && isPlayerAttack)
        {
            //player.healthbar.value -= 10f;
            
        }
        if (collision.gameObject.tag == "Player")
        {
            isPlayerFollow = true;
        }
        if (collision.gameObject.tag == "Player" && isContact &&!isChk)
        {
            isChk = true;
            StartCoroutine("SmallObj");
        }
        if (collision.gameObject.tag == "Player" && isDownandDestroy)
        {
            isDown = true;
            isDownandDestroy = false;
           

        }
    }
    void DownandDestroy()
    {
        if (isPlayerFollow)
        {
            player.gameObject.transform.position = player.transform.position + new Vector3(0, -1, 0) * 10f * Time.smoothDeltaTime;
        }
        transform.position = transform.position + new Vector3(0, -1, 0) * 10f * Time.smoothDeltaTime;
       
        Destroy(this.gameObject, 5f);

    }
    IEnumerator SmallObj()
    {

        pos = this.gameObject.transform.position;
        scale = this.gameObject.transform.localScale;
        yield return new WaitForSeconds(1f);
        this.gameObject.transform.localScale = new Vector3(2f, 2f, 2f);
        ChangeParticle.Play();
        yield return new WaitForSeconds(0.5f);
        this.gameObject.transform.localScale = new Vector3(1.5f,1.5f , 1.5f);
        ChangeParticle.Play();
        this.Type = MoveObstacleType.B;

        initPositionX = transform.position.x;
        turningPoint = initPositionX - distance;
        yield return new WaitForSeconds(10f);
        this.Type = MoveObstacleType.L;
        //this.moveSpeed = 0f;
        this.gameObject.transform.position = pos;
        this.gameObject.transform.localScale = scale;
        isChk = false;


    }
            //MoveObstacleType.A
    
    void OnCollisionStay(Collision collision) 
    {
        if(collision.gameObject.tag == "Player" && isBigJump)
        {
            collision.rigidbody.AddForce(Vector3.forward * BigJumpPower, ForceMode.Impulse);
           
            isBigJump = false;
        }
        if (collision.gameObject.tag == "Player" && isMove) 
        {
            isPlayerFollow = true;

        }
       
    }
    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isPlayerFollow = false;
        }
    }

    void Swing()
    {
        //isPlayerAttack = true;
        lerpTime += Time.deltaTime * speed;
        transform.rotation = CalculateMovementOfPendulum();


    }

    
    void Orbit()
    {
        offSet = transform.position - Circletarget.position;

        transform.position = Circletarget.position + offSet;
        transform.RotateAround(Circletarget.position,
                                Vector3.up,
                                orbitSpeed * Time.deltaTime);
        offSet = transform.position - Circletarget.position;

        if (isPlayerFollow)
        {
            player.gameObject.transform.RotateAround(Circletarget.position,
                                Vector3.up,
                                orbitSpeed * Time.deltaTime);
        }
    }

    void Circle()
    {
        offSet = transform.position - Circletarget.position;

        transform.position = Circletarget.position + offSet;
        transform.RotateAround(Circletarget.position,
                                Vector3.back, orbitSpeed * Time.deltaTime);

        offSet = transform.position - Circletarget.position;
    }

    
    Quaternion CalculateMovementOfPendulum()
    {
        return Quaternion.Lerp(Quaternion.Euler(Vector3.forward * angle),
            Quaternion.Euler(Vector3.back * angle), GetLerpTParam());
    }

    float GetLerpTParam()
    {
        return (Mathf.Sin(lerpTime) + 1) * .5f;
    }

void Update()
    {

        switch (Type)
        {
            case MoveObstacleType.A:
                isMove = true;
                //isPlayerAttack = true;
                upDown();

                break;
            case MoveObstacleType.B:
                isMove = true;
                //isPlayerAttack = true;
                //StartCoroutine(leftRight());
                leftRight();
                break;
            case MoveObstacleType.C:
                isMove = true;
                rotate();
                break;
            case MoveObstacleType.D:
                isBigJump = true;
                break;
            case MoveObstacleType.E:
                isDropObj = true;
                break;
            case MoveObstacleType.F:
                isPlayerAttack = true;
                Swing();
                break;
            case MoveObstacleType.G:
                isPlayerAttack = true;
                //Fire();
                break;
            case MoveObstacleType.H:
                isMove = true;
                leftRightZ();
                break;
            case MoveObstacleType.I:
                isMove = false;
                //rotate_z();
                break;
            case MoveObstacleType.J:
                isMove = true;
                Orbit();
                break;
            case MoveObstacleType.K:
                Circle();
                break;
            case MoveObstacleType.L: // 밟으면 작아지는 obj
                isContact = true;
                break;
            case MoveObstacleType.M:
                isDownandDestroy = true;
                if (isDown)
                {
                    DownandDestroy();
                    
                }
                break;
           

        }
       
    }
  
}
