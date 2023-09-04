using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LoadingTyping : MonoBehaviour
{
    public TextMeshProUGUI textUI;
    public float typingSpeed = 0.05f; // Ÿ���� �ӵ� (�ʴ� ���� ��)

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
        while (true) // ���� ����
        {
            index = 0; // �ε����� ����
            currentText = ""; // ���� �ؽ�Ʈ�� ����

            while (index < fullText.Length)
            {
                currentText += fullText[index];
                textUI.text = currentText;
                index++;
                yield return new WaitForSeconds(typingSpeed);
            }

            yield return new WaitForSeconds(1f); // �ؽ�Ʈ�� �Ϸ�� �� 2�� ���
        }
    }

}
