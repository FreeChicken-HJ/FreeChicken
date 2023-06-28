using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    Vector3 Start_Pos;
    // Update is called once per frame
   
    void Update()
    {
        transform.position = target.position + offset;
        Start_Pos = transform.localPosition;
    }
    public IEnumerator Shake(float duration, float magnitude)
    {
        float timer = 0;
        while (timer <= duration)
        {
            transform.localPosition = (Vector3)Random.insideUnitSphere * magnitude + Start_Pos;

            timer += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = Start_Pos;
    }

}
