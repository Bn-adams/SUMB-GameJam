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

    protected float Offset;

    public float Offset_amount;

    int FullPtr = 0;

    public void SetHealth(int Hearts, int Max_Hearts)
    {
        while(DisplayedHearts.Count > Max_Hearts && Max_Hearts >= 0)
        {
            GameObject HeartContainer = DisplayedHearts[DisplayedHearts.Count - 1];
            DisplayedHearts.Remove(HeartContainer);
            Destroy(HeartContainer);
            Offset -= Offset_amount;
        }
        while(DisplayedHearts.Count < Max_Hearts && Max_Hearts >= 0)
        {
            GameObject NewHeartContainer = Instantiate(HeartContainer, this.transform);
            Vector3 NHC_pos = new Vector3(NewHeartContainer.transform.position.x + Offset, NewHeartContainer.transform.position.y, NewHeartContainer.transform.position.z);
            NewHeartContainer.transform.position = NHC_pos;
            DisplayedHearts.Add(NewHeartContainer);
            Offset += Offset_amount;
        }
        while(FullPtr < Hearts && Hearts >= 0 && Max_Hearts >= 0)
        {
            GameObject LastEmptyHeartInRow = DisplayedHearts[FullPtr];
            FullPtr++;
            LastEmptyHeartInRow.GetComponent<Image>().sprite = FullHeartContainerSprite;
        }
        while (FullPtr > Hearts && Hearts >= 0 && Max_Hearts >= 0)
        {
            FullPtr--;
            GameObject LastFullHeartInRow = DisplayedHearts[FullPtr];

            LastFullHeartInRow.GetComponent<Image>().sprite = EmptyHeartContainerSprite;
            
        }
    }

}
