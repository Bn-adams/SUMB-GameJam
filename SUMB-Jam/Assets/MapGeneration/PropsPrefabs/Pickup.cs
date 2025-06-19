using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{

    Transform playerT;
    Pc playerControler;

    protected int seed = 123;
    private System.Random rng;

    public List<Sprite> Sprites = new List<Sprite>();
    public List<Color> SpriteAppliedColor = new List<Color>();
    public List<float> SpriteSizes = new List<float>();

    protected List<PickupPayloadBase> PickUpPayloads = new List<PickupPayloadBase>()
    {
        new BronzeCoinPickUP(){chance =100}, // Make sure to match Sprites with classes using the above Sprites class.
        new SilverCoinPickUP(){chance =25},
        new GoldCoinPickUP(){chance =5},
        new HeartPickUP(){chance =25},
        new AmmoPickUP(){chance =10},
    };

    protected PickupPayloadBase ChosenPayload;
    public SpriteRenderer Renderer;

    private void Start()
    {
        seed = Random.Range(0, 500);
        rng = new System.Random(seed);
        playerT = GameObject.Find("BlackBoard").GetComponent<BlackBoard>().player.transform;
        playerControler = playerT.GetComponent<Pc>();

        List<PickupPayloadBase> WorkingPool = new List<PickupPayloadBase>();

        WorkingPool.AddRange(PickUpPayloads);

        int ChosenID = 0;

        int totalWeight = 0;
        foreach (var item in WorkingPool)
            totalWeight += item.chance;

        int roll = rng.Next(0, totalWeight);
        int runningTotal = 0;

        for (int j = 0; j < WorkingPool.Count; j++)
        {
            runningTotal += WorkingPool[j].chance;
            if (roll < runningTotal)
            {
                ChosenPayload = WorkingPool[j];
                ChosenID = j;
                WorkingPool.RemoveAt(j); // remove so we don't pick it again
                break;
            }
        }


        Renderer.sprite = Sprites[ChosenID];
        Renderer.color = SpriteAppliedColor[ChosenID];
        Renderer.gameObject.transform.localScale = Renderer.gameObject.transform.localScale * SpriteSizes[ChosenID];
    }

    private void Update()
    {
        float Distance = Vector2.Distance(transform.position, playerT.position);
        if (Distance < 0.5f)
        {
            ChosenPayload.PickUP(playerControler);
            Destroy(this.gameObject);
        }
        else if(Distance > 20f)
        {
            Destroy(this.gameObject);
        }
    }

}

public abstract class PickupPayloadBase
{
    public int chance;

    public virtual void PickUP(Pc playerController)
    {

    }
}

public class BronzeCoinPickUP : PickupPayloadBase
{

    public override void PickUP(Pc playerController)
    {
        playerController.AddGold(Random.Range(1, 4));
    }
}

public class SilverCoinPickUP : PickupPayloadBase
{
    public override void PickUP(Pc playerController)
    {
        playerController.AddGold(Random.Range(5, 9));
    }
}

public class GoldCoinPickUP : PickupPayloadBase
{
    public override void PickUP(Pc playerController)
    {
        playerController.AddGold(Random.Range(10, 16));
    }
}

public class HeartPickUP : PickupPayloadBase
{
    public override void PickUP(Pc playerController)
    {
        playerController.Health++;
        if (playerController.Health > playerController.MaxHealth)
        {
            playerController.Health = playerController.MaxHealth;
        }
        else
        {
            playerController.UpdateHUD();
        }
        
    }
}

public class AmmoPickUP : PickupPayloadBase
{
    public override void PickUP(Pc playerController)
    {
        playerController.Total_Ammo += Random.Range(1,3);
        playerController.UpdateHUD();

    }
}

