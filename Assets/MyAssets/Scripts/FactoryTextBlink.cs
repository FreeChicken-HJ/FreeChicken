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
            text.color = new Color(1, 1, 1, 1 -time);
        }
        else
        {
            text.color = new Color(1, 1, 1, time);
            if(time > 1f)
            {
                time = 0;
            }
        }
        time += Time.deltaTime;
    }
    /*void Start()
    {
        text = GetComponent<TextMeshProUGUI>();

        StartCoroutine(BlinkText());


    }
  
    public IEnumerator BlinkText()
    {
        //yield return new WaitForSeconds(1f);
        while (true)
        {
            text.text = "";
            yield return new WaitForSeconds(.5f);
            text.text = "»ß¾à!! »ß¾à!!";
            yield return new WaitForSeconds(.5f);
        }
    }*/
}
