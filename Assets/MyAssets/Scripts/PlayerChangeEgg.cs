using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChangeEgg : MonoBehaviour
{
    public FactoryPlayer factoryPlayer;
    public FactoryMoveEggBox moveEggBox;
    // Start is called before the first frame update
    void Start()
    {
        factoryPlayer = GameObject.Find("FactoryPlayer").GetComponent<FactoryPlayer>();   
        moveEggBox = GameObject.Find("EggBox").GetComponent <FactoryMoveEggBox>();

    }
    // Update is called once per frame
    void Update()
    {
        if (factoryPlayer.isSetEggFinish)
        {
            Change();
        }
    }
    void Change()
    {
      
        factoryPlayer.EggPrefab.transform.position = factoryPlayer.tmpBox.transform.position;
      
    }
}
