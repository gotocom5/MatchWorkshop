using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State 
{
    protected PlayerMovement PlayerMovement;
    public State(PlayerMovement playerMovement) 
    {
        PlayerMovement = playerMovement;
    }
    public virtual IEnumerator Move() 
    {
        yield break;
    }
    public virtual IEnumerator Jump()
    {
        yield break;
    }
    public virtual IEnumerator Idle()
    {
        yield break;
    }
}

public class MoveRight : State 
{
    public MoveRight(PlayerMovement playerMovement) : base(playerMovement) { }
    public override IEnumerator Move() 
    {
        PlayerMovement.rigidbody.velocity = new Vector2(PlayerMovement.moveSpeed, PlayerMovement.rigidbody.velocity.y);
        //設定自身縮放的值
        PlayerMovement.transform.localScale = new Vector2(-1f, 1f);
        yield break;
    }
}

public class MoveLeft : State
{
    public MoveLeft(PlayerMovement playerMovement) : base(playerMovement) { }
    public override IEnumerator Move()
    {
        PlayerMovement.rigidbody.velocity = new Vector2(-PlayerMovement.moveSpeed, PlayerMovement.rigidbody.velocity.y);
        //設定自身縮放的值
        PlayerMovement.transform.localScale = new Vector2(1f, 1f);
        yield break;
    }
}

public class ReallyJump : State
{
    public ReallyJump(PlayerMovement playerMovement) : base(playerMovement) { }
    public override IEnumerator Jump()
    {
        PlayerMovement.rigidbody.velocity = new Vector2(PlayerMovement.rigidbody.velocity.x, PlayerMovement.JumpSpeed);
        PlayerMovement.jump = false;
        yield break;
    }
}
