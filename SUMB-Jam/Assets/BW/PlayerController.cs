using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D playerRb;
    [SerializeField] private float playerSpeed;
    float movementX;
    float movementY;

    [SerializeField] GameObject attackHitBox;
    [SerializeField] private float AttackDuration;
    [SerializeField] private float AttackCooldownDuration;

    public bool attackIsCoolingDown = false;

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
}
