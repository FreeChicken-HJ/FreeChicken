using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.Events;
public class AudioMixController : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider sfxSlider;
    
    // Start is called before the first frame update
    void Awake()
    {

        bgmSlider.onValueChanged.AddListener(SetBGMVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);
        
    }
    void Start()
    {
        float saveBGM = PlayerPrefs.GetFloat("BGM", 1.0f);
        audioMixer.SetFloat("BGM", Mathf.Log10(saveBGM) * 20);
        bgmSlider.value = saveBGM;
        float saveSFX = PlayerPrefs.GetFloat("SFX", 1.0f);
        audioMixer.SetFloat("SFX", Mathf.Log10(saveSFX) * 20);
        sfxSlider.value = saveSFX;
    }
  
    public void SetBGMVolume(float volume)
    {
        bgmSlider.value = volume;
        PlayerPrefs.SetFloat("BGM", volume);
        
        audioMixer.SetFloat("BGM", Mathf.Log10(volume) * 20);
        PlayerPrefs.Save();
    }
    public void SetSFXVolume(float volume)
    {
        sfxSlider.value = volume;
        PlayerPrefs.SetFloat("SFX", volume);

        audioMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
        PlayerPrefs.Save();
    }
}
