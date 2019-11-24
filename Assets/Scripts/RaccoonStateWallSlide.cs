using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaccoonStateWallSlide : ByTheTale.StateMachine.State
{
    public RaccoonMachine Raccoon { get { return (RaccoonMachine)machine; } }

    public override void Enter()
    {
        base.Enter();
        //Raccoon.SetAnimator("WallSlide");
    }

    public override void Execute()
    {
        Debug.Log("Wall sliding");
        base.Execute();
    }

    public override void Exit()
    {
        base.Exit();
    }
}
