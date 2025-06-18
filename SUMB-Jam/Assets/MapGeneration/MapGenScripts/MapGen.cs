using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Unity.AI;
using UnityEngine.UI;
using UnityEngine.AI;

public class MapGen : MonoBehaviour
{
    protected List<GameObject> CurrentPeices = new List<GameObject>();

    public List<GameObject> LeftPrefabs = new List<GameObject>();

    public List<GameObject> RightPrefabs = new List<GameObject>();

    public List<GameObject> ForwardsPrefabs = new List<GameObject>();

    public List<GameObject> Prefabs = new List<GameObject>();

    protected List<ConnectionNode> HeaderNodes = new List<ConnectionNode>();

    protected int DisplayLength = 2;

    public List<GameObject> StartingPeice = new List<GameObject>();

    public List<GameObject> ShopPeice = new List<GameObject>();

    protected int DirectionPointer = 0; // 0 -Forwards, 1 -Left, 2 -Right

    protected int RoomCounter = 0;

    protected int ShopCounter = 1;

    protected int ShopCounterMax = 3;

    public NavMeshPlus.Components.NavMeshSurface NavSurface;

    public MobSpawnManagement MobSpawnManagement;

    private void Start()
    {

        Debug.Log(NavSurface.ToString());

        GameObject startingPeice = Instantiate(StartingPeice[0]);

        CurrentPeices.Add(startingPeice);

        HeaderNodes.Add(startingPeice.GetComponent<RoomTile>().ExitNodes[0]);

        AttachPeiceNoDetection(GetPeiceToAttach(DirectionPointer), HeaderNodes[0]);

        HeaderNodes.RemoveAt(0);

        RebuildNavigation();
    }

    private void RebuildNavigation()
    {
        StartCoroutine(WaitAndExecute());
    }

    IEnumerator WaitAndExecute()
    {
        yield return new WaitForSeconds(0.1f);
        NavSurface.BuildNavMesh();
        CurrentPeices[1].GetComponent<RoomTile>().SpawnEnemies(MobSpawnManagement.GenerateEncounter(RoomCounter));
    }

    private void Update()
    {
        Vector3 PlayerLocation = GameObject.Find("Drone").transform.position;
        for(int i = 0; i < HeaderNodes.Count; i++)
        {

            if (Vector3.Distance(HeaderNodes[i].AnchorPoint.position, PlayerLocation) < 3.5f)
            {
                ConnectionNode CurrentHead = HeaderNodes[i];
                Vector2 forward = CurrentHead.AnchorPoint.up;  // or transform.up
                ShopCounter--;

                float angle = Mathf.Atan2(forward.y, forward.x) * Mathf.Rad2Deg;
                if (angle >= -45f && angle <= 45f)
                    DirectionPointer = 2; //Right
                else if (angle > 45f && angle < 135f)
                    DirectionPointer = 0; //Forwards
                else if (angle <= -135f || angle >= 135f)
                    DirectionPointer = 1; //Left


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

                if (ShopCounter <= 0)
                {
                    ShopCounter = ShopCounterMax;
                    GameObject SpawnedShopPeice = Instantiate(ShopPeice[0]);
                    AttachPeiceNoDetection(SpawnedShopPeice, CurrentHead);
                }
                else
                {
                    AttachPeiceNoDetection(GetPeiceToAttach(DirectionPointer), CurrentHead);
                }
                CurrentHead.DisableWall();

                RebuildNavigation();
            }
        }
    }

    GameObject GetPeiceToAttach(int DirectionPointer)
    {
        switch (DirectionPointer)
        {
            case 0:
                int RNG = Random.Range(0, 12);
                if (RNG >= 8)
                {
                    GameObject NewPrefab = Instantiate(ForwardsPrefabs[Random.Range(0, ForwardsPrefabs.Count)]);
                    return NewPrefab;
                }
                else if (RNG >= 4)
                {
                    GameObject NewPrefab1 = Instantiate(LeftPrefabs[Random.Range(0, LeftPrefabs.Count)]);
                    return NewPrefab1;
                }
                else
                {
                    GameObject NewPrefab2 = Instantiate(RightPrefabs[Random.Range(0, RightPrefabs.Count)]);
                    return NewPrefab2;
                }
            case 1:
                int RNG2 = Random.Range(0, 8);
                if (RNG2 >= 4)
                {
                    GameObject NewPrefab3 = Instantiate(ForwardsPrefabs[Random.Range(0, ForwardsPrefabs.Count)]);
                    return NewPrefab3;
                }
                else
                {
                    GameObject NewPrefab4 = Instantiate(RightPrefabs[Random.Range(0, RightPrefabs.Count)]);
                    return NewPrefab4;
                }
            case 2:
                int RNG3 = Random.Range(0, 8);
                if (RNG3 >= 4)
                {
                    GameObject NewPrefab5 = Instantiate(ForwardsPrefabs[Random.Range(0, ForwardsPrefabs.Count)]);
                    return NewPrefab5;
                }
                else
                {
                    GameObject NewPrefab6 = Instantiate(LeftPrefabs[Random.Range(0, LeftPrefabs.Count)]);
                    return NewPrefab6;
                }
            default:
                GameObject NewPrefab7 = Instantiate(Prefabs[Random.Range(0, Prefabs.Count)]);
                return NewPrefab7;
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

        RoomCounter++;
    }
}
