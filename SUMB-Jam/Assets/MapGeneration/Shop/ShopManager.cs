using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    protected List<Upgrade> shelves = new List<Upgrade>()
    {
        new SabreDmgUpgrade() { name = "Sabre Damage", description = "Increases the damage of your Sabre Attacks.", icon = "Missing", price = 5, price_up = 5, stock = 1, max_stock = 1, Shelf_Chance= 15},
        new SabreASUpgrade() { name = "Sabre Attack Speed", description = "Increases the Attack Speed of your Sabre Attacks.", icon = "Missing", price = 5, price_up = 5 , stock = 1, max_stock = 2, Shelf_Chance= 10},
        new HealthUpgrade() { name = "Health", description = "Refils Missing Health.", icon = "Missing", price = 3, price_up = 1, stock = 1, max_stock = 3, Shelf_Chance= 25},
        new MaxHealthUpgrade() { name = "Health Up", description = "Increases Max Health and Health.", icon = "Missing", price = 5, price_up = 5, stock = 1, max_stock = 2, Shelf_Chance= 10},
        new PistolDamage() { name = "Pistol Damage", description = "Increases the Damage of your Pistol Attacks.", icon = "Missing", price = 5, price_up = 5, stock = 2, max_stock = 2, Shelf_Chance= 10},
        new PistolReloadSpeed() { name = "Pistol Reload Rate", description = "Decreases the time spent reloading your Pistol.", icon = "Missing", price = 5, price_up = 5, stock = 2, max_stock = 2, Shelf_Chance= 10},
        new PistolMaxAmmo() { name = "Pistol Mag Up", description = "Adds another bullet to your Pistol magazine.", icon = "Missing", price = 3, price_up = 5, stock = 2, max_stock = 2, Shelf_Chance= 15},
        new PistolAmmo() { name = "Pistol Ammo x1", description = "Provides Another bullet for your Pistol.", icon = "Missing", price = 4, price_up = 0, stock = 2, max_stock = 2, Shelf_Chance= 35},
    };

    public Pc PlayerController;

    public GameObject THE_E;

    private void Start()
    {
        THE_E = GameObject.Find("E");
        THE_E.SetActive(false);
    }

    public List<Upgrade> GetPossibleStock()
    {
        
        return shelves;
    }

    public GameObject GetTheE()
    {
        return THE_E;
        
    }

}

public class SabreDmgUpgrade : Upgrade
{
    public override void DoUpgrade()
    {
        GameObject.Find("Player").GetComponent<Pc>().SabreProjDamage++;
        base.DoUpgrade();
    }
}

public class SabreASUpgrade : Upgrade
{
    public override void DoUpgrade()
    {
        GameObject.Find("Player").GetComponent<Pc>().SabreAS++;
        base.DoUpgrade();
    }
    
}

public class HealthUpgrade : Upgrade
{
    public override void DoUpgrade()
    {
        Pc playerController = GameObject.Find("Player").GetComponent<Pc>();
        if(playerController.Health + 1 > playerController.MaxHealth)
        {
            return;
        }
        else
        {
            playerController.Health += 1;
        }
        base.DoUpgrade();
    }
}

public class MaxHealthUpgrade : Upgrade
{
    public override void DoUpgrade()
    {
        Pc playerController = GameObject.Find("Player").GetComponent<Pc>();
        playerController.MaxHealth++;
        base.DoUpgrade();
    }
}

public class PistolMaxAmmo : Upgrade
{
    public override void DoUpgrade()
    {
        Pc playerController = GameObject.Find("Player").GetComponent<Pc>();
        playerController.MaxAmmo++;
        base.DoUpgrade();
    }
}

public class PistolAmmo : Upgrade
{
    public override void DoUpgrade()
    {
        Pc playerController = GameObject.Find("Player").GetComponent<Pc>();
        playerController.Total_Ammo++;
        base.DoUpgrade();
    }
}

public class PistolDamage : Upgrade
{
    public override void DoUpgrade()
    {
        Pc playerController = GameObject.Find("Player").GetComponent<Pc>();
        playerController.PistolProjDamage++;
        base.DoUpgrade();
    }
}

public class PistolReloadSpeed : Upgrade
{
    public override void DoUpgrade()
    {
        Pc playerController = GameObject.Find("Player").GetComponent<Pc>();
        playerController.PistolReloadDuration++;
        base.DoUpgrade();
    }
}

public class Upgrade
{

    public string name;

    public string description;

    public string icon;

    public int price;

    public int price_up;

    public int stock;

    public int max_stock;

    public int Shelf_Chance;

    public virtual void DoUpgrade()
    {
        Pc playerController = GameObject.Find("Player").GetComponent<Pc>();
        playerController.Gold -= price;
        price += price_up;
        playerController.UpdateStats();
    }
}