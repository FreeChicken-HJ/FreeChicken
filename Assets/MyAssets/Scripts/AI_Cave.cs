using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class AI_Cave : MonoBehaviour
{
    [SerializeField] private string AIName;

    [SerializeField] private float walkSpeed;

    private Vector3 destination;

    private bool isAction;
    private bool isWalking;

    [SerializeField] private float walkTime;
    [SerializeField] private float waitTime;
    private float currentTime;

    [SerializeField] private Animator anim;
    [SerializeField] Rigidbody rigid;
    [SerializeField] private CapsuleCollider capsuleCol;

    protected NavMeshAgent nav;

    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        currentTime = waitTime;
        isAction = true;
    }

    void Update()
    {
        Move();
        //Rotation();
        ElapseTime();
    }

    private void Move()
    {
        if (isWalking)
            //rigid.MovePosition(transform.position + transform.forward * walkSpeed * Time.deltaTime);
            nav.SetDestination(transform.position + destination);
    }

    //private void Rotation()
    //{
    //    if (isWalking)
    //    {
    //        Vector3 _rotation = Vector3.Lerp(transform.eulerAngles, direction, 0.01f);
    //        rigid.MoveRotation(Quaternion.Euler(_rotation));
    //    }
    //}

    private void ElapseTime()
    {
        if (isAction)
        {
            currentTime -= Time.deltaTime;
            if (currentTime <= 0)  // 랜덤하게 다음 행동을 개시
                ReSet();
        }
    }

    private void ReSet()  // 다음 행동 준비
    {
        isWalking = false;
        isAction = true;
        nav.ResetPath();
        anim.SetBool("Walking", isWalking);

        destination.Set(Random.Range(-0.2f,0.2f),0f,Random.Range(0.5f,1f));

        RandomAction();
    }

    private void RandomAction()
    {
        int _random = Random.Range(0, 2); // 대기, 걷기

        if (_random == 0)
            Wait();
        else if (_random == 1)
            TryWalk();
    }

    private void Wait()  // 대기
    {
        currentTime = waitTime;
        Debug.Log("대기");
    }

    private void TryWalk()  // 걷기
    {
        currentTime = walkTime;
        isWalking = true;
        anim.SetBool("Walking", isWalking);
        Debug.Log("걷기");
    }
}
