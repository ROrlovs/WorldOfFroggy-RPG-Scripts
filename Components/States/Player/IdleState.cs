
using UnityEngine;

public class IdleState : BaseState
{
    private MovementStateMachine _sm;
    private float _horizontalInput;
    private float _verticalInput;
    private Player _player;

    public IdleState(MovementStateMachine stateMachine) : base("IdleState", stateMachine) 
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
        {
            if(!PlayerCastManager.Instance.isCasting)
            {
                stateMachine.ChangeState(((MovementStateMachine) stateMachine).movingState);
            }

            else
            {
                PlayerCastManager.Instance.InterruptCast();
            }
            
        }
            


}
}
