using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CityMap_CountDown : MonoBehaviour
{
    public TextMeshProUGUI text;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CountDown());
    }
    IEnumerator CountDown()
    {
        int countdownValue = 3;

        while (countdownValue > 0)
        {
            text.text = countdownValue.ToString();
            yield return new WaitForSeconds(.9f);
            countdownValue--;
        }

        text.text = "GO!";
        yield return new WaitForSeconds(.2f);

        text.gameObject.SetActive(false);
    }
}
