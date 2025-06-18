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

    public GameObject aimer;
    GameObject atk;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] SpriteRenderer TheStrap;
    [SerializeField] GameObject projectile;
    [SerializeField] GameObject gunBase;
    [SerializeField] GameObject reticle;





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
    }

    private void HandleInput()
    {
        movementX = Input.GetAxisRaw("Horizontal");
        movementY = Input.GetAxisRaw("Vertical");

        if (Input.GetButtonDown("Fire1"))
        {
            Attack();
        }

        if(Input.GetMouseButtonDown(1))
        {
            GUN();
        }
    }

    private void Movement()
    {
        playerRb.velocity = new Vector2(movementX, movementY).normalized * playerSpeed;

        SetMoveAni();
    }

    private void GUN()
    {
        var x = Instantiate(projectile, gunBase.transform.position,gunBase.transform.rotation);
        x.GetComponent<PlayerProjectile>().target = reticle;
    }

    private void Attack()
    {
        if (!attackIsCoolingDown)
        {
            Vector3 MousePos = Input.mousePosition;
            atk = GameObject.Instantiate(attackHitBox, this.transform.position, Quaternion.Euler(0,0,aimer.transform.eulerAngles.z));
            PlayerAttack pA = atk.GetComponent<PlayerAttack>();
            pA.player = this.gameObject;
            //Debug.Log("Start of attack!");
            animator.SetBool("IsRunning", false);
            animator.SetBool("IsAttacking", true);
            StartCoroutine(Attacking());
        }
    }

    private void PlayerSpriteDirection(Vector2 mousePosition)
    {

    }

    private IEnumerator Attacking()
    {
        yield return new WaitForSeconds(AttackDuration);
        GameObject.Destroy(atk);
        //Debug.Log("End of attack!");
        animator.SetBool("IsAttacking", false);
        StartCoroutine(AttackCooldown());
    }

    private IEnumerator AttackCooldown()
    {
        attackIsCoolingDown = true;
        yield return new WaitForSeconds(AttackCooldownDuration);
        attackIsCoolingDown = false;

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

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        spriteRenderer.flipX = mousePos.x > transform.position.x;
        TheStrap.flipY = mousePos.x > transform.position.x;
    }
}
