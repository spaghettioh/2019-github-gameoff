using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaccoonStateRun : ByTheTale.StateMachine.State
{
    public RaccoonMachine Raccoon { get { return (RaccoonMachine)machine; } }

    public override void Enter()
    {
        base.Enter();
        Raccoon.SetAnimator("Run");
    }

    public override void PhysicsExecute()
    {
        // Update orientation
        if (Raccoon.body.velocity.x < -0.1f)
        {
            Raccoon.direction = -1;
        }
        else
        {
            Raccoon.direction = 1;
        }

        if (Raccoon.IsAgainstWall.value || (float)Mathf.Abs(Raccoon.body.velocity.x) < 0.1f)
        {
            Raccoon.ChangeState<RaccoonStateIdle>();
        }

        if (Raccoon.RequestingJump)
        {
            Raccoon.GroundedJump();
            return;
        }
        else
        {
            Raccoon.RequestingJump = false;
        }

        if (!Raccoon.IsGrounded.value)
        {
            Raccoon.ChangeState<RaccoonStateInAir>();
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
