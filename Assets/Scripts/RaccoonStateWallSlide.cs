using UnityEngine;

public class RaccoonStateWallSlide : ByTheTale.StateMachine.State
{
    public RaccoonMachine Raccoon { get { return (RaccoonMachine)machine; } }

    float dragModifier;
    
    public override void Enter()
    {
        base.Enter();
        Raccoon.SetAnimator("WallSlide");
        dragModifier = 50;
        Raccoon.push = false;
    }

    public override void PhysicsExecute()
    {
        dragModifier -= 1;
        Raccoon.body.drag = dragModifier;
        
        if (Raccoon.RequestingJump)
        {
            Raccoon.direction *= -1;
            Raccoon.body.drag = 0;
            Raccoon.body.velocity += new Vector2(Raccoon.moveSpeed * Raccoon.direction, (Raccoon.jumpForce * Raccoon.jumpMultiplier));
            //Raccoon.body.AddForce(new Vector2((Raccoon.moveForce + 2) * Raccoon.direction, Raccoon.jumpForce + 2 + Raccoon.jumpModifier), ForceMode2D.Impulse);
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
