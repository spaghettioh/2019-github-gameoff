using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaccoonStateInAir : ByTheTale.StateMachine.State
{
    public RaccoonMachine Raccoon { get { return (RaccoonMachine)machine; } }

    public override void Enter()
    {
        base.Enter();
        Raccoon.SetAnimator("InAir");
    }

    public override void PhysicsExecute()
    {
        if (Raccoon.IsAgainstWall.value && Raccoon.body.velocity.y < -0.01f)
        {
            Raccoon.ChangeState<RaccoonStateWallSlide>();
        }

        if (Raccoon.RequestingJump)
        {
            // TODO: mid-air jump logic goes here
            Raccoon.RequestingJump = false;
        }

        if (Raccoon.IsGrounded.value)
        {
            Raccoon.ChangeState<RaccoonStateIdle>();
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
