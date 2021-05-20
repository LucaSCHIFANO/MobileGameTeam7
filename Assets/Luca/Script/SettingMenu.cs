using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public AudioMixer audioMixer2;

    void Start()
    {
        SetVolume(PlayerPrefs.GetFloat("volumeOST"));
        SetVolumeSFX(PlayerPrefs.GetFloat("volumeSFX"));
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