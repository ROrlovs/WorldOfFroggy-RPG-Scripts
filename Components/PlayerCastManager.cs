
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;



public class PlayerCastManager : MonoBehaviour
{

    public delegate void OnPlayerStartCast();
    public OnPlayerStartCast onPlayerStartCast;

    public delegate void OnPlayerStopCast();
    public OnPlayerStopCast onPlayerStopCast;

    private Ability _currentAbility;

    public List<Ability> abilityList;



    public static PlayerCastManager instance;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }

        else
        {
            Debug.LogWarning("MULTIPLE INSTANCES OF "+this);
            
        }
    }

    void Start()
    {
        InitialiseAbilityList();
    }

    private void InitialiseAbilityList()
    {
        abilityList = GetComponentsInChildren<Ability>().ToList();
        foreach (Ability ab in abilityList)
        {
            Debug.Log("Ability added to list: "+ab.nameOfAbility+" into index "+abilityList.FindIndex(a => a == ab));
        }
    }


//add new abilities player can use into spellbook
    void AddAbility(AbilitySO ability)
    {

    }

    public void Cast(int index) 
    {
        //-----
    if (index < 0 || index >= abilityList.Count || abilityList[index] == null)
    {
        Debug.Log("Invalid ability index or null ability at index " + index);
        return;
    }
        //-----


        _currentAbility = abilityList[index];
        if(_currentAbility.usable && !_currentAbility.onCooldown)
        {
            StartCoroutine(StartCastingTime());
            Debug.Log("casting "+_currentAbility.nameOfAbility);
        }

        else
        {
            Debug.Log("Cannot use this ability! "+_currentAbility.nameOfAbility);
        }

        
    }

    private IEnumerator StartCastingTime()
    {
        int secondsPassed = 0;

        while(secondsPassed != _currentAbility.castingTime)
        {
            yield return new WaitForSecondsRealtime(1);
            secondsPassed++;
        }

        FireAbility();


               
    }
   

    private IEnumerator StartCooldown(Ability ability)
    {
        int secondsPassed = 0;

        while(secondsPassed != ability.cooldown)
        {
            yield return new WaitForSecondsRealtime(1);
            secondsPassed++;
        }

    }

    private void FireAbility()
    {
        StartCoroutine(StartCooldown(_currentAbility));
        _currentAbility = null;
    }



}
