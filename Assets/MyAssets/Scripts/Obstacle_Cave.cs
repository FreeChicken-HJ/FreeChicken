using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using static UnityEditor.PlayerSettings;

public class Obstacle_Cave : MonoBehaviour
{
    public float delayTime = 1f;
    public float repeatTime = 5f;

    public enum MoveObstacleType { A, B, C, D, E, F, G, H, I, J, K, L, M, N,O,P,Q};
    // N => Sense Ȯ�ο� 
    public MoveObstacleType Type;

    //PlayerController player;
    CaveScenePlayer player;
    //Sense_Cave sense;

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
    public float swingSpeed;

    //Fire
    public ParticleSystem firePs;
    public bool playerFirePs;

    public GameObject obj;
    public bool isSense;
    public bool removeObj;

    //Attack
    public bool isPlayerAttack;

    public bool isBallContact;
    //CubeRotate
    public Animator objAnimator;
    public GameObject moveObj;
    public bool isCube;
    public bool isBarrel;

    //Orbit
    public Transform Circletarget;
    public float orbitSpeed;
    Vector3 offSet;

    Rigidbody rigid;

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
        if(Type == MoveObstacleType.L) // �뱼�뱼
        {
            objAnimator = GetComponent<Animator>();
        }
        // Case C == Rotate
        // Case D == Big Jump

        // Case E == Delay & Drop
        // Case F == Swing
        rigid = GetComponent<Rigidbody>();

    }

    void Start()
    {
        player = GameObject.Find("CaveCharacter").GetComponent<CaveScenePlayer>();
        isPlayerFollow = false;
        isSense= false;
        removeObj = false;
        
    }

    void upDown()
    {
        isSense = false;
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
            if (isPlayerFollow)
            {
                player.gameObject.transform.position = player.gameObject.transform.position + new Vector3(0, 1, 0) * moveSpeed * Time.deltaTime;
            }
        }
        else
        {
            transform.position = transform.position + new Vector3(0, -1, 0) * moveSpeed * Time.deltaTime;
            if (isPlayerFollow)
            {
                player.gameObject.transform.position = player.gameObject.transform.position + new Vector3(0, -1, 0) * moveSpeed * Time.deltaTime;
            }
        }
    }

    void rotate()
    {
        isSense = false;
        transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
        if (isPlayerFollow)
        {
            player.gameObject.transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
        }

    }

    void rotate_y()
    {
        isSense = false;
        transform.Rotate(0, rotateSpeed, 0);
    }

    void rotate_z()
    {
        isSense = false;
        transform.Rotate(0, 0, rotateSpeed);
    }

    void leftRight()
    {
        isSense = false;
        float currentPositionX = transform.position.x;

        if (currentPositionX >= initPositionX + distance)
        {
            turnSwitch = false;
        }
        else if (currentPositionX <= turningPoint)
        {
            turnSwitch = true;
        }

        if (turnSwitch)
        {
            this.gameObject.transform.position = this.gameObject.transform.position + new Vector3(1, 0, 0) * moveSpeed * Time.deltaTime;
            if (isPlayerFollow)
            {
                player.gameObject.transform.position = player.gameObject.transform.position + new Vector3(1, 0, 0) * moveSpeed * Time.deltaTime;
            }
        }
        else
        {
            this.gameObject.transform.position = this.gameObject.transform.position + new Vector3(-1, 0, 0) * moveSpeed * Time.deltaTime;
            if (isPlayerFollow)
            {
                player.gameObject.transform.position = player.gameObject.transform.position + new Vector3(-1, 0, 0) * moveSpeed * Time.deltaTime;
            }
        }

    }
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
   
    void OnTriggerEnter(Collider other) // Case E == Delay & Drop
    {
        if (other.gameObject.tag == "Player" && isDropObj)
        {

            transform.position = Vector3.Lerp(transform.position, other.transform.position, dropSpeed);
        }

        if(other.gameObject.tag == "Player" && this.Type == MoveObstacleType.N )
        {
            isSense = true;
        }

        if(other.gameObject.tag == "Obstacle")
        {
            isBallContact = true;
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

        if (collision.gameObject.tag == "Wall")
        {
            Debug.Log("�ε���");
            removeObj = true;
            //obj.gameObject.SetActive(false);
            
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && isBigJump)
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
        isPlayerAttack = true;
        
        lerpTime += Time.deltaTime * swingSpeed;
        transform.rotation = CalculateMovementOfPendulum();


    }

    public void deguldegul()
    {

        if (!removeObj && isCube)
        {
            objAnimator.SetBool("isMove", true);

            moveObj.transform.position += new Vector3(0, 0, -1) * moveSpeed * Time.deltaTime;

        }
        if (!removeObj && isBarrel)
        {
            objAnimator.SetBool("isMove", true);

            moveObj.transform.position += new Vector3(1, 0, 0) * moveSpeed * Time.deltaTime;


        }
        else if (removeObj)
        {


            objAnimator.SetBool("isMove", false);

            //moveObj.SetActive(false);
            //obj.transform.position 
        }

    }

    void GetFire() // �����ϱ�
    {
        if (player.isSense == true)
        {
            firePs.Play();
        }
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

    void Accel()
    {
        rigid.AddForce(Vector3.right * moveSpeed, ForceMode.Acceleration);
    }

    void Accel_z()
    {
        rigid.AddForce(new Vector3(0, 0, -1) * moveSpeed, ForceMode.Acceleration);
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

    void Update()
    {
       /* if (removeObj)
        {
            obj.gameObject.SetActive(false);
        }*/

        switch (Type)
        {
            
            case MoveObstacleType.A:
                isMove = false;
                upDown();
                break;
            case MoveObstacleType.B:
                isMove = false;
                isPlayerAttack = true;
                leftRight();
                break;
            case MoveObstacleType.C:
                isMove = false;
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
                isMove = false;
                Swing();
                break;
            case MoveObstacleType.G:
                isPlayerAttack = true;
                rotate_y();
                break;
            case MoveObstacleType.H:
                isPlayerFollow = false;
                isMove = false;
                leftRightZ();
                break;
            case MoveObstacleType.I:
                isPlayerFollow = false;
                isMove = false;
                rotate_z();
                break;
            case MoveObstacleType.L:
                isMove = false;
                deguldegul();
                break;
            case MoveObstacleType.M:
                isMove = false;
                GetFire();
                break;
            case MoveObstacleType.N:
                if (isSense)
                {
                    moveObj.SetActive(true);
                }
                //isSense = true;
                break;
            case MoveObstacleType.O:
                Accel();
                break;
            case MoveObstacleType.P:
                Orbit();
                isMove = false;
                break;
            case MoveObstacleType.Q:
                Accel_z();
                break;
        }

    }
}
