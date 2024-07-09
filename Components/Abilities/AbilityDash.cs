
using UnityEngine;

[CreateAssetMenu(fileName = "Dash",menuName = "Ability/Agility/Dash")]
public class AbilityDash : Ability
{

    private float _dashForce = 1f;
    private new Player _player;
    private int ticksMoved = 0;
    private int maxTicksToMove = 200;
    private Vector2 positionToDashTo;

    public override void Update()
    {
        base.Update();
        if(ticksMoved<maxTicksToMove)
        {
            _player.transform.position = Vector2.MoveTowards(_player.transform.position,positionToDashTo,Time.deltaTime * _dashForce);
            ticksMoved++;
        }

        else
        {
            ticksMoved = 0;
        }

    }

    public override void Action(Enemy target)
    {
        _player=GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        positionToDashTo = abilityMousePos;

    }

}