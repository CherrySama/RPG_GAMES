using UnityEngine;

public class PlayerWallSlideState : PlayerState
{
    public PlayerWallSlideState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        // Make sure to force reset Jump parameters
        // to resolve animation conflicts
        player.anim.SetBool("Jump", false);
        player.hasDoubleJumped = false;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        player.rb.linearVelocity = new Vector2(0.0f, player.rb.linearVelocityY * 0.8f);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            stateMachine.ChangeState(player.wallJumpState);
            return;
        }

        if (xInput != 0 && player.facingDir * xInput < 0)
        {
            stateMachine.ChangeState(player.idleState);
            return;
        }

        if (player.IsGroundDetected())
        {
            stateMachine.ChangeState(player.idleState);
            return;
        }

        if (Input.GetAxisRaw("Vertical") != 0 && player.IsLadderDetected())
        {
            stateMachine.ChangeState(player.ladderClimbState);
            return;
        }

        if (Input.GetAxisRaw("Vertical") == 0 && player.IsLadderDetected())
        {
            stateMachine.ChangeState(player.airState);
            return;
        }
    }
}
