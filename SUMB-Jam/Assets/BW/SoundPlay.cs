using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlay : MonoBehaviour
{
    public AudioSource audioSource;

    private void Awake()
    {
        Destroy(gameObject, audioSource.clip.length);
    }
}
