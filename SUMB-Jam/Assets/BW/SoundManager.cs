using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public SoundPlay soundPlayScript;
    
    [SerializeField] List<GameObject> soundPrefabs;
    public void SpawnSoundPrefab(Vector3 prefabPosition, int prefabElement)
    {
        GameObject spawnedSoundPrefab = Instantiate(soundPrefabs[prefabElement], prefabPosition, Quaternion.identity);
        if (spawnedSoundPrefab != null)
        {
            soundPlayScript = spawnedSoundPrefab.GetComponent<SoundPlay>();
        }
    }
}
