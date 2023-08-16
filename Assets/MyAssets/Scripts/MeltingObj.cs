using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeltingObj : MonoBehaviour
{
    public GameObject meltingObj; // ������ �����ϰ� setActive�� ������ ������Ʈ
    public Color colColor = Color.red; // �浹 �� ǥ���� ����
    private Color originalColor; // ������ ����
    private bool isCollision = false; // �浹 ���� Ȯ��
    public float hideTime; // ������ �ð�
    public float respawnTime; // �ٽ� ������ �ð�
    private Vector3 originalPos; // ���� ��ġ

    void Start()
    {
        // �ʱ� ���� ���� ����
        originalColor = meltingObj.GetComponent<Renderer>().material.color;

        // �ʱ� ���� ��ġ ����
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
        meltingObj.transform.position = originalPos; // ���� ��ġ�� �̵�
        meltingObj.SetActive(true);
        isCollision = false;
    }
}
