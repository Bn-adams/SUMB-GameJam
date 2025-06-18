using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public GameObject target;
    Vector3 aim;

    public int speed;


    private void Start()
    {
        //speed = 10;
        takeAim();
    }

    void Update()
    {
        transform.position += aim * Time.deltaTime * speed;
    }

    void takeAim()
    {

        Rigidbody2D rb = target.GetComponent<Rigidbody2D>();

        Vector3 playerVelocity = new Vector3(rb.velocity.x, rb.velocity.y, 0);
        aim = playerVelocity + target.transform.position;
    }
}
