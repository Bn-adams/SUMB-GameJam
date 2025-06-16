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

}
