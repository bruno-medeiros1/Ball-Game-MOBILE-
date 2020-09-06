using UnityEngine.Audio;
using System;//esta tag permite-nos usar uma sintaxe de procura numa array de uma forma mais rapida
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance; 
    public Sound[] sound;

    private void Awake()
    {
        /*Atribuição de todas as propriedades que se encontram na classe Sound a cada
         Som que estiver na lista de objectos da Classe Sound*/
        foreach (Sound s in sound) 
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.clip = s.clip;
            s.source.mute = s.mute;
        }
    }
    public void Start()
    {
        AudioListener.volume = PlayerPrefs.GetFloat("volume");
        Play("Theme");

    }
    public void Stop(string name)
    {
        Sound s = Array.Find(sound, sound => sound.name == name);
        if(s == null) 
        {
            Debug.LogWarning(name + " nao existe na lista do AudioManager!");
            return;
        }
        s.source.Stop();
    }
    public void Play(string name) 
    {
        Sound s = Array.Find(sound, sound => sound.name == name);
        if( s == null) 
        {
            Debug.LogWarning(name + " nao existe na lista do AudioManager!");
            return;
        }
        s.source.Play();
    }
}
