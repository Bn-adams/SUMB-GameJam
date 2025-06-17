using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngryCrowd : MonoBehaviour
{
    public Transform target;
    public float moveSpeed = 3f;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (target == null) return;

        Vector2 direction = (target.position - transform.position).normalized;
        rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);

        if(Vector2.Distance(target.position, transform.position) < 25f)
        {
            Debug.Log("You Lose");
        }

    }
}
