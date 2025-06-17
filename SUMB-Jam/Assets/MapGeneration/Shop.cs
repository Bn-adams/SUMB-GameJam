using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Shop : MonoBehaviour
{

    protected ShopMenu shopMenu;

    protected int seed = 123;
    private System.Random rng;


    protected List<Shelf> shelves = new List<Shelf>()
    {
        new Shelf() { name = "Sabre Damage", description = "Increases the damage of your Sabre Attacks.", icon = "Missing", price = 5, price_up = 5, stock = 1, max_stock = 1, Shelf_Chance= 10, upgrade = new SabreDmgUpgrade()},
        new Shelf() { name = "Sabre Attack Speed", description = "Increases the Attack Speed of your Sabre Attacks.", icon = "Missing", price = 10, price_up = 10 , stock = 1, max_stock = 2, Shelf_Chance= 10, upgrade = new SabreASUpgrade()},
        new Shelf() { name = "Health", description = "Refils Missing Health.", icon = "Missing", price = 5, price_up = 1, stock = 1, max_stock = 3, Shelf_Chance= 25, upgrade = new HealthUpgrade()},
        new Shelf() { name = "Health Up", description = "Increases Max Health and Health.", icon = "Missing", price = 10, price_up = 10, stock = 1, max_stock = 2, Shelf_Chance= 10, upgrade = new MaxHealthUpgrade()},
        new Shelf() { name = "Musket Ammo", description = "Provides Another bullet for your musket.", icon = "Missing", price = 6, price_up = 0, stock = 2, max_stock = 6, Shelf_Chance= 15, upgrade = new MusketAmmo()},
        new Shelf() { name = "Pistol Ammo", description = "Provides Another bullet for your psitol.", icon = "Missing", price = 3, price_up = 0, stock = 4, max_stock = 14, Shelf_Chance= 15, upgrade = new PistolAmmo()},
    };

    private void Start()
    {
        seed = Random.Range(0, 500);
        rng = new System.Random(seed);
        shopMenu = GameObject.Find("ShopMenu").GetComponent<ShopMenu>();
        List<Shelf> ChosenShelves = new List<Shelf>();
        List<Shelf> WorkingPool = new List<Shelf>();
        WorkingPool.AddRange(shelves);
        
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

    void OnMouseDown()
    {
        OpenShopMenu();
    }

    void OpenShopMenu()
    {
        shopMenu.OpenShopMenu();
    }
}

public class Upgrade
{
    public virtual void DoUpgrade()
    {

    }
}

public class SabreDmgUpgrade : Upgrade
{
    public override void DoUpgrade()
    {
        Debug.Log("Do SabreDmgUpgrade!");
    }
}

public class SabreASUpgrade : Upgrade
{
    public override void DoUpgrade()
    {
        Debug.Log("Do SabreASUpgrade!");
    }
}

public class HealthUpgrade : Upgrade
{
    public override void DoUpgrade()
    {
        Debug.Log("Do Health Up!");
    }
}

public class MaxHealthUpgrade : Upgrade
{
    public override void DoUpgrade()
    {
        Debug.Log("Do Max Health Up!");
    }
}

public class MusketAmmo : Upgrade
{
    public override void DoUpgrade()
    {
        Debug.Log("Do Musket Ammo Up!");
    }
}

public class PistolAmmo : Upgrade
{
    public override void DoUpgrade()
    {
        Debug.Log("Do Pistol Ammo Up!");
    }
}

public struct Shelf
{

    public string name;

    public string description;

    public string icon;

    public int price;

    public int price_up;

    public int stock;

    public int max_stock;

    public int Shelf_Chance;

    public Upgrade upgrade;
}