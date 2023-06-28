using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FactorySceneLeaveTruk : MonoBehaviour
{
    public bool isTouch;
    public GameObject particle;
    public GameObject showCanvas;

    public GameObject playerDieParticle;
    public bool isNextScene2;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (isTouch)
        {
            this.gameObject.transform.Translate(Vector3.forward * Time.deltaTime * 2.5f);
            showCanvas.SetActive(true);
            if (isNextScene2)
            {
                playerDieParticle.SetActive(true);

            }


        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("�浹");
            isTouch = true;
            particle.SetActive(true);

            Invoke("ReLoad", 3.5f);
        }
    }
    void ReLoad()
    {
        if (isNextScene2)
        {
            SceneManager.LoadScene("FactoryScene_2");
        }
        else
        {
            SceneManager.LoadScene("FactoryScene_3");
        }
       
    }
}
// �� ������ 
// �ι��� �� �����Ҷ� ���� ȭ�� 3�� �Ŀ� �ٽ� ���� 
