using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaccoonStateInAir : ByTheTale.StateMachine.State
{
    public RaccoonMachine Raccoon { get { return (RaccoonMachine)machine; } }

    public override void Enter()
    {
        base.Enter();
        Raccoon.SetAnimator("Jump");
    }

    public override void PhysicsExecute()
    {
        if (Raccoon.body.velocity.y < -0.01f)
        {

            if (Raccoon.IsAgainstWall.value)
            {

                Raccoon.ChangeState<RaccoonStateWallSlide>();
                return;
            }
            else
            {
                Raccoon.SetAnimator("Fall");

            }
        }

        if (Raccoon.IsGrounded.value)
        {
            if (Mathf.Abs(Raccoon.body.velocity.x) > 0.01f)
            {
                Raccoon.push = true;
                Raccoon.ChangeState<RaccoonStateRun>();
                return;

            }
            else
            {
                Raccoon.ChangeState<RaccoonStateIdle>();
                return;

            }
        }

        if (Raccoon.RequestingJump)
        {
            // TODO: mid-air jump logic goes here
            Raccoon.RequestingJump = false;
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
