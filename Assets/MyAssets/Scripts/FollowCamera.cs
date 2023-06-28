using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform target; 
    public Vector3 firstTarget; // 현재 플레이어 위치
    public Vector3 offset; // 카메라 위치

    void Update()
    {
        transform.position = target.position + offset - firstTarget;
    }

}
