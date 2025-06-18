using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTile : MonoBehaviour
{
    public List<ConnectionNode> ExitNodes;

    public List<ConnectionNode> EntranceNodes;

    public List<Spawner> spawners = new List<Spawner>();

    public void SpawnEnemies(List<GameObject> Entities)
    {
        
        if (spawners.Count > 0)
        {
            Debug.Log("Spawning Entities");
            foreach (var e in Entities)
            {
                spawners[Random.Range(0, spawners.Count)].SpawnEntity(e);
            }
        }
        else
        {
            Debug.Log("No Spawners Found");
        }
        
    }


}

[System.Serializable]
public struct ConnectionNode
{
    public Transform AnchorPoint;

    public bool NotAvailable;

    public BoxCollider2D RoadBlockCollider;

    public BoxCollider2D BackTrackCollider;

    public void DisableWall()
    {
        if(RoadBlockCollider != null)
            RoadBlockCollider.enabled = false;
        if(BackTrackCollider != null)
            BackTrackCollider.enabled = true;
    }

}
