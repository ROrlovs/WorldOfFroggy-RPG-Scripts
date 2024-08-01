
using UnityEngine;
using Vit.Utilities.Singletons;

public class PlayerEquipmentManager : Singleton<PlayerEquipmentManager>
{

    public Weapon weapon;
    public delegate void OnEquipmentChanged();
    public OnEquipmentChanged onEquipmentChanged;

    public override void Awake()
    {
        base.Awake();
        //testing
        weapon = ScriptableObject.CreateInstance<Weapon>();
        weapon.damageLower = 10;
        weapon.damageUpper = 15;
    }


    public int ReturnWeaponDamage()
    {
        return Random.Range(weapon.damageLower,weapon.damageUpper);
    }
}