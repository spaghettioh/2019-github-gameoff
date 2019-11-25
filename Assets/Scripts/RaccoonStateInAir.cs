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

        if (Raccoon.IsAgainstWall.value)
        {
            Raccoon.push = false;

            if (Raccoon.RequestingJump)
            {
                Raccoon.direction *= -1;
                Raccoon.body.velocity += new Vector2(Raccoon.moveSpeed * Raccoon.direction, (Raccoon.jumpForce * Raccoon.jumpMultiplier));
                //Raccoon.body.AddForce(new Vector2((Raccoon.moveForce + 2) * Raccoon.direction, Raccoon.jumpForce + 2 + Raccoon.jumpModifier), ForceMode2D.Impulse);
                Raccoon.RequestingJump = false;
                Raccoon.wallJumpAudio.PlayRandomSound();
                Debug.Log("JumpForce: " + Raccoon.jumpForce + "\nJumpMultiplier: " + Raccoon.jumpMultiplier);
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
