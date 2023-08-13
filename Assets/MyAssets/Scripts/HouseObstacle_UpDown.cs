using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseObstacle_UpDown : MonoBehaviour
{
    public enum MoveObstacleType { A,B};
    public MoveObstacleType Type;

    public float moveSpeed = 3.0f;      // 오브젝트의 이동 속도

    private bool isPlayerFollow = false;
    private Transform player;
    private Vector3 originalPosition;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        originalPosition = transform.position;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerFollow = true;
            Invoke("DestroyObject", 2f);
            Invoke("RespawnObject", 5f);
        }
    }

    void Update()
    {
        switch (Type)
        {
            case MoveObstacleType.A:
                if (isPlayerFollow)
                {
                    transform.position += Vector3.up * moveSpeed * Time.deltaTime;
                    player.position += Vector3.up * moveSpeed * Time.deltaTime;
                }
                break;
            case MoveObstacleType.B:
                if (isPlayerFollow)
                {
                    transform.position += Vector3.down * moveSpeed * Time.deltaTime;
                    player.position += Vector3.down * moveSpeed * Time.deltaTime;
                }
                break;
        }
    }

    void DestroyObject()
    {
        gameObject.SetActive(false);
    }

    void RespawnObject()
    {
        gameObject.SetActive(true);
        transform.position = originalPosition; // 초기 위치로 이동
        isPlayerFollow = false;
    }

}
