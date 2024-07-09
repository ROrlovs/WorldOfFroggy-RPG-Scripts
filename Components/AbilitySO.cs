using UnityEngine;

[CreateAssetMenu(fileName ="AbilitySO", menuName ="Custom/SO")]
public class AbilitySO : ScriptableObject
{
    public string nameOfAbility;
    public float cooldown;
    public float castingTime;
    public GameObject gameObjToInstantiate;
    public Ability script;
    public float damageToDeal;
    public bool requiresTarget;

}
