using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LoadingTyping : MonoBehaviour
{
    public TextMeshProUGUI textUI;
    public float typingSpeed = 0.05f; // 타이핑 속도 (초당 글자 수)

    private string fullText;
    private string currentText;
    private int index;

    private void Start()
    {
        fullText = textUI.text;
        StartCoroutine(TypeText());
    }

    private IEnumerator TypeText()
    {
        while (true) // 무한 루프
        {
            index = 0; // 인덱스를 리셋
            currentText = ""; // 현재 텍스트를 리셋

            while (index < fullText.Length)
            {
                currentText += fullText[index];
                textUI.text = currentText;
                index++;
                yield return new WaitForSeconds(typingSpeed);
            }

            yield return new WaitForSeconds(1f); // 텍스트가 완료된 후 2초 대기
        }
    }

}
