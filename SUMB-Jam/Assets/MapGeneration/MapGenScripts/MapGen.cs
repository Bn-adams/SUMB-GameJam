using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MapGen : MonoBehaviour
{
    public List<GameObject> CurrentPeices = new List<GameObject>();

    public List<GameObject> Prefabs = new List<GameObject>();

    public List<ConnectionNode> HeaderNodes = new List<ConnectionNode>();

    public int DisplayLength = 2;

    public GameObject StartingPeice;

    private void Start()
    {
        GameObject startingPeice = Instantiate(StartingPeice);

        CurrentPeices.Add(startingPeice);

        HeaderNodes.Add(startingPeice.GetComponent<RoomTile>().ExitNodes[0]);

        GameObject NewPrefab = Instantiate(Prefabs[Random.Range(0, Prefabs.Count)]);
        AttachPeiceNoDetection(NewPrefab, HeaderNodes[0]);

        HeaderNodes.RemoveAt(0);
    }

    private void Update()
    {
        Vector3 PlayerLocation = GameObject.Find("Drone").transform.position;
        for(int i = 0; i < HeaderNodes.Count; i++)
        {

            if (Vector3.Distance(HeaderNodes[i].AnchorPoint.position, PlayerLocation) < 2f)
            {
                ConnectionNode CurrentHead = HeaderNodes[i];

                GameObject gameObject = CurrentPeices[0];
                List<ConnectionNode> Tail_ExitNodes = gameObject.GetComponent<RoomTile>().ExitNodes;
                foreach (ConnectionNode ExitNode in Tail_ExitNodes)
                {
                    HeaderNodes.Remove(ExitNode);
                }
                List<ConnectionNode> Head_ExitNodes = HeaderNodes[i].AnchorPoint.parent.GetComponent<RoomTile>().ExitNodes;
                foreach (ConnectionNode Node in Head_ExitNodes)
                {
                    HeaderNodes.Remove(Node);
                }

                CurrentPeices.RemoveAt(0);
                Destroy(gameObject);
                GameObject NewPrefab = Instantiate(Prefabs[Random.Range(0, Prefabs.Count)]);
                AttachPeiceNoDetection(NewPrefab, CurrentHead);
                
            }
        }
    }

    void AttachPeiceNoDetection(GameObject NewPrefab, ConnectionNode ExitNode)
    {
        Transform EntrancePoint = NewPrefab.GetComponent<RoomTile>().EntranceNodes[0].AnchorPoint;

        Vector2 entranceDir = EntrancePoint.up;
        Vector2 exitDir = ExitNode.AnchorPoint.up;

        float angle = Vector2.SignedAngle(entranceDir, exitDir);

        NewPrefab.transform.Rotate(0, 0, angle);

        Vector3 EntranceLocation = EntrancePoint.position;
        Vector3 targetLocation = ExitNode.AnchorPoint.position;

        Vector3 Offset = targetLocation - EntranceLocation;

        NewPrefab.transform.position += Offset;

        ExitNode.NotAvailable = true;

        CurrentPeices.Add(NewPrefab);

        HeaderNodes.Remove(ExitNode);

        HeaderNodes.AddRange(NewPrefab.GetComponent<RoomTile>().ExitNodes);


    }
}
