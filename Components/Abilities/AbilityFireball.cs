
using UnityEngine;

[CreateAssetMenu(fileName = "Fireball",menuName = "Ability/Fire/Fireball")]
public class AbilityFireball : Ability
{

    private Projectile projectile;
    
    public override void Action(Enemy target)
    {
        base.Action(target);

    }



    public override void Start()
    {
        base.Start();
    }
}
