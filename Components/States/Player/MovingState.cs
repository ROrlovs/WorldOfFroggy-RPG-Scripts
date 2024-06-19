using System;

using UnityEngine;
using UnityEngine.UIElements;

public class MovingState : BaseState
{
    private MovementStateMachine _sm;
    private float _horizontalInput;
    private float _verticalInput;

    public MovingState(MovementStateMachine stateMachine) : base("MovingState", stateMachine) {
        _sm = (MovementStateMachine) stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
        _horizontalInput = _verticalInput = 0f;
        PlayerCastManager.Instance.onPlayerStartCast += StopMovement;
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        _horizontalInput = Input.GetAxis("Horizontal");
        _verticalInput = Input.GetAxis("Vertical");

        if(Mathf.Abs(_horizontalInput)<Mathf.Epsilon && Mathf.Abs(_verticalInput)<Mathf.Epsilon)
            stateMachine.ChangeState(_sm.idleState);

    }
    public override void UpdatePhysics()
    {
        base.UpdatePhysics();

        Vector2 vel = _sm.rb.velocity;
        vel = new Vector2(_horizontalInput, _verticalInput);
        vel*=_sm.speed;
        _sm.rb.velocity = vel;
    }

    private void StopMovement(float notused_float, string notused_string)
    {
        //Debug.Log("stop movement!!!");
        _sm.rb.velocity = Vector2.zero;
        _horizontalInput = _verticalInput = 0;
        stateMachine.ChangeState(_sm.idleState);
    }
}
