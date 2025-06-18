using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pc : MonoBehaviour
{
    private Rigidbody2D playerRb;
    [SerializeField] private float playerSpeed;
    float movementX;
    float movementY;

    [SerializeField] GameObject attackHitBox;
    [SerializeField] private float AttackDuration;
    [SerializeField] private float AttackCooldownDuration;

    //Animation varibles

    public Animator animator;
    private bool IsFacingLeft;
    //

    public bool attackIsCoolingDown = false;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        IsFacingLeft = true;

    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
        Movement();
        SetAni();
    }

    private void HandleInput()
    {
        movementX = Input.GetAxisRaw("Horizontal");
        movementY = Input.GetAxisRaw("Vertical");

        if (Input.GetButtonDown("Fire1"))
        {
            Attack();
        }
    }

    private void Movement()
    {
        playerRb.velocity = new Vector2(movementX, movementY).normalized * playerSpeed;
        SetMoveAni();
    }

    private void Attack()
    {
        if (!attackIsCoolingDown)
        {
            attackHitBox.SetActive(true);
            Debug.Log("Start of attack!");
            StartCoroutine(Attacking());
        }
    }

    private void PlayerSpriteDirection(Vector2 mousePosition)
    {

    }

    private IEnumerator Attacking()
    {
        yield return new WaitForSeconds(AttackDuration);
        attackHitBox.SetActive(false);
        Debug.Log("End of attack!");
        StartCoroutine(AttackCooldown());
    }

    private IEnumerator AttackCooldown()
    {
        attackIsCoolingDown = true;
        yield return new WaitForSeconds(AttackCooldownDuration);
        attackIsCoolingDown = false;

    }


    private void Flip()
    {
        IsFacingLeft = !IsFacingLeft;
        Vector2 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }

    public void SetMoveAni()
    {
        if ((movementX != 0) || (movementY != 0))
        {
            animator.SetBool("IsRunning", true);
        }
        else
        {
            animator.SetBool("IsRunning", false);
        }

        if (IsFacingLeft && movementX > 0)
        {
            Flip();
        }
        else if (!IsFacingLeft && movementX < 0)
        {
            Flip();
        }

        if (IsFacingLeft && movementY > 0)
        {
            Flip();
        }
        else if (!IsFacingLeft && movementY < 0)
        {
            Flip();
        }

    }
    public void SetAni()
    {
        Debug.Log("HitVoid");
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetBool("IsAttacking", true);
            Debug.Log("Hitting");
        }
        else if (Input.GetMouseButtonUp(0))
        {
            animator.SetBool("IsAttacking", false);
        }

    }
}
