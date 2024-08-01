
using System.Collections;

using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : Pawn, ITargetable
{
   [Header("PLACE SO HERE")]
    public EnemySO enemySO;
    [Space]
    public bool isTargeted;
    public bool isTargetable;


    public SpriteRenderer targetSpriteRenderer;
    public int expToGive;

    public override void Awake()
    {
        base.Awake();
    }


    void Start()
    {
        //Debug.Log(ReturnTargetTransform());
        ToggleTargetSprite(false);
        InitialiseStats();
        isTargetable = true;
    }

    public override void InitialiseStats()
    {
        pawnName = enemySO.enemyName;
        speed = enemySO.speed;
        expToGive = enemySO.expToGive;
        isTargetable=enemySO.isTargetable;
        strength = enemySO.strength;
        agility = enemySO.agility;
        intelligence = enemySO.intelligence;
        stamina = enemySO.stamina;
        maxHealth = enemySO.maxHealth;
        maxMana = enemySO.maxMana;
        maxEnergy = enemySO.maxEnergy;
        healthRegen = enemySO.healthRegen;
        manaRegen = enemySO.manaRegen;
        energyRegen = enemySO.energyRegen;
        canRegenerateEnergy = enemySO.canRegenerateEnergy;
        canRegenerateHealth = enemySO.canRegenerateHealth;
        canRegenerateMana = enemySO.canRegenerateMana;

        base.InitialiseStats();       
    }




    public override void Move(Vector2 movement)
    {
        base.Move(movement);
    }

    public override void Die()
    {
        Destroy(GetComponent<BoxCollider2D>());
        PlayerExperienceManager.Instance.AddExperience(expToGive);
        isTargetable = false;
        if(isTargeted)
        {
            Debug.Log("targeted");
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().ClearTarget();
        } 
        isTargeted = false;
        base.Die();
    }



    private void Update() {
        if(targetSpriteRenderer.gameObject.activeSelf)
        {
            RotateTargetSprite();
        }
        
    }

    public void SetAsTargetedEnemy()
    {
        if(isTargetable)
        {
            isTargeted = true;
            ToggleTargetSprite(true);
        }

    }

    public void UnsetAsTargetedEnemy()
    {
        isTargeted = false;
        ToggleTargetSprite(false);
    }

    public Transform ReturnTargetTransform()
    {
        return this.gameObject.transform;
    }


    private void ToggleTargetSprite(bool state)
    {
        targetSpriteRenderer.gameObject.SetActive(state);
    }

    public void RotateTargetSprite()
    {
        targetSpriteRenderer.transform.Rotate(new Vector3 (0,0,1));
    }
}
