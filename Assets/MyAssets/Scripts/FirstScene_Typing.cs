using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FirstScene_Typing : MonoBehaviour
{
   
    public TextMeshProUGUI[] targetText;
    public float initialDelay = 2.0f; 
    public float delay = 0.3f;
    public AudioSource[] typingSounds; 
    public AudioSource FirstSound;
    private int currentSoundIndex = 0; 
    public float fadeDuration = 1.0f; 
    public float textFadeDuration = 2.0f; 
    public float waitBeforeLoad = 1.0f; 

    public Image fadeImage; 
   
    void Start()
    {
        Cursor.visible = false;
        for(int i = 0; i < targetText.Length; i++)
        {
           
            FirstSound.Play();
          
            if (currentSoundIndex < typingSounds.Length)
            {
                AudioSource selectedAudioSource = typingSounds[currentSoundIndex];
                StartCoroutine(StartTyping(selectedAudioSource));
            }
        }
      
    }

    IEnumerator StartTyping(AudioSource audioSource)
    {
        yield return new WaitForSeconds(initialDelay); 

      

        for (int i = 0; i < targetText.Length; i++)
        {
            
            targetText[i].gameObject.SetActive(true);
            audioSource.Play();
            yield return new WaitForSeconds(.3f);
            
        }
  

        yield return new WaitForSeconds(delay);
        

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
        

        for (int i = 0; i < targetText.Length; i++)
        {
            

            targetText[i].gameObject.SetActive(false);
           

        }
           
        yield return new WaitForSeconds(waitBeforeLoad);
        Cursor.visible = true;
        SceneManager.LoadScene("StartScene_Final");
    }
}

