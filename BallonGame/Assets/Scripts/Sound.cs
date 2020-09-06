using UnityEngine.Audio;
using UnityEngine;

[System.Serializable] //permite que a nossa classe seja vista no inspector
public class Sound 
{
    public string name;
    [Range(0.1f, 3f)]
    public float pitch;

    [Range(0f, 1f)]
    public float volume;

    public bool loop;

    [HideInInspector]
    public AudioSource source;

    public AudioClip clip;
    public bool mute;

}
