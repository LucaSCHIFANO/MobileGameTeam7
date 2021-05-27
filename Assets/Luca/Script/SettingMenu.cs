using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public AudioMixer audioMixer2;

    public GameObject toFalse;

    void Start()
    {
        SetVolume(PlayerPrefs.GetFloat("volumeOST"));
        SetVolumeSFX(PlayerPrefs.GetFloat("volumeSFX"));

        GameObject.Find("SliderOST").GetComponent<Slider>().value = PlayerPrefs.GetFloat("volumeOST");
        GameObject.Find("SliderSFX").GetComponent<Slider>().value = PlayerPrefs.GetFloat("volumeSFX");

        toFalse.SetActive(false);
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("VolumeOST", volume);
        PlayerPrefs.SetFloat("volumeOST", volume);
    }

    public void SetVolumeSFX(float volumeSFX)
    {
        audioMixer2.SetFloat("VolumeSFX", volumeSFX);
        PlayerPrefs.SetFloat("volumeSFX", volumeSFX);
    }
}