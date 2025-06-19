using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;
using static UnityEngine.GraphicsBuffer;

public class BaseMobAi : MonoBehaviour
{
    [SerializeField] int H = 5;
    [SerializeField] int AS = 2000;
    [SerializeField] float MS = 5f;
    [SerializeField] float AR = 1f;
    [SerializeField] float VR = 3f;

    public bool isOnCoolDown;
    public bool isRanged; 
    public NavMeshAgent agent;
    public BlackBoard bb;
    public GameObject projectile;
    public GameObject WeaponSlash;
    public GameObject PickupPrefab;

    [HideInInspector] public int health = 5;
    [HideInInspector] public int attackSpeed;
    [HideInInspector] public float attackRange;
    [HideInInspector] public float moveSpeed;
    [HideInInspector] public bool reloading;

    [SerializeField] public Animator anim;
    [SerializeField] public SpriteRenderer spriteRenderer;
    

    private Node root;


    CircleCollider2D cdr; 


    void Start()
    {
        Delayer(200);

        agent = GetComponent<NavMeshAgent>();
        cdr = GetComponent<CircleCollider2D>();
        bb = FindAnyObjectByType<BlackBoard>();

        agent.updateUpAxis = false;
        agent.updateRotation = false;

        health = H;

        // The MS between each attack
        attackSpeed = AS;
        attackRange = AR;
        moveSpeed = MS;
        agent.speed = 0;
        isOnCoolDown = false;
        reloading = false;


        //root node, we begin 
        SelectorNode rootSelector = new SelectorNode(bb);
        root = rootSelector;

        //first selector for checking own health
        SelectorNode healthCheck = new SelectorNode(bb);
        rootSelector.AddChildClass(healthCheck);

        //first node, fails if healthy, success and destroy if not 
        CheckHealth checkHealth = new CheckHealth(bb, this);
        healthCheck.AddChildClass(checkHealth);

        //range detection selector
        SelectorNode detectionRangeCheck = new SelectorNode(bb);
        healthCheck.AddChildClass(detectionRangeCheck);

        //checks the range to the player for detection purposes, fails if player in range 
        CheckDetectionRange checkDetectionRange = new CheckDetectionRange(this, bb, VR);
        detectionRangeCheck.AddChildClass(checkDetectionRange);

        //sequence containing move to and check range 
        SequenceNode moveThenCheck = new SequenceNode(bb);
        detectionRangeCheck.AddChildClass(moveThenCheck);

        //the move node sets the players destination to the player
        MoveToPlayer move = new MoveToPlayer(this, bb);
        moveThenCheck.AddChildClass(move);

        //range check selector 
        SelectorNode attackRangeCheck = new SelectorNode(bb);
        moveThenCheck.AddChildClass(attackRangeCheck);

        //checks attack range 
        CheckDetectionRange checkattackRange = new CheckDetectionRange(this, bb, attackRange);
        attackRangeCheck.AddChildClass(checkattackRange);

        //checks if ranged attack
        SelectorNode checkIsRanged = new SelectorNode(bb);
        attackRangeCheck.AddChildClass(checkIsRanged);

        //attack action
        AttackPlayer attackPlayer = new AttackPlayer(this, bb);
        checkIsRanged.AddChildClass(attackPlayer);

        //Ranged attack
        RangedAttack rangedAttack = new RangedAttack(this, bb);
        checkIsRanged.AddChildClass(rangedAttack);

        InvokeRepeating("StartTree", 0f, 0.5f);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        GameObject.Find("SoundManager").GetComponent<SoundManager>().SpawnSoundPrefab(transform.position, "DamageTaken");
        

        //Debug.Log("Enemy takes "+damage+" damage");
        if (health <= 0)
        {

            if (UnityEngine.Random.Range(0, 100) > 50)
            {
                Instantiate(PickupPrefab, transform.position, Quaternion.identity);
            }
            Destroy(this.gameObject);
        }
        else
        {
            if (UnityEngine.Random.Range(0, 100) > 15)
            {
                GameObject.Find("SoundManager").GetComponent<SoundManager>().SpawnSoundPrefab(transform.position, "EnemyHitVL");
            }

            moveSpeed = MS / 10f;
            agent.speed = moveSpeed;
            spriteRenderer.color = Color.red;
            StartCoroutine(StopBeingRedAfter(0.2f));
        }
    }

    public IEnumerator StopBeingRedAfter(float time)
    {
        yield return new WaitForSeconds(time);
        spriteRenderer.color = Color.white;
        moveSpeed = MS;
        agent.speed = moveSpeed;
    }


    public int getHealth()
    {
        return health;
    }

    public float getMoveSpeed()
    {
        return moveSpeed;
    }

    public int getAttackSpeed()
    {
        return attackSpeed;
    }

    public bool IsRanged() 
    {  
        return isRanged;
    }

    public bool IsReloading()
    {
        return reloading;
    }

    public void decrementHealth()
    {
        health--; 
    }

