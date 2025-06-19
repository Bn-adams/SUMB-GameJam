using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public GameObject SoundPrefab;
    public float GlobalSoundVolume;

    [SerializeField] List<SoundCollection> soundCollections;

    public void SpawnSoundPrefab(Vector3 prefabPosition, string SoundCollection)
    {
        List<AudioClip> SoundClips = soundCollections.Find(x => x.Name == SoundCollection).soundPrefabs;
        GameObject spawnedSoundPrefab = Instantiate(SoundPrefab, prefabPosition, Quaternion.identity);
        if (spawnedSoundPrefab != null)
        {
            SoundPlay sound = spawnedSoundPrefab.GetComponent<SoundPlay>();
            sound.clip = SoundClips[Random.Range(0, SoundClips.Count -1)];
            sound.volume = GlobalSoundVolume;
            sound.PlaySoundEffect();
        }
        
    }
}

[System.Serializable]
public struct SoundCollection
{
    public List<AudioClip> soundPrefabs;
    public string Name;
}
