using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using Unity.VisualScripting;

using UnityEngine;

public class Obstacle_House : MonoBehaviour
{
    public float delayTime = 1f;
    public float repeatTime = 5f;

    public enum MoveObstacleType { A, B, C,D,E,F,G,H,I,K};
    public MoveObstacleType Type;

    public HouseScenePlayer player;
    public HouseScene2_Player player2;
    

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

    //Drop
    public float dropSpeed;
    public bool isDropObj;

    //Swing
    public float angle = 0;
    private float lerpTime = 0;
    public float swingSpeed;

    //Circle
    public float circleR; // 반지름
    public float deg; // 각도
    public float objSpeed; // 원운동 속도
    public Transform Circletarget;
    public float orbitSpeed;
    Vector3 offSet;

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
        if(Type == MoveObstacleType.C)
        {
            initPositionZ = transform.position.z;
            turningPoint = initPositionZ - distance;
        }
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<HouseScenePlayer>();
        player2 = GameObject.FindGameObjectWithTag("Player").GetComponent<HouseScene2_Player>();
        isPlayerFollow = false;
        this.gameObject.SetActive(true);
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
            if (isPlayerFollow)
            {
                if (player != null)
                {
                    player.gameObject.transform.position = player.gameObject.transform.position + new Vector3(0, 1, 0) * moveSpeed * Time.deltaTime;
                }
                else if(player2 != null)
                {
                    player2.gameObject.transform.position = player2.gameObject.transform.position + new Vector3(0, 1, 0) * moveSpeed * Time.deltaTime;
                }
            }
        }
        else
        {
            transform.position = transform.position + new Vector3(0, -1, 0) * moveSpeed * Time.deltaTime;
            if (isPlayerFollow)
            {
                if (player != null)
                {
                    player.gameObject.transform.position = player.gameObject.transform.position + new Vector3(0, -1, 0) * moveSpeed * Time.deltaTime;
                }
                else if (player2 != null)
                {
                    player2.gameObject.transform.position = player2.gameObject.transform.position + new Vector3(0, -1, 0) * moveSpeed * Time.deltaTime;
                }
            }
        }
    }

    void rotate_z()
    {
        transform.Rotate(0, 0, -angle_z / 50 * rotateSpeed);
    }

    void rotate_y()
    {
        transform.Rotate(0, rotateSpeed, 0);
    }

    void rotatae_x()
    {
        transform.Rotate(-angle_z / 50 * rotateSpeed, 0, 0);
    }

    void DownandDestroy()
    {
        if(isPlayerFollow)
        {
            player.gameObject.transform.position = player.transform.position + new Vector3(0, -1, 0) * moveSpeed * Time.smoothDeltaTime;
        }
        transform.position = transform.position + new Vector3(0, -1, 0) * moveSpeed * Time.smoothDeltaTime;

        Destroy(this.gameObject, 5f);
    }


    void rotate_xyz()
    {
        transform.Rotate(-angle_z / 50, -angle_z / 50, -angle_z / 50);
    }    

    void leftRight_x()
    {
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
            transform.position = transform.position + new Vector3(1, 0, 0) * moveSpeed * Time.deltaTime;
            if (isPlayerFollow)
            {
                if (player != null)
                {
                    player.gameObject.transform.position = player.gameObject.transform.position + new Vector3(1, 0, 0) * moveSpeed * Time.deltaTime;
                }
                else if (player2 != null)
                {
                    player2.gameObject.transform.position = player2.gameObject.transform.position + new Vector3(1, 0, 0) * moveSpeed * Time.deltaTime;
                }
            }
        }
        else
        {
            transform.position = transform.position + new Vector3(-1, 0, 0) * moveSpeed * Time.deltaTime;
            if (isPlayerFollow)
            {
                if (player != null)
                {
                    player.gameObject.transform.position = player.gameObject.transform.position + new Vector3(-1, 0, 0) * moveSpeed * Time.deltaTime;
                }
                else if (player2 != null)
                {
                    player2.gameObject.transform.position = player2.gameObject.transform.position + new Vector3(-1, 0, 0) * moveSpeed * Time.deltaTime;
                }
            }
        }
    }

    void leftRight_z()
    {
        float currentPositionZ = transform.position.z;

        if (currentPositionZ >= initPositionZ + distance)
        {
            turnSwitch = false;
        }
        else if (currentPositionZ <= turningPoint)
        {
            turnSwitch = true;
        }

        if (turnSwitch)
        {
            transform.position = transform.position + new Vector3(0, 0, 1) * moveSpeed * Time.deltaTime;
            if (isPlayerFollow)
            {
                if (player != null)
                {
                    player.gameObject.transform.position = player.gameObject.transform.position + new Vector3(0, 0, 1) * moveSpeed * Time.deltaTime;
                }
                else if (player2 != null)
                {
                    player2.gameObject.transform.position = player2.gameObject.transform.position + new Vector3(0, 0, 1) * moveSpeed * Time.deltaTime;
                }
            }
        }
        else
        {
            transform.position = transform.position + new Vector3(0, 0, -1) * moveSpeed * Time.deltaTime;
            if (isPlayerFollow)
            {
                if (player != null)
                {
                    player.gameObject.transform.position = player.gameObject.transform.position + new Vector3(0, 0, -1) * moveSpeed * Time.deltaTime;
                }
                else if (player2 != null)
                {
                    player2.gameObject.transform.position = player2.gameObject.transform.position + new Vector3(0, 0, -1) * moveSpeed * Time.deltaTime;
                }
            }
        }
    }


    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") &&!isPlayerFollow)
        {
            isPlayerFollow = true;
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && isMove && !isPlayerFollow)
        {
            isPlayerFollow = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && isPlayerFollow)
        {
            isPlayerFollow = false;
        }
    }

    public void deguldegul()
    {
        transform.Rotate(0, 0, -angle_z / 50 * rotateSpeed);
        transform.position += new Vector3(0, 0, -1) * moveSpeed * Time.deltaTime;
    }

    void Orbit()
    {
        offSet = transform.position - Circletarget.position;

        transform.position = Circletarget.position + offSet;
        transform.RotateAround(Circletarget.position,
                                Vector3.up,
                                orbitSpeed*Time.deltaTime);
        offSet = transform.position - Circletarget.position;

        if (isPlayerFollow)
        {
            if (player != null)
            {
                player.gameObject.transform.RotateAround(Circletarget.position,
                                    Vector3.up,
                                    orbitSpeed * Time.deltaTime);
            }
            else if(player2 != null)
            {
                player2.gameObject.transform.RotateAround(Circletarget.position,
                                    Vector3.up,
                                    orbitSpeed * Time.deltaTime);
            }
        }
    }

    void Circle()
    {
        offSet = transform.position - Circletarget.position;

        transform.position = Circletarget.position + offSet;
        transform.RotateAround(Circletarget.position,
                                Vector3.back, orbitSpeed*Time.deltaTime);

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
                upDown();
                break;
            case MoveObstacleType.B:
                isMove = true;
                leftRight_x();
                break;
            case MoveObstacleType.C:
                isMove = true;
                leftRight_z();
                break;
            case MoveObstacleType.D:
                isMove = false;
                deguldegul();
                break;
            case MoveObstacleType.E:
                rotate_z();
                break;
            case MoveObstacleType.F:
                isMove = true;
                Orbit();
                break;
            case MoveObstacleType.G:
                isMove = true;
                rotatae_x();
                break;
            case MoveObstacleType.H:
                Circle();
                break;
            case MoveObstacleType.I:
                isMove = true;
                rotate_y();
                break;
            case MoveObstacleType.K:
                rotate_xyz();
                break;
        }
    }
}
