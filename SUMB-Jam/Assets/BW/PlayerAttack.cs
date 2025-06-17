using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public BaseMobAi baseMobAi;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
