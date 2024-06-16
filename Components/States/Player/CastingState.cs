
using UnityEngine;

public class CastingState : BaseState
{
    private MovementStateMachine _sm;
    private float _horizontalInput;
    private float _verticalInput;

    public CastingState(MovementStateMachine stateMachine) : base("CastingState", stateMachine) 
    {
        _sm = (MovementStateMachine) stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
        _horizontalInput = _verticalInput = 0f;
        _sm.rb.velocity = new Vector2(0f,0f);
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        
        _horizontalInput = Input.GetAxis("Horizontal");
        _verticalInput = Input.GetAxis("Vertical");
        if(Mathf.Abs(_horizontalInput)>Mathf.Epsilon || Mathf.Abs(_verticalInput)>Mathf.Epsilon)
            stateMachine.ChangeState(((MovementStateMachine) stateMachine).movingState);


    }
}
