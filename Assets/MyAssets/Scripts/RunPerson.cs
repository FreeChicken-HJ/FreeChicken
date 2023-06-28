using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunPerson : MonoBehaviour
{
    Animator animator;
    public float runSpeed;
    Vector3 curPosition;
    Vector3 curRotation;
    Quaternion rotation;
    // Start is called before the first frame update
    void Start()
    {
        curPosition = transform.position;
        curRotation = transform.localEulerAngles;
        rotation = Quaternion.Euler(curRotation);
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(Move());
    }
    IEnumerator Move()
    {
        yield return new WaitForSeconds(.5f);
        animator.SetTrigger("doRun");
        transform.Translate(Vector3.forward * runSpeed * Time.deltaTime);
        yield return new WaitForSeconds(3f);
        Instantiate(this.gameObject, curPosition, rotation);
        Destroy(this.gameObject);
        yield return new WaitForSeconds(2f);
    }
}
