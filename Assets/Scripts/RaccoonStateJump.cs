using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaccoonStateJump : ByTheTale.StateMachine.State
{
    public RaccoonMachine Raccoon { get { return (RaccoonMachine)machine; } }

    public override void Enter()
    {
        base.Enter();
        //Raccoon.SetAnimator("Jump");
    }

    public override void Execute()
    {
        Debug.Log("Jumping");
        base.Execute();
    }

    public override void Exit()
    {
        base.Exit();
    }
}
