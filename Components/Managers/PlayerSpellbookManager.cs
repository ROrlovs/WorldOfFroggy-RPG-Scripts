using System.Collections.Generic;

using UnityEngine;
using Vit.Utilities.Singletons;

public class PlayerSpellbookManager : Singleton<PlayerSpellbookManager>
{
    public List<Ability> spellbookAbilityList = new List<Ability>();

    void Start()
    {
        foreach(Ability ability in spellbookAbilityList)
        {
            ability.onCooldown=false;
        }
    }

    public List<Ability> ReturnSpellbookAbilities()
    {

        return spellbookAbilityList;
    }
    public void AddAbilityToSpellbook(Ability abilityToAdd)
    {
        spellbookAbilityList.Add(abilityToAdd);
    }

    public void RemoveAbilityFromSpellbook(Ability abilityToRemove)
    {
        spellbookAbilityList.Remove(abilityToRemove);
    }
}
