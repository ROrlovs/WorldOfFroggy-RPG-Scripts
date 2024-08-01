using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Pawn : MonoBehaviour
{
    public int strength;
    public int agility;
    public int intelligence;
    public int stamina;
    public float attackDamage;
    public float spellDamage;
    public float attackCrit;
    public float spellCrit;
    public string pawnName;
    public Rigidbody2D rb;
    public float health = 1f;
    public float maxHealth = 1f;
    public float healthRegen = 1f;
    public float mana = 1f;
    public float maxMana=1f;
    public float manaRegen=1f;
    public float energy = 1f;
    public float maxEnergy=1f;
    public float energyRegen=1f;
    public float speed = 1f;
    public float timeBeforeDestroy = 2f;

    public bool canRegenerateHealth = true;
    public bool canRegenerateMana = true;
    public bool canRegenerateEnergy = true;

    public virtual void Awake()
    {

    }




    

    public virtual void Move(Vector2 movement)
    {
        transform.position = Vector2.MoveTowards(transform.position, movement, speed * Time.deltaTime);
    }

    public virtual void InitialiseStats()
    {
        health = maxHealth;
        mana = maxMana;
        energy = maxEnergy;

        EvaluateStats();
        StartCoroutine(RegenerateHealth());
        StartCoroutine(RegenerateMana());
        StartCoroutine(RegenerateEnergy());  

    }

    public virtual IEnumerator RegenerateHealth()
    {
        while (true)
        {
            if(canRegenerateHealth && health<maxHealth)
            {
                health+=maxHealth*0.05f*healthRegen;
                if(health>maxHealth) health=maxHealth;
            }

            yield return new WaitForSecondsRealtime(1f);
        }
    }

    public virtual IEnumerator RegenerateMana()
    {
        while (true)
        {
            if(canRegenerateMana && mana<maxMana)
            {
                mana+=maxMana*0.05f*manaRegen;
                if(mana>maxMana) mana=maxMana;
            }

            yield return new WaitForSecondsRealtime(1f);
        }
    }

    public virtual IEnumerator RegenerateEnergy()
    {
        while (true)
        {
            if(canRegenerateEnergy && energy<maxEnergy)
            {
                energy+=maxEnergy*0.05f*energyRegen;
                if(energy>maxEnergy) energy=maxEnergy;
            }

            yield return new WaitForSecondsRealtime(0.5f);
        }
    }

    public virtual IEnumerator StopHealthRegenForSeconds(float time)
    {
        int secondsPassed=0;
        canRegenerateHealth = false;
        while(secondsPassed<time)
        {
            secondsPassed++;
            yield return new WaitForSecondsRealtime(1f);
        }
        canRegenerateHealth=true;
    }

    public virtual IEnumerator StopManaRegenForSeconds(float time)
    {
        int secondsPassed=0;
        canRegenerateHealth = false;
        while(secondsPassed<time)
        {
            secondsPassed++;
            yield return new WaitForSecondsRealtime(1f);
        }
        canRegenerateHealth=true;
    }

    public virtual IEnumerator StopEnergyRegenForSeconds(float time)
    {
        int secondsPassed=0;
        canRegenerateHealth = false;
        while(secondsPassed<time)
        {
            secondsPassed++;
            yield return new WaitForSecondsRealtime(1f);
        }
        canRegenerateHealth=true;
    }

    public virtual void EvaluateStats()
    {
        healthRegen +=stamina*0.01f;
        manaRegen += intelligence*0.01f;
        energyRegen += agility*0.01f;
        maxHealth += stamina*10;
        maxMana += intelligence*10;
        maxEnergy += agility;
    }

    public virtual void HealHealth(float amount)
    {
        Debug.Log($"{pawnName} healed {amount} damage)");
        health += amount;
        if(health>maxHealth) health = maxHealth;
    }

    public virtual void AddMana(float amount)
    {
        Debug.Log($"{pawnName} gained {amount} mana)");
        mana += maxMana;
        if(mana>maxMana) mana = maxMana;
    }

    public virtual void AddEnergy(float amount)
    {
        Debug.Log($"{pawnName} gained {amount} energy)");
        energy += amount;
        if(energy>maxEnergy) energy = maxEnergy;
    }

    public virtual void TakeDamage(float amount)
    {
        Debug.Log($"{pawnName} took {amount} damage)");
        health -= amount;
        if(health<=0) Die();
    }

    public virtual void RemoveMana(float amount)
    {
        Debug.Log($"{pawnName} removed {amount} mana)");
        mana -= amount;
        if(mana<0) mana=0;
    }

    public virtual void RemoveEnergy(float amount)
    {
        Debug.Log($"{pawnName} removed {amount} energy)");
        energy -= amount;
        if(energy<0) energy=0;
    }

    public virtual void Die()
    {
        Destroy(this.gameObject,timeBeforeDestroy);
    }



}
