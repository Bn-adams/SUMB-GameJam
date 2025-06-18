using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Spawner : MonoBehaviour
{

    protected int maxAttempts = 15;
    public Collider2D spawnArea;

    public void SpawnEntities(List<GameObject> gameObjects)
    {
        foreach (GameObject obj in gameObjects)
        {
            Instantiate(obj,GetRandomPointOnNavMesh(),Quaternion.identity);
        }
    }

    public void SpawnEntity(GameObject entity)
    {
        Instantiate(entity, GetRandomPointOnNavMesh(),Quaternion.identity);
    }

    Vector3 GetRandomPointOnNavMesh()
    {
        for (int i = 0; i < maxAttempts; i++)
        {
            Vector2 randomPoint = (Vector2)(spawnArea.bounds.center) + Random.insideUnitCircle * spawnArea.bounds.size;
            randomPoint = spawnArea.ClosestPoint(randomPoint);
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 1f, NavMesh.AllAreas))
            {
                return hit.position;
            }
        }

        Debug.LogWarning("Failed to find valid NavMesh position after multiple attempts.");
        return transform.position; // fallback
    }

}
