using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Obstacle_House : MonoBehaviour
{
    public float delayTime = 1f;
    public float repeatTime = 5f;

    public enum MoveObstacleType { A, B, C,D,E,F,G,H};
    public MoveObstacleType Type;

    HouseScenePlayer player;
    //Sense_House sense_house;

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

    //Dart
    public bool isDartFollow = true;

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

        //if (Type == MoveObstacleType.H)
        //{
        //    initPositionZ = transform.position.z;
        //    turningPoint = initPositionZ - distance;
        //}
    }

    void Start()
    {
        player = GameObject.Find("HouseCharacter").GetComponent<HouseScenePlayer>();
        isPlayerFollow = false;
        //isDartFollow = false;
        //sense_house = GameObject.FindGameObjectWithTag("Obstacle").GetComponent<Sense_House>();

        //offSet = transform.position - Circletarget.position;
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
        transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
        if (isPlayerFollow)
        {
            player.gameObject.transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
        }
    }

    void rotate_z()
    {
        transform.Rotate(0, 0, -angle_z / 50);
    }

    void rotatae_x()
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
                player.gameObject.transform.position = player.gameObject.transform.position + new Vector3(1, 0, 0) * moveSpeed * Time.deltaTime;
            }
        }
        else
        {
            transform.position = transform.position + new Vector3(-1, 0, 0) * moveSpeed * Time.deltaTime;
            if (isPlayerFollow)
            {
                player.gameObject.transform.position = player.gameObject.transform.position + new Vector3(-1, 0, 0) * moveSpeed * Time.deltaTime;
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
                player.gameObject.transform.position = player.gameObject.transform.position + new Vector3(0, 0, 1) * moveSpeed * Time.deltaTime;
            }
        }
        else
        {
            transform.position = transform.position + new Vector3(0, 0, -1) * moveSpeed * Time.deltaTime;
            if (isPlayerFollow)
            {
                player.gameObject.transform.position = player.gameObject.transform.position + new Vector3(0, 0, -1) * moveSpeed * Time.deltaTime;
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isPlayerFollow = true;
        }

        if(collision.gameObject.tag == "Note")
        {
            isDartFollow = true;
        }

        //if (collision.gameObject.tag == "Wall")
        //{
        //    Debug.Log("부딪힘");
        //    gameObject.SetActive(false);
        //}
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

        if(collision.gameObject.tag == "Note")
        {
            isDartFollow = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isPlayerFollow = false;
        }

        if(collision.gameObject.tag == "Note")
        {
            isDartFollow = false;
        }
    }

    public void deguldegul()
    {
        transform.Rotate(0, 0, -angle_z / 50);
        transform.position += new Vector3(0, 0, -1) * moveSpeed * Time.deltaTime;
    }

    //void Circle()
    //{
    //    deg += Time.deltaTime * objSpeed;
    //    if (deg < 360)
    //    {
    //        var rad = Mathf.Deg2Rad * (deg);
    //        var x = circleR * Mathf.Sin(rad);
    //        var y = circleR * Mathf.Cos(rad);
    //        var z = circleR * Mathf.Tan(rad);
    //        transform.position = transform.position + new Vector3(x, y,z);
    //        transform.rotation = Quaternion.Euler(0, 0, deg * -1); //가운데를 바라보게 각도 조절
    //    }
    //    else
    //    {
    //        deg = 0;
    //    }
    //}

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
        }
    }
}
