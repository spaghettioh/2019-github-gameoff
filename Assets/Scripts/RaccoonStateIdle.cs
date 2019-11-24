using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaccoonStateIdle : ByTheTale.StateMachine.State
{
    public RaccoonMachine Raccoon { get { return (RaccoonMachine)machine; } }

    public override void Enter()
    {
        base.Enter();
        Raccoon.SetAnimator("Idle");
    }

    public override void PhysicsExecute()
    {
        if (Raccoon.IsGrounded.value)
        {
            if (!Raccoon.IsAgainstWall.value)
            {
                Raccoon.pushing = true;
            }
            else
            {
                Raccoon.pushing = false;
            }

            if (Raccoon.RequestingJump)
            {
                Raccoon.pushing = true;
                Raccoon.GroundedJump();
            }

            if ((float)Mathf.Abs(Raccoon.body.velocity.x) > 1)
            {
                Raccoon.ChangeState<RaccoonStateRun>();
            }
        }
        else
        {
            Raccoon.ChangeState<RaccoonStateInAir>();
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
