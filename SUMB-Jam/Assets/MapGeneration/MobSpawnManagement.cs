using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MobSpawnManagement : MonoBehaviour
{
    public List<SpawnableEntity> SpawnableEntities = new List<SpawnableEntity>();

    protected int HighestValue = 0;

    private void Start()
    {
        for (int i = 0; i < SpawnableEntities.Count; i++)
        {
            if (SpawnableEntities[i].EntityThreat > HighestValue)
            {
                HighestValue = SpawnableEntities[i].EntityThreat;
            }
        }
    }

    public List<GameObject> GenerateEncounter(int RoomCount)
    {
        List<GameObject> ChosenEntities = new List<GameObject>();

        int EntityThreat_total = Random.Range(RoomCount, (RoomCount * Random.Range(1,HighestValue)));

        var shuffled = SpawnableEntities.OrderBy(e => UnityEngine.Random.value).ToList();

        int CurrentThreat = 0;

        while (CurrentThreat < EntityThreat_total)
        {
            var viable = shuffled.Where(e => e.EntityThreat + CurrentThreat <= EntityThreat_total).ToList();
            if (viable.Count == 0)
                break;
            SpawnableEntity ChosenEntity = viable[UnityEngine.Random.Range(0, viable.Count)];
            ChosenEntities.Add(ChosenEntity.Entity);
            CurrentThreat += ChosenEntity.EntityThreat;
        }


        return ChosenEntities;
    }
}

[System.Serializable]
public struct SpawnableEntity
{
    public int EntityThreat;
    public GameObject Entity;
}