using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopMenu : MonoBehaviour
{
    public List<ShelfDisplay> shelfDisplays = new List<ShelfDisplay>();
    public GameObject UI;
    public void StockShelves(List<Upgrade> shelves)
    {
        int ptr = 0;
        foreach(ShelfDisplay shelf in shelfDisplays)
        {
            if (ptr > shelves.Count - 1)
            {
                break;
            }
            shelf.UpdateDisplay(shelves[ptr]);
            ptr++;
        }
    }

    public void OpenShopMenu()
    {
        UI.SetActive(true);
    }

    public void CloseShopMenu()
    {
        UI.SetActive(false);
    }
}
