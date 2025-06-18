using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{

    Transform playerT;

    public List<Sprite> Sprites = new List<Sprite>();
    public List<Color> SpriteAppliedColor = new List<Color>();

    protected List<PickupPayloadBase> PickUpPayloads = new List<PickupPayloadBase>()
    {
        new CoinPickUP(), // Make sure to match Sprites with classes using the above Sprites class.
    };

    protected PickupPayloadBase ChosenPayload;
    public SpriteRenderer Renderer;

    private void Start()
    {
        playerT = GameObject.Find("BlackBoard").GetComponent<BlackBoard>().player.transform;
        int RNG_Value = Random.Range(0, PickUpPayloads.Count);
        Renderer.sprite = Sprites[RNG_Value];
        Renderer.color = SpriteAppliedColor[RNG_Value];
        ChosenPayload = PickUpPayloads[RNG_Value];
    }

    private void Update()
    {
        float Distance = Vector2.Distance(transform.position, playerT.position);
        if (Distance < 2f)
        {
            ChosenPayload.PickUP();
            Destroy(this.gameObject);
        }
        else if(Distance > 20f)
        {
            Destroy(this.gameObject);
        }
    }

}

public class PickupPayloadBase
{
    public virtual void PickUP(/* Player Class */)
    {

    }
}

public class CoinPickUP : PickupPayloadBase
{
    public override void PickUP()
    {

    }
}
