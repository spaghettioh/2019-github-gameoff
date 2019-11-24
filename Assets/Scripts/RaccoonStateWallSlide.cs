﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaccoonStateWallSlide : ByTheTale.StateMachine.State
{
    public RaccoonMachine Raccoon { get { return (RaccoonMachine)machine; } }

    public override void Enter()
    {
        base.Enter();
        Raccoon.SetAnimator("WallSlide");
    }

    public override void PhysicsExecute()
    {
        base.Execute();

        if (Raccoon.RequestingJump)
        {
            Raccoon.direction *= -1;
            Raccoon.pushing = false;
            Raccoon.body.AddForce(new Vector2((Raccoon.moveForce + 2) * Raccoon.direction, Raccoon.jumpForce + 2), ForceMode2D.Impulse);
            Raccoon.RequestingJump = false;
            Raccoon.wallJumpAudio.PlayRandomSound();

            Raccoon.ChangeState<RaccoonStateInAir>();
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
