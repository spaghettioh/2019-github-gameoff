using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaccoonStateWallSlide : ByTheTale.StateMachine.State
{
    public RaccoonMachine Raccoon { get { return (RaccoonMachine)machine; } }

    public override void Enter()
    {
        base.Enter();
        Raccoon.SetAnimator("WallSlide");
        Raccoon.body.drag = 5;
    }

    public override void PhysicsExecute()
    {
        if (Raccoon.RequestingJump)
        {
            Raccoon.direction *= -1;
            Raccoon.push = false;
            Raccoon.body.AddForce(new Vector2((Raccoon.moveForce + 2) * Raccoon.direction, Raccoon.jumpForce + 2), ForceMode2D.Impulse);
            Raccoon.RequestingJump = false;
            Raccoon.wallJumpAudio.PlayRandomSound();

            Raccoon.ChangeState<RaccoonStateInAir>();
            return;
        }

        if (!Raccoon.IsAgainstWall.value)
        {
            Raccoon.ChangeState<RaccoonStateInAir>();
            return;

        }

        if (Raccoon.IsGrounded.value)
        {
            Raccoon.ChangeState<RaccoonStateIdle>();
            return;
        }
    }

    public override void Exit()
    {
        base.Exit();
        Raccoon.body.drag = 0;
    }
}
