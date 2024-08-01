using UnityEngine;

[CreateAssetMenu(fileName = "enemySO",menuName ="EnemySO")]
public class EnemySO : ScriptableObject
{
    public string enemyName;

    public float speed;
    public int expToGive;
    public bool isTargetable;

    [Space]

    public bool canRegenerateHealth = true;
    public bool canRegenerateMana = true;
    public bool canRegenerateEnergy = true;

    public float maxHealth;
    public float maxMana;
    public float maxEnergy;
    public float healthRegen;
    public float manaRegen;
    public float energyRegen;

    [Space]

    public int strength;
    public int agility;
    public int intelligence;
    public int stamina;
    public float attackDamage;



}
