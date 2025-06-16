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
    public int health;
    public bool isOnCoolDown;
    public NavMeshAgent agent;


    public BlackBoard bb;
    float attackRange;


    private Node root;


    void Start()
    {
        Delayer(200);
        isOnCoolDown = false;
        agent = GetComponent<NavMeshAgent>();
        agent.updateUpAxis = false;
        agent.updateRotation = false;
        attackRange = 1;
        health = 5;
        bb = FindAnyObjectByType<BlackBoard>();

        agent.speed = 0;

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
        CheckDetectionRange checkDetectionRange = new CheckDetectionRange(this, bb, 3f);
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

        //attack action
        AttackPlayer attackPlayer = new AttackPlayer(this, bb);
        attackRangeCheck.AddChildClass(attackPlayer);

        InvokeRepeating("StartTree", 0f, 2f);
    }

    public void StartTree()
    {
        root.Execute();
    }

    private async void Delayer(int timer)
    {
        await Task.Delay(timer);
    }

    private async void coolDown(int timer)
    {
        await Task.Delay(timer);
        isOnCoolDown = false;
    }

    void Update()
    {
        Delayer(500);
        agent.SetDestination(bb.player.transform.position); 
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
        if(me.health <= 0)
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
        Debug.Log("moving to player");

        me.agent.speed = 5;
        return result.Success;
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
        Debug.Log("attack");
        throw new System.NotImplementedException();
    }
}


