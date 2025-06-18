using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public GameObject target;
    Vector3 aim;

    public float speed;
    public float PlayerPredictionFactor;

    public float lifetime = 1f;

    private void Start()
    {
        //speed = 10;
        
    }

    void Update()
    {
        transform.position += aim * Time.deltaTime * speed;
        if(lifetime <= 0f)
        {
            Destroy(this.gameObject);
        }
        else
        {
            lifetime -= Time.deltaTime;
        }
    }

    public void takeAim()
    {

        Rigidbody2D rb = target.GetComponent<Rigidbody2D>();
        Vector3 direction = (target.transform.position - transform.position).normalized;
        Vector3 playerVelocity = new Vector3(rb.velocity.x, rb.velocity.y, 0);
        aim = (playerVelocity * PlayerPredictionFactor) + direction;
    }

    void OnCollisionEnter2D(Collision2D other)
    {

        if(other.gameObject.tag == target.tag)
        {
            Debug.Log("Projectile: TARGET HIT");
        }
        else
        {
            Debug.Log("Projectile: NOT TARGET HIT");
        }
            Destroy(this.gameObject);
    }
}
