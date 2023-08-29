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
   
    public FactoryPlayer_2 player;
    public GameObject Pos;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").
            GetComponent<FactoryPlayer_2>();
            
    }

    // Update is called once per frame

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" &&!isTouch)
        {
           
            isTouch = true;
            particle.SetActive(true);
            showCanvas.SetActive(true);

            playerDieParticle.SetActive(true);

            Invoke("ReLoad", 2.5f);
        }
    }
    void Restart()
    {
        DeadCount.count++;
        showCanvas.SetActive(false);
        
        player.transform.position = Pos.transform.position;
        player.isDie = false;

        playerDieParticle.SetActive(false);
        isTouch = false;


    }
    void ReLoad()
    {
        player.isDie = true;
        Invoke("Restart", .5f);
    }
}

