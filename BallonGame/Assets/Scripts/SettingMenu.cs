using UnityEngine;
using UnityEngine.Audio;

public class SettingMenu : MonoBehaviour
{
    public AudioMixer audiomixer;
    public void SetVolume(float _Volume) 
    {
        PlayerPrefs.SetFloat("volume", _Volume);
        AudioListener.volume = PlayerPrefs.GetFloat("volume");
        audiomixer.SetFloat("Volume", _Volume);
    }
}