    public void StartTree()
    {
        root.Execute();
    }

    private async void Delayer(int timer)
    {
        await Task.Delay(timer);
    }

    private async void AniDelayer(int timer)
    {
        await Task.Delay(timer);
        if (anim != null)
        {
            anim.SetBool("IsEAttacking", false);
        }
        //anim.SetBool("IsERunning", true);
    }

    public async void RangedCooldown(int timer)
    {
        await Task.Delay(timer);
        isOnCoolDown = false;
        reloading = false;
    }

    public async void meleeCooldown(int timer)
    {
        await Task.Delay(timer);
        isOnCoolDown = false;
    }

    void Update()
    {
        agent.SetDestination(bb.player.transform.position);
        float direction = bb.player.transform.position.x - transform.position.x;
        if (direction < 0)
            spriteRenderer.flipX = false;
        else if (direction > 0)
            spriteRenderer.flipX = true;
    }

    public void FireProjectile()
    {
        var x = GameObject.Instantiate(projectile,this.transform.position,Quaternion.identity);
        x.GetComponent<Projectile>().target = bb.player;
        x.GetComponent<Projectile>().takeAim();
    }

    public void SlashAttack()
    {
        AniDelayer(500);
        var x = GameObject.Instantiate(WeaponSlash, this.transform.position, Quaternion.identity);
        x.GetComponent<Projectile>().target = bb.player;
        Vector2 direction = (bb.player.transform.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        x.transform.rotation = Quaternion.Euler(0, 0, angle);
        x.GetComponent<Projectile>().takeAim();

    }

}

public class CheckHealth : Node
{
    private BaseMobAi me;

    public CheckHealth(BlackBoard bb,BaseMobAi ai) : base(bb)
    {
        me = ai;
    }

    public override result Execute()
    {
        if(me.getHealth() <= 0)
        {
            GameObject.Destroy(me.gameObject);
            return result.Success;
        }
        else
        {
            return result.Failure;
        }
    }
}

public class CheckDetectionRange : Node
{
    private BaseMobAi me;
    private BlackBoard bb;
    private float distanceBetween;
    private float threshold; 

    public CheckDetectionRange(BaseMobAi me, BlackBoard bb, float x) : base(bb) 
    {
        this.me = me;
        this.bb = bb;
        threshold = x;
    }

    public override result Execute()
    {
        distanceBetween = Mathf.Abs(bb.player.transform.position.magnitude - me.transform.position.magnitude);
        if (distanceBetween > 100f)
        {
            GameObject.Destroy(me.gameObject);
        }

        if (distanceBetween > threshold)
        {
            return result.Success;
        }
        else
        {
            return result.Failure;
        }
        
    }
}

public class MoveToPlayer : Node
{
    private BaseMobAi me;
    private BlackBoard bb;

    public MoveToPlayer(BaseMobAi me, BlackBoard bb) : base(bb)
    {
        this.me = me;
        this.bb = bb;
    }

    public override result Execute()
    {


        if (me.IsReloading() == false)
        {
            me.anim.SetBool("IsERunning", true); 
            me.agent.speed = me.getMoveSpeed();
            return result.Success;
        }
        else
        {
            return result.Success;
        }
    }


}

public class AttackPlayer : Node
{
    private BaseMobAi me;
    private BlackBoard bb;

    public AttackPlayer(BaseMobAi me, BlackBoard bb) : base(bb)
    {
        this.me = me;
        this.bb = bb;
    }

    public override result Execute()
    {
        if (me.IsRanged() == false)
        {
            if (me.isOnCoolDown == false)
            {
                if (UnityEngine.Random.Range(0, 100) > 80)
                {
                    GameObject.Find("SoundManager").GetComponent<SoundManager>().SpawnSoundPrefab(me.transform.position, "EnemyAttackVL");
                }
                //me.anim.SetBool("IsERunning", false);
                me.anim.SetBool("IsEAttacking", true);
                me.SlashAttack();
                me.isOnCoolDown = true;
                me.reloading = true;
                me.meleeCooldown(me.getAttackSpeed());
                return result.Success;
            }
            else
            {
                return result.Failure;
            }
        }
        else
        {
            return result.Failure;
        }
    }
}


public class RangedAttack : Node
{
    private BaseMobAi me;
    private BlackBoard bb;

    public RangedAttack(BaseMobAi me, BlackBoard bb) : base(bb)
    {
        this.me = me;
        this.bb = bb;
    }

    public override result Execute()
    {
        if(me.isOnCoolDown == false)
        {
            if (UnityEngine.Random.Range(0, 100) > 80)
            {
                GameObject.Find("SoundManager").GetComponent<SoundManager>().SpawnSoundPrefab(me.transform.position, "EnemyAttackVL");
            }

            me.FireProjectile();
            me.isOnCoolDown = true;
            me.reloading = true;
            me.RangedCooldown(me.getAttackSpeed());
            return result.Success;
        }
        else
        {
            return result.Failure;
        }
    }

}


