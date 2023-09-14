using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LoadingTyping : MonoBehaviour
{
    public TextMeshProUGUI textUI;
    public float typingSpeed = 0.05f;

    private string fullText;
    private string currentText;
    private int index;

   

    private void Start()
    {
        if (textUI != null)
        {
            fullText = textUI.text;

            StartCoroutine(TypeText());
        }
    }

    private IEnumerator TypeText()
    {
        while (true) 
        {
            index = 0; 
            currentText = "";

            while (index < fullText.Length)
            {
                currentText += fullText[index];
                textUI.text = currentText;
                index++;
                yield return new WaitForSeconds(typingSpeed);
            }

            yield return new WaitForSeconds(1f); 
        }
    }

}
