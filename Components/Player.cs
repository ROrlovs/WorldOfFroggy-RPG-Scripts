


using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.Pool;
using UnityEngine.Rendering;

public class Player : Pawn
{

    public Vector2 mousePos;
    [SerializeField] private float enemyTargetDistance = 3f;
    public Enemy target;

    public delegate void OnNewTarget(Enemy target);
    public OnNewTarget onNewTarget;

    public delegate void OnClearTarget();
    public OnClearTarget onClearTarget;

    private GameObject _lastHoveredObject;


    private void OnDestroy() 
    {
        PlayerExperienceManager.Instance.onLevelUp -= OnLevelUpStats; 
    }

    void Start()
    {
        if(PlayerExperienceManager.Instance!=null)
        PlayerExperienceManager.Instance.onLevelUp += OnLevelUpStats;
        else Debug.Log("PLAYEREPXZEIRENCEMANAGER DOES NOT EXIST!!");

        InitialiseStats();
    }
    
    void Update()
    {
        mousePos = Input.mousePosition;
        CheckAbilityInputs();
        CheckTargetInput();
        CheckForPawnUnderMouse();
        CheckMouseClicks();

        if(target!=null) CheckDistanceFromEnemy();
        

    }

    public override void InitialiseStats()
    {
        base.InitialiseStats();
       
    }

    public override void EvaluateStats()
    {
        base.EvaluateStats();
        attackDamage+= PlayerEquipmentManager.Instance.ReturnWeaponDamage() + (strength*2);
    }

    public void OnLevelUpStats()
    {
        strength++;
        intelligence++;
        agility++;
        stamina++;
        EvaluateStats();
    }


    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position,enemyTargetDistance);
    }

    private void CheckAbilityInputs()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            PlayerCastManager.Instance.AttemptCast(0, target);
        }

        if(Input.GetKeyDown(KeyCode.E))
        {
            PlayerCastManager.Instance.AttemptCast(1, target);
        }
    }

    private void CheckMouseClicks()
    {
        if(Input.GetMouseButton(0))
        {
            if(_lastHoveredObject != null)
                SetTarget(_lastHoveredObject.transform);
        }

        if(Input.GetMouseButton(1))
        {
            if(_lastHoveredObject != null)
                SetTarget(_lastHoveredObject.transform);
                     
        }

    }

    private void CheckTargetInput()
    {
        if(Input.GetKeyDown(KeyCode.Tab)) FindClosestEnemy();
        if(Input.GetKeyDown(KeyCode.Escape)) ClearTarget();
        
    }

    private void FindClosestEnemy()
    {
        int x=0;
        List<Transform> transforms = new List<Transform>();
        Collider2D[] hit;
        hit = Physics2D.OverlapCircleAll(transform.position,enemyTargetDistance);
        foreach (Collider2D enemy in hit)
        {
            transforms.Add(enemy.transform);
        }
        Transform closestEnemy = GetNextClosestEnemy(transforms);
        
        if(closestEnemy != null && closestEnemy.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            SetTarget(closestEnemy);
            
        }

    }

    public void ClearTarget()
    {
        if(target!=null)
        {
            target.UnsetAsTargetedEnemy();
        }
        target = null;
        onClearTarget.Invoke();
    }

    private void SetTarget(Transform targetTransform)
    {
        ClearTarget();
        onNewTarget.Invoke(targetTransform.GetComponent<Enemy>());
        target = targetTransform.GetComponent<Enemy>();
        target.SetAsTargetedEnemy();
        //Debug.Log("found enemy = "+target);
    }

    private void CheckDistanceFromEnemy()
    {
        float distance = (target.transform.position - transform.position).magnitude;
        //Debug.Log("distance from target = "+distance);
        if( distance > enemyTargetDistance)
        {
            ClearTarget();
        }
    }

    Transform GetNextClosestEnemy(List<Transform> enemies)
    {
        Transform bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        for (int i = 0; i < enemies.Count; i++)
        {
            Vector3 directionToTarget = enemies[i].position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if(dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = enemies[i];
                if(bestTarget.GetComponent<Enemy>().isTargeted && enemies.Count>1)
                {
                    if(i-1==0) bestTarget =  enemies[i-1];
                    else if (i+1<enemies.Count) bestTarget = enemies[i+1];
                    else bestTarget = enemies[i];
                    
                }
            }
        }
 
        return bestTarget;
    }

    

    private void LookAtMouse()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.rotation = Quaternion.LookRotation(Vector3.forward, (Vector3) mousePos-transform.position);
    }

    private void CheckForPawnUnderMouse()
    {
        
        Vector2 ray = new Vector2(Camera.main.ScreenToWorldPoint(mousePos).x, Camera.main.ScreenToWorldPoint(mousePos).y);
        RaycastHit2D hit = Physics2D.Raycast(ray, ray, LayerMask.NameToLayer("Enemy"));
        if (hit.collider != null)
        {
            Debug.Log(hit.collider.gameObject);
            GameObject pawnGO = hit.collider.gameObject;
            pawnGO.GetComponentInChildren<HoverOutline>().ActivateOutline();
            _lastHoveredObject = hit.collider.gameObject;
        }
        else
        {
            if(_lastHoveredObject != null)
            {
                _lastHoveredObject.GetComponentInChildren<HoverOutline>().DeactivateOutline();
                _lastHoveredObject = null;
            }
        }
    }


    public void AddStat(int stat)
    {
        switch(stat)
        {
            case (0):
                strength++;
            break;

            case (1):
                agility++;
            break;

            case (2):
                intelligence++;
            break;
        }

    }


}