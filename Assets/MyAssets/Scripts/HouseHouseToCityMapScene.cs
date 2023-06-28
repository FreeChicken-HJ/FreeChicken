using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
public class HouseHouseToCityMapScene : MonoBehaviour
{
    public GameObject TalkUI;
    public GameObject CityMapUI;

    public CinemachineVirtualCamera mainCam;
    public CinemachineVirtualCamera startCam;
    
    // Start is called before the first frame update
    void Start()
    {
        
        Invoke("Talk", 3f);
        //Destroy(gameObject,3f);
        
    }
    private void Update()
    {
        this.gameObject.transform.Translate(Vector3.forward * Time.deltaTime * 5f, Space.World);


        startCam.Priority = 1;
        mainCam.Priority = 2;
        
    }
    void Talk()
    {
        TalkUI.SetActive(true);
        CityMapUI.SetActive(true);
        
    }
}
