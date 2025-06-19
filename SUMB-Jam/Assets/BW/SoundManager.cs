using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public GameObject SoundPrefab;
    public float GlobalSoundVolume;

    [SerializeField] List<SoundCollection> soundCollections;
    public void SpawnSoundPrefab(Transform prefabPosition, int prefabElement)
    {
        if (prefabElement > soundCollections.Count - 1)
        {
            List<AudioClip> SoundClips = soundCollections[prefabElement].soundPrefabs;
            GameObject spawnedSoundPrefab = Instantiate(SoundPrefab, prefabPosition);
            SoundPlay sound = spawnedSoundPrefab.GetComponent<SoundPlay>();
            sound.clip = SoundClips[Random.Range(0, SoundClips.Count)];
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
