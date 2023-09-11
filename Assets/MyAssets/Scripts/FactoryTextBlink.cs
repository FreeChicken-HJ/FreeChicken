using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class FactoryTextBlink : MonoBehaviour
{
    public TextMeshProUGUI text;
    float time;
    void Update()
    {
        if(time < 0.5f)
        {
            text.color = new Color(1, 0, 0, 1 -time);
        }
        else
        {
            text.color = new Color(1, 0, 0, time);
            if(time > 1f)
            {
                time = 0;
            }
        }
        time += Time.deltaTime;
    }
  
}
