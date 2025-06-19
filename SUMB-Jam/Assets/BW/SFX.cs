using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX : MonoBehaviour
{
    [SerializeField] AudioSource playerAudioSource;

    [SerializeField] AudioClip[] swordSwoosh;
    [SerializeField] AudioClip[] swordClang;
    [SerializeField] AudioClip[] hitSound;
    [SerializeField] AudioClip[] musketShot;
    [SerializeField] AudioClip[] playerWalking;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwordSwoosh()
    {
        int swordSwooshElement = Random.Range(0, swordSwoosh.Length);

        playerAudioSource.clip = swordSwoosh[swordSwooshElement];
        playerAudioSource.Play();
    }

    public void SwordClang()
    {

    }

    public void HitSound()
    {

    }

    public void MusketShot()
    {

    }

    public void PlayerWalking()
    {

    }

    public void CoinPickup()
    {

    }

    public void Heartbeat()
    {

    }
}
