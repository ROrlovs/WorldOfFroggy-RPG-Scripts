using UnityEngine;

public class MovementStateMachine : StateMachine
{
    [HideInInspector]
    public IdleState idleState;
    [HideInInspector]
    public MovingState movingState;
    [HideInInspector]
    public CastingState castingState;
    public Rigidbody2D rb;
    public float speed = 4f;

    

    private void Awake()
    {
        idleState = new IdleState(this);
        movingState = new MovingState(this);
        castingState = new CastingState(this);
    }

    protected override BaseState GetInitialState()
    {
        return idleState;
    }
}
