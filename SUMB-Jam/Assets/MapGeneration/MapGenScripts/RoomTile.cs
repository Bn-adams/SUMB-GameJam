using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTile : MonoBehaviour
{
    public List<ConnectionNode> ExitNodes;

    public List<ConnectionNode> EntranceNodes;



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
