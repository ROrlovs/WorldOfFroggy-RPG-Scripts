using UnityEngine;

public class Fireball : Projectile
{
    public override void Action(Enemy target)
    {
        base.Action(target);
        target.TakeDamage(damageToDeal);
    }
    


}
