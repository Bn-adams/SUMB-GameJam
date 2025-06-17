using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

public class BaseMobAi : MonoBehaviour
{
    public int H = 5;
    public int AS = 2000;
    public float MS = 5f;
    public float AR = 1f;
    public float VR = 3f;

    public bool isOnCoolDown;
    public bool isRanged; 
    public NavMeshAgent agent;
    public BlackBoard bb;
    public GameObject projectile;


    [HideInInspector] public int health;
    [HideInInspector] public int attackSpeed;
    [HideInInspector] public float attackRange;
    [HideInInspector] public float moveSpeed;
    [HideInInspector] public bool reloading; 


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
        attackSpeed = AS;
        attackRange = AR;
        moveSpeed = MS;
        agent.speed = 0;
        isOnCoolDown = false;
        reloading = false;

        if (isRanged == false)
        {
            cdr.radius = AR;
        }



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
        Delayer(500);
        agent.SetDestination(bb.player.transform.position); 
    }

    public void FireProjectile()
    {
        var x = GameObject.Instantiate(projectile,this.transform);
        
        
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
        Debug.Log("checking health");
        if(me.getHealth() <= 0)
        {
            Debug.Log("destroyed");
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
        Debug.Log("checking range");

        distanceBetween = Mathf.Abs(bb.player.transform.position.magnitude - me.transform.position.magnitude);
        if(distanceBetween > threshold)
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
            Debug.Log("moving to player");

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
                Debug.Log("attack");
                me.isOnCoolDown = true;
                me.meleeCooldown(me.getAttackSpeed());
                return result.Success;
            }
            else
            {
                Debug.Log("cooldown");
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
            me.FireProjectile();
            Debug.Log("shoot");
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


