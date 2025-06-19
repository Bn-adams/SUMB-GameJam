using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System.Threading.Tasks;

public class PlayerProjectile : MonoBehaviour
{
    public GameObject target;
    Vector3 x;
    public float speed;
    public float lifetime;
    public int Damage = 1;

    public bool ThisIsSword;

    private void Start()
    {
        x = target.transform.position; 
        x = (x - transform.position).normalized;
    }

    void Update()
    {
        transform.position += x * Time.deltaTime * speed;
        if (lifetime <= 0f)
        {
            Destroy(this.gameObject);
        }
        else
        {
            lifetime -= Time.deltaTime;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {

        if (other.gameObject.tag == "Enemy")
        {
            if (other.gameObject.GetComponent<BaseMobAi>() != null)
            {
                other.gameObject.GetComponent<BaseMobAi>().TakeDamage(Damage);
            }
            if (!ThisIsSword)
                Destroy(this.gameObject);
        }
        else if (other.gameObject.tag == "Projectile")
        {
            Debug.Log("Hit Enemy projectile");
            if (!ThisIsSword)
                Destroy(this.gameObject);
        }
        else
        {
            Debug.Log("Projectile: NOT TARGET HIT");
            Destroy(this.gameObject);
        }
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.tag == "Enemy")
        {
            if (other.gameObject.GetComponent<BaseMobAi>() != null)
            {
                other.gameObject.GetComponent<BaseMobAi>().TakeDamage(Damage);
            }
            if (!ThisIsSword)
                Destroy(this.gameObject);
        }
        else if(other.gameObject.tag == "Projectile")
        {
            Debug.Log("Hit Enemy projectile");
            if (!ThisIsSword)
                Destroy(this.gameObject);
        }
        else
        {
            Debug.Log("Projectile: NOT TARGET HIT");
            Destroy(this.gameObject);
        }

    }

}
