using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FirstScene_Typing : MonoBehaviour
{
    private string text;
    public TextMeshProUGUI targetText;
    public float initialDelay = 2.0f; 
    public float delay = 0.5f;
    public AudioSource[] typingSounds; 
    public AudioSource FirstSound;
    private int currentSoundIndex = 0; 
    public float fadeDuration = 1.0f; 
    public float textFadeDuration = 2.0f; 
    public float waitBeforeLoad = 1.0f; 

    public Image fadeImage; 
    private TextMeshProUGUI textMeshPro;

    void Start()
    {
        Cursor.visible = false;
        text = targetText.text.ToString();
        targetText.text = " ";
        FirstSound.Play();
        textMeshPro = targetText.GetComponent<TextMeshProUGUI>();

        // 현재 텍스트에 대한 소리 할당
        if (currentSoundIndex < typingSounds.Length)
        {
            AudioSource selectedAudioSource = typingSounds[currentSoundIndex];
            StartCoroutine(StartTyping(selectedAudioSource));
        }
    }

    IEnumerator StartTyping(AudioSource audioSource)
    {
        yield return new WaitForSeconds(initialDelay); 

        int count = 0;

        while (count != text.Length)
        {
            if (count < text.Length)
            {
                targetText.text += text[count].ToString();
                audioSource.Play(); 
                count++;
            }

            yield return new WaitForSeconds(delay);
        }
        audioSource.Stop();

        float startTime = Time.time;
        float endTime = startTime + fadeDuration;
        Color startColor = fadeImage.color;
        Color endColor = Color.black; 

        while (Time.time < endTime)
        {
            float t = (Time.time - startTime) / fadeDuration;
            fadeImage.color = Color.Lerp(startColor, endColor, t);
            yield return null;
        }

        startTime = Time.time;
        endTime = startTime + textFadeDuration;
        Color textStartColor = textMeshPro.color;
        Color textEndColor = new Color(textStartColor.r, textStartColor.g, textStartColor.b, 0f); 

        while (Time.time < endTime)
        {
            float t = (Time.time - startTime) / textFadeDuration;
            textMeshPro.color = Color.Lerp(textStartColor, textEndColor, t);
            yield return null;
        }

        yield return new WaitForSeconds(waitBeforeLoad);
        Cursor.visible = true;
        SceneManager.LoadScene("StartScene_Final");
    }
}

