using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaPlayerController : MonoBehaviour
{
    private Rigidbody2D playerRb;
    [SerializeField] private float playerSpeed;
    float movementX;
    float movementY;

    public Animator Animator;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
        Movement();
        SetAni();
        //flip();
    }

    private void HandleInput()
    {
        movementX = Input.GetAxisRaw("Horizontal");
        movementY = Input.GetAxisRaw("Vertical");
    }

    private void Movement()
    {
        playerRb.velocity = new Vector2(movementX, movementY).normalized * playerSpeed;
    }

    private void Attack()
    {

    }

    private void PlayerSpriteDirection(Vector2 mousePosition)
    {

    }

    public void SetAni()
    {
        if (Input.GetKey(KeyCode.A))
        {
            Animator.SetBool("IsRunningL", true);
        }
        else
        {
            Animator.SetBool("IsRunningL", false);
        }

        if (Input.GetKey(KeyCode.D))
        {
            Animator.SetBool("IsRunningR", true);
        }
        else
        {
            Animator.SetBool("IsRunningR", false);
        }



    }
    //public void flip()
    //{
    //    float moveInput = Input.GetAxisRaw("Horizontal");

    //    if (moveInput > 0)
    //        transform.localScale = new Vector3(1, 1, 1); // face right
    //    else if (moveInput < 0)
    //        transform.localScale = new Vector3(-1, 1, 1); // face left
    //}
}
