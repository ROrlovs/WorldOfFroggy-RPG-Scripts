
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

    public delegate void OnPlayerStartCast(Ability ability);
    public OnPlayerStartCast onPlayerStartCast;

    public delegate void OnPlayerInterruptCast();
    public OnPlayerInterruptCast onPlayerInterruptCast;

    public delegate void OnPlayerSuccessCast();
    public OnPlayerSuccessCast onPlayerSuccessCast;
    public bool isCasting;

    private Ability _currentAbility;





    void Start()
    {
        player = 
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>()!=null ? 
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>() : null;
        
    }






    public void AttemptCast(int index, Enemy abilityTarget) 
    {
        target = abilityTarget;
        if(target==null)
        {
            AttemptCast(index);
            return;
        } 
        _currentAbility = PlayerSpellbookManager.Instance.spellbookAbilityList[index];
        if(_currentAbility.usable && !_currentAbility.onCooldown && target!=null && !isCasting)
        {
            Debug.Log("casting "+_currentAbility.nameOfAbility);
            StartCoroutine(StartCastingTime());
            
        }

        else
        {
            Debug.Log($"Cannot use this ability! {_currentAbility.nameOfAbility}, variables: Usable:{_currentAbility.usable}, OnCooldown:{_currentAbility.onCooldown}, PlayerIsCasting:{isCasting}");
        }

        
    }

    public void AttemptCast(int index) 
    {
        _currentAbility = PlayerSpellbookManager.Instance.spellbookAbilityList[index];
        if(_currentAbility.usable && !_currentAbility.onCooldown && !_currentAbility.requiresTarget && !isCasting)
        {
            Debug.Log("casting "+_currentAbility.nameOfAbility);
            StartCoroutine(StartCastingTime());
        }

        else
        {
            Debug.Log($"Cannot use this ability! {_currentAbility.nameOfAbility}, variables: Usable:{_currentAbility.usable}, OnCooldown:{_currentAbility.onCooldown}, PlayerIsCasting:{isCasting}");
        }

        
    }


    private IEnumerator StartCastingTime()
    {
        
        isCasting = true;
        //Debug.Log("invoking onplayerstartcast with "+_currentAbility.castingTime +" "+ _currentAbility.nameOfAbility);
        onPlayerStartCast.Invoke(_currentAbility);
        float ticksPassed = 0;

        while(isCasting && ticksPassed != _currentAbility.castingTime*10)
        {
            yield return new WaitForSecondsRealtime(0.1f);
            ticksPassed++;
        }

        if(isCasting)
        {
            FireAbility(target);
        }
        


               
    }
   

    private IEnumerator StartCooldown(Ability ability)
    {
        int secondsPassed = 0;
        ability.onCooldown = true;

        while(secondsPassed != ability.cooldown)
        {
            yield return new WaitForSecondsRealtime(1);
            secondsPassed++;
        }

        ability.onCooldown = false;

    }

    private void FireAbility(Enemy target)
    {
        //onPlayerSuccessCast.Invoke();
        _currentAbility.Action(target);
        if(_currentAbility.objectToInstantiate !=null)
        {
            Projectile projectile;
            projectile = Instantiate(_currentAbility.objectToInstantiate,player.transform.position,Quaternion.identity).GetComponent<Projectile>();
            projectile.target = target;
        }
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
