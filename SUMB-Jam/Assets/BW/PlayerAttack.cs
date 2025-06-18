using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public int Damage = 0;

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            BaseMobAi baseMobAi = other.gameObject.GetComponent<BaseMobAi>();
            if (baseMobAi != null)
            {
                // call take damage function from mob ai script
                baseMobAi.TakeDamage(Damage);
            }
        }
    }
}
