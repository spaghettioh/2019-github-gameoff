using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaccoonStateRun : ByTheTale.StateMachine.State
{
    public RaccoonMachine Raccoon { get { return (RaccoonMachine)machine; } }

    public override void Enter()
    {
        base.Enter();
        //Raccoon.SetAnimator("Run");
    }

    public override void Execute()
    {
        Debug.Log("Running");
        base.Execute();
    }

    public override void Exit()
    {
        base.Exit();
    }
}
