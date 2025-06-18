using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public BaseMobAi baseMobAi;
    public GameObject player;
    void Start()
    {

    }

    void Update()
    {
        transform.position = player.transform.position;
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            baseMobAi = other.gameObject.GetComponent<BaseMobAi>();
            if (baseMobAi != null)
            {
                // call take damage function from mob ai script
                baseMobAi.TakeDamage(5);
            }
        }
    }
}
