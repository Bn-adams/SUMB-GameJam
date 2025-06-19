using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Shop : MonoBehaviour
{

    protected ShopMenu shopMenu;
    protected ShopManager shopManager;

    protected int seed = 123;
    private System.Random rng;

    protected bool CanOpen = false;

    protected GameObject GO_E_UI_Element;

    private void Start()
    {
        seed = Random.Range(0, 500);
        rng = new System.Random(seed);
        shopMenu = GameObject.Find("ShopMenu").GetComponent<ShopMenu>();
        shopManager = GameObject.Find("ShopManager").GetComponent<ShopManager>();

        GO_E_UI_Element = shopManager.GetTheE();

        List<Upgrade> ChosenShelves = new List<Upgrade>();
        List<Upgrade> WorkingPool = new List<Upgrade>();

        WorkingPool.AddRange(shopManager.GetPossibleStock());
        
        for (int i = 0; i < 3 && WorkingPool.Count > 0; i++)
        {
            int totalWeight = 0;
            foreach (var item in WorkingPool)
                totalWeight += item.Shelf_Chance;

            int roll = rng.Next(0, totalWeight);
            int runningTotal = 0;

            for (int j = 0; j < WorkingPool.Count; j++)
            {
                runningTotal += WorkingPool[j].Shelf_Chance;
                if (roll < runningTotal)
                {
                    ChosenShelves.Add(WorkingPool[j]);
                    WorkingPool.RemoveAt(j); // remove so we don't pick it again
                    break;
                }
            }
        }

        shopMenu.StockShelves(ChosenShelves);


    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if(CanOpen)
            {
                OpenShopMenu();
                Cursor.visible = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            if (!GO_E_UI_Element.activeInHierarchy)
                GO_E_UI_Element.SetActive(true);
            CanOpen = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if(GO_E_UI_Element.activeInHierarchy)
                GO_E_UI_Element.SetActive(false);
            CanOpen = false;
            shopMenu.CloseShopMenu();
        }
    }

    void OpenShopMenu()
    {
        shopMenu.OpenShopMenu();
    }
}

