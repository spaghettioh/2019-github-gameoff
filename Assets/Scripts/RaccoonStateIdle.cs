using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaccoonStateIdle : ByTheTale.StateMachine.State
{
    public RaccoonMachine Raccoon { get { return (RaccoonMachine)machine; } }

    public override void Enter()
    {
        base.Enter();
        //Raccoon.SetAnimator("Idle");
    }

    public override void Execute()
    {
        Debug.Log("Idling");
        base.Execute();

        //if (Raccoon.IsGrounded)
        //{
        //    Raccoon.ChangeState<RaccoonStateRun>();
        //}
    }

    public override void Exit()
    {
        base.Exit();
    }
}
