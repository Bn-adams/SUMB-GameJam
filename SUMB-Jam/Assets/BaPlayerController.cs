using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaPlayerController : MonoBehaviour
{
    private Rigidbody2D playerRb;
    [SerializeField] private float playerSpeed;
    public float movementX;
    public float movementY;

    public Animator animator;

    private float lastPositionX;
    private string movementDirection;


    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();

        lastPositionX = transform.position.x;

    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
        Movement();
        SetAni();
        //flip();


        //checkDirection();
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
        //if (Input.GetKeyDown(KeyCode.A))
        //{
        //    animator.SetBool("IsRunningL", true);
        //}
        //else
        //{
        //    animator.SetBool("IsRunningL", false);
        //}

        //if (Input.GetKeyDown(KeyCode.D))
        //{
        //    animator.SetBool("IsRunningR", true);
        //}
        //else
        //{
        //    animator.SetBool("IsRunningR", false);
        //}

        if (movementX > 0)
        {
            animator.SetBool("IsRunningL", false);
            animator.SetBool("IsRunningR", true);
        }

        if (movementX == 0)
        {
            animator.SetBool("IsRunningL", false);
            animator.SetBool("IsRunningR", false);
        }

        if (movementX < 0)
        {
            animator.SetBool("IsRunningR", false);
            animator.SetBool("IsRunningL", true);
        }

        if (movementY > 0)
        {
            animator.SetBool("IsRunningL", false);
            animator.SetBool("IsRunningR", true);
        }

        if (movementY < 0)
        {
            animator.SetBool("IsRunningR", false);
            animator.SetBool("IsRunningL", true);
        }
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

    //public void checkDirection()
    //{
        

    //    float currentPositionX = transform.position.x;

    //    if (currentPositionX > lastPositionX)
    //    {
    //        movementDirection = "Right";
    //    }
    //    else if (currentPositionX < lastPositionX)
    //    {
    //        movementDirection = "Left";
    //    }
    //    else
    //    {
    //        movementDirection = "Idle";
    //    }

    //    if(movementDirection == "Right")
    //    {
    //        animator.SetBool("IsRunningL", false);
    //        animator.SetBool("IsRunningR", true);
    //    }

    //    if (movementDirection == "Left")
    //    {
    //        animator.SetBool("IsRunningR", false);
    //        animator.SetBool("IsRunningL", true);
    //    }

    //    if (movementDirection == "Idle")
    //    {
    //        animator.SetBool("IsRunningR", false);
    //        animator.SetBool("IsRunningL", false);
    //    }

    //    lastPositionX = transform.position.x;
    //}

