using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartBar : MonoBehaviour
{
    public GameObject HeartContainer;
    public Sprite EmptyHeartContainerSprite;
    public Sprite FullHeartContainerSprite;

    protected List<GameObject> DisplayedHearts = new List<GameObject>();
    protected List<GameObject> DisplayedEmptyHearts = new List<GameObject>();
    protected List<GameObject> DisplayedFullHearts = new List<GameObject>();

    protected float Offset;

    public void Start()
    {
        SetHealth(3, 3);
    }

    public void SetHealth(int Hearts, int Max_Hearts)
    {
        while(DisplayedHearts.Count > Max_Hearts)
        {
            GameObject HeartContainer = DisplayedHearts[DisplayedHearts.Count - 1];
            DisplayedEmptyHearts.Remove(HeartContainer);
            DisplayedFullHearts.Remove(HeartContainer);
            DisplayedHearts.Remove(HeartContainer);
            Destroy(HeartContainer);
            Offset -= 130;
        }
        while(DisplayedHearts.Count < Max_Hearts)
        {
            GameObject NewHeartContainer = Instantiate(HeartContainer, this.transform);
            Vector3 NHC_pos = new Vector3(NewHeartContainer.transform.position.x + Offset, NewHeartContainer.transform.position.y, NewHeartContainer.transform.position.z);
            NewHeartContainer.transform.position = NHC_pos;
            DisplayedEmptyHearts.Add(NewHeartContainer);
            DisplayedHearts.Add(NewHeartContainer);
            Offset += 130;
        }
        while(DisplayedFullHearts.Count < Hearts)
        {
            GameObject LastEmptyHeartInRow = DisplayedEmptyHearts[DisplayedEmptyHearts.Count - 1];

            LastEmptyHeartInRow.GetComponent<Image>().sprite = FullHeartContainerSprite;

            DisplayedEmptyHearts.Remove(LastEmptyHeartInRow);
            DisplayedFullHearts.Add(LastEmptyHeartInRow);

            
        }
        while (DisplayedFullHearts.Count > Hearts)
        {
            GameObject LastFullHeartInRow = DisplayedFullHearts[DisplayedEmptyHearts.Count - 1];

            LastFullHeartInRow.GetComponent<Image>().sprite = EmptyHeartContainerSprite;

            DisplayedFullHearts.Remove(LastFullHeartInRow);
            DisplayedEmptyHearts.Add(LastFullHeartInRow);
        }
    }

}
