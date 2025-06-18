using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pc : MonoBehaviour
{
    private Rigidbody2D playerRb;
    [SerializeField] private float playerSpeed;
    float movementX;
    float movementY;

    [SerializeField] private float SabreAttackCooldownDuration;



    //Animation varibles
    public Animator animator;
    private bool IsFacingLeft;
    public bool attackIsCoolingDown = false;

    public GameObject aimer;
    GameObject atk;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] SpriteRenderer TheStrap;
    [SerializeField] List<GameObject> projectile = new List<GameObject>();
    [SerializeField] GameObject gunBase;
    [SerializeField] GameObject reticle;



    // ScalingStats
    // Utiltity
    public int Gold = 0;
    public int GoldMulti = 1;
    public int MovementSpeed = 1;
    // Sabre
    public int SabreProjDamage = 1;
    public int SabreAS = 1;
    public float SabreProjSize = 1f;
    // Pistol
    public int PistolProjDamage = 1;
    public int PistolReloadDuration = 1;
    public float PistolProjSize = 1f;
    public bool Reload = false;
    public int Ammo = 3;
    public int MaxAmmo = 3;
    public float ReloadDuration = 3f;
    // Defense
    public int Health = 3;
    public int MaxHealth = 3;

    protected bool Invincible;



    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        IsFacingLeft = true;
        UpdateStats();
        
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
        if (Reload != true)
        {
            Ammo--;
            UpdateHUD();
            var x = Instantiate(projectile[0], gunBase.transform.position, gunBase.transform.rotation);
            x.GetComponent<PlayerProjectile>().target = reticle;
            x.GetComponent<PlayerProjectile>().Damage = PistolProjDamage;
            x.transform.localScale = x.transform.localScale * PistolProjSize;
        }
        if (Ammo <= 0 && Reload == false)
        {
            Reload = true;
            StartCoroutine(GUNReload());
        }
    }

    private void Attack()
    {
        if (!attackIsCoolingDown)
        {
            attackIsCoolingDown = true;
            Vector3 MousePos = Input.mousePosition;
            var x = Instantiate(projectile[1], transform.position, gunBase.transform.rotation);
            x.GetComponent<PlayerProjectile>().target = reticle;
            x.GetComponent<PlayerProjectile>().Damage = SabreProjDamage;
            x.transform.localScale = x.transform.localScale * SabreProjSize;
            //Debug.Log("Start of attack!");
            animator.SetBool("IsRunning", false);
            animator.SetBool("IsAttacking", true);
            StartCoroutine(AttackAnim());
            StartCoroutine(AttackCooldown());
        }
    }

    public IEnumerator AttackAnim()
    {
        yield return new WaitForSeconds(0.1f);
        animator.SetBool("IsRunning", true);
        animator.SetBool("IsAttacking", false);
    }

    public void UpdateStats()
    {
        SabreAttackCooldownDuration = 1f / SabreAS;
        ReloadDuration = 3f / PistolReloadDuration;
        UpdateHUD();
    }

    public void AddGold(int Amount)
    {
        Gold += Amount * GoldMulti;
        UpdateHUD();
    }

    public void UpdateHUD()
    {
        GameObject.Find("HeartBar").GetComponent<HeartBar>().SetHealth(Health,MaxHealth);
        GameObject.Find("GoldCounterText").GetComponent<Text>().text = Gold.ToString();
        GameObject.Find("CurrentAmmoText").GetComponent<Text>().text = Ammo.ToString();
        GameObject.Find("MaxAmmoText").GetComponent<Text>().text = MaxAmmo.ToString();
    }


    private IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(SabreAttackCooldownDuration);
        attackIsCoolingDown = false;

    }

    private IEnumerator GUNReload()
    {
        yield return new WaitForSeconds(ReloadDuration);
        Ammo = MaxAmmo;
        UpdateHUD();
        Reload = false;
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

    public void TakeDamage(int Amount)
    {
        if (!Invincible)
        {
            Health -= Amount;
            UpdateHUD();
            spriteRenderer.color = Color.red;
            if(Health <= 0)
            {
                ThePlayerDiedLOL();
            }
            Invincible = true;
            StartCoroutine(TempInvincible());
        }
    }

    private static void ThePlayerDiedLOL()
    {
        Debug.Log("RIP BOZO");
    }

    protected IEnumerator TempInvincible()
    {
        yield return new WaitForSeconds(1f);
        spriteRenderer.color = Color.white;
        Invincible = false;
    }

}
