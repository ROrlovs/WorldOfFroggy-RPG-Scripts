
using UnityEngine;

[CreateAssetMenu(fileName = "Smite",menuName = "Ability/Holy/Smite")]
public class AbilitySmite : Ability
{


    private new Player _player;




    public override void Action(Enemy target)
    {
        target.TakeDamage(10);
    }

}