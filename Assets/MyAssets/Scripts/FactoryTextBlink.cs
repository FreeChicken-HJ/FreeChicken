using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class FactoryTextBlink : MonoBehaviour
{
    public TextMeshProUGUI text;
   
    // Start is called before the first frame update
    void Start()
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
    }
}
