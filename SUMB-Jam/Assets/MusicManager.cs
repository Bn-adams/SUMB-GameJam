using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public List<AudioClip> music_Playlist;

    public AudioSource AudioSource;

    int Pointer = 0;

    private void Start()
    {
        Pointer = Random.Range(0, music_Playlist.Count);
        StartCoroutine(PlaySong(music_Playlist[Pointer]));
    }

    IEnumerator PlaySong(AudioClip MusicClip)
    {
        AudioSource.clip = MusicClip;
        AudioSource.Play();

        yield return new WaitForSecondsRealtime(MusicClip.length);

        if (Pointer < music_Playlist.Count - 1)
        {
            Pointer++;
            StartCoroutine(PlaySong(music_Playlist[Pointer]));
        }
        else
        {
            Pointer = 0;
            StartCoroutine(PlaySong(music_Playlist[Pointer]));
        }
    }
}
