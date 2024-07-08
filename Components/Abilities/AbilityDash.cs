
using UnityEngine;

public class AbilityDash : Ability
{

    [SerializeField] private float _dashForce;
    private new Player _player;
    private float ticksMoved;



    public override void Action(Enemy target)
    {
        _player=GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        _player.rb.velocity=Vector2.zero;
        Vector2 moveTo = Vector2.MoveTowards(_player.transform.position,_player.mousePos,20f);
        _player.rb.AddForce(moveTo * _dashForce);
    }

}