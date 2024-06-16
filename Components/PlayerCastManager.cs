
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Vit.Utilities.Singletons;



public class PlayerCastManager : Singleton<PlayerCastManager>
{



    public Player player;

    public delegate void OnPlayerStartCast();
    public OnPlayerStartCast onPlayerStartCast;

    public delegate void OnPlayerInterruptCast();
    public OnPlayerInterruptCast onPlayerInterruptCast;

    public delegate void OnPlayerSuccessCast();
    public OnPlayerSuccessCast onPlayerSuccessCast;
    public bool isCasting;

    private Ability _currentAbility;

    public List<Ability> abilityList;




    void Start()
    {
        player = 
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>()!=null ? 
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>() : null;
            
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

    public void AttemptCast(int index) 
    {
        _currentAbility = abilityList[index];
        if(_currentAbility.usable && !_currentAbility.onCooldown)
        {
            Debug.Log("casting "+_currentAbility.nameOfAbility);
            StartCoroutine(StartCastingTime());
            
        }

        else
        {
            Debug.Log("Cannot use this ability! "+_currentAbility.nameOfAbility);
        }

        
    }

    private IEnumerator StartCastingTime()
    {
        onPlayerStartCast.Invoke();
        isCasting = true;
        int secondsPassed = 0;

        while(isCasting && secondsPassed != _currentAbility.castingTime)
        {
            yield return new WaitForSecondsRealtime(1);
            secondsPassed++;
        }

        if(isCasting)
        {
            FireAbility();
        }
        


               
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
        //onPlayerSuccessCast.Invoke();
        _currentAbility.Action();
        StartCoroutine(StartCooldown(_currentAbility));
        isCasting=false;
        _currentAbility = null;
    }

    public void InterruptCast()
    {
        //onPlayerInterruptCast.Invoke();
        StopCoroutine(StartCastingTime());
        isCasting = false;
        Debug.Log("interrupted cast" + _currentAbility.nameOfAbility);
        _currentAbility = null;
    }



}
