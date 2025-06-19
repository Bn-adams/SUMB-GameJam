using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlay : MonoBehaviour
{
    public AudioSource audioSource;

    public AudioClip clip;

    public float volume;

    public void PlaySoundEffect()
    {
        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.Play(); 
        Destroy(gameObject, audioSource.clip.length);
    }
}
