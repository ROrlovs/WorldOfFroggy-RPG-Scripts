
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Vit.Utilities.Singletons;



public class PlayerCastManager : Singleton<PlayerCastManager>
{



    public Player player;
    public Enemy target;

    public delegate void OnPlayerStartCast(float castTime, string abilityName);
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

    public void AttemptCast(int index, Enemy abilityTarget) 
    {
        target = abilityTarget;
        _currentAbility = abilityList[index];
        if(_currentAbility.usable && !_currentAbility.onCooldown && target!=null)
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
        
        isCasting = true;
        //Debug.Log("invoking onplayerstartcast with "+_currentAbility.castingTime +" "+ _currentAbility.nameOfAbility);
        onPlayerStartCast.Invoke(_currentAbility.castingTime,_currentAbility.nameOfAbility);
        float secondsPassed = 0;

        while(isCasting && secondsPassed != _currentAbility.castingTime)
        {
            yield return new WaitForSecondsRealtime(1);
            secondsPassed++;
        }

        if(isCasting)
        {
            FireAbility(target);
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

    private void FireAbility(Enemy target)
    {
        //onPlayerSuccessCast.Invoke();
        _currentAbility.Action(target);
        StartCoroutine(StartCooldown(_currentAbility));
        isCasting=false;
        target = null;
        _currentAbility = null;
    }

    public void InterruptCast()
    {
        //onPlayerInterruptCast.Invoke();
        StopCoroutine(StartCastingTime());
        isCasting = false;
        Debug.Log("interrupted cast" + _currentAbility.nameOfAbility);
        target = null;
        _currentAbility = null;
    }



}
