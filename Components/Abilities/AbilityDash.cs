
using UnityEngine;

public class AbilityDash : Ability
{

    private float dashForce;



    public override void Action()
    {
        _pawn.rb.AddForce(new Vector2(1,0));
    }

}
