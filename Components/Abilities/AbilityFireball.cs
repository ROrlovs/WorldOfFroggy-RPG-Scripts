
using UnityEngine;

public class AbilityFireball : Ability
{

    private Projectile projectile;
    
    public override void Action(Enemy target)
    {
        base.Action(target);
        projectile = Instantiate(objectToInstantiate,player.transform.position,Quaternion.identity).GetComponent<Projectile>();
        projectile.target = target;
    }

    public override void Start()
    {
        base.Start();
    }
}
