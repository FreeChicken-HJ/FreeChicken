using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeltingObj : MonoBehaviour
{
    public GameObject meltingObj; // 색상을 변경하고 setActive를 조작할 오브젝트
    public Color colColor = Color.red; // 충돌 시 표시할 색상
    private Color originalColor; // 원래의 색상
    private bool isCollision = false; // 충돌 상태 확인
    public float hideTime; // 숨겨질 시간
    public float respawnTime; // 다시 생성될 시간
    private Vector3 originalPos; // 원래 위치

    void Start()
    {
        // 초기 원래 색상 저장
        originalColor = meltingObj.GetComponent<Renderer>().material.color;

        // 초기 원래 위치 저장
        originalPos = meltingObj.transform.position;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!isCollision)
            {
                meltingObj.GetComponent<Renderer>().material.color = colColor;
                isCollision = true;

                Invoke("HideObject", hideTime);
            }
        }
    }

    void HideObject()
    {
        meltingObj.SetActive(false);
        Invoke("RespawnObject", respawnTime);
    }

    void RespawnObject()
    {
        meltingObj.GetComponent<Renderer>().material.color = originalColor;
        meltingObj.transform.position = originalPos; // 원래 위치로 이동
        meltingObj.SetActive(true);
        isCollision = false;
    }
}
