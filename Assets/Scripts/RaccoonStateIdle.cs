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
                Raccoon.push = true;
            }
            else
            {
                Raccoon.push = false;
            }



            if ((float)Mathf.Abs(Raccoon.body.velocity.x) > .1f)
            {
                Raccoon.ChangeState<RaccoonStateRun>();
                return;
            }
        }
        else
        {
            Raccoon.ChangeState<RaccoonStateInAir>();
            return;
        }
        if (Raccoon.RequestingJump)
        {
            Raccoon.push = true;
            Raccoon.GroundedJump();
        }
    }

    public override void Execute()
    {
    }

    public override void Exit()
    {
        base.Exit();
    }
}
