using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum result
{
    Success, Failure, Running
}
public abstract class Node
{
    public BlackBoard board;


    public Node(BlackBoard board)
    {
        this.board = board;
    }

    public abstract result Execute();

    public virtual void Reset()
    {

    }
}

public abstract class CompositeNode : Node
{
    protected List<Node> children;
    protected int childIndex = 0;
    public CompositeNode(BlackBoard board) : base(board)
    {
        this.board = board;
        children = new List<Node>();
    }

    public void AddChildClass(Node child)
    {
        children.Add(child);
    }

    public override void Reset()
    {
        childIndex = 0;
        for (int i = 0; i < children.Count; i++)
        {
            children[i].Reset();
        }
    }
}


public class SelectorNode : CompositeNode
{
    public SelectorNode(BlackBoard board) : base(board) { this.board = board; }
    public override result Execute()
    {
        for (int i = childIndex; i < children.Count; i++)
        {
            result rv = children[i].Execute();
            if (rv == result.Success)
            {
                return result.Success;
            }
            else if (rv == result.Running)
            {
                childIndex = i;
                return result.Running;
            }
        }
        Reset();
        return result.Failure;
    }

}
public class SequenceNode : CompositeNode
{

    public SequenceNode(BlackBoard board) : base(board) { this.board = board; }


    public override result Execute()
    {
        for (int i = childIndex; i < children.Count; i++)
        {
            result rv = children[i].Execute();
            if (rv == result.Failure)
            {
                Reset();
                return result.Failure;
            }
            else if (rv == result.Running)
            {
                childIndex = i;
                return result.Running;
            }
        }
        Reset();
        return result.Success;
    }
}




