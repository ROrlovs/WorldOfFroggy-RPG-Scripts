

using System.Runtime.CompilerServices;
using UnityEngine;

public class Player : Pawn
{
    public Vector2 mousePos;
    [SerializeField] private float enemyTargetDistance = 3f;
    public Enemy target;

    void Start()
    {
    }
    
    void Update()
    {

        CheckAbilityInputs();
        CheckTargetInput();
        LookAtMouse();
        if(target!=null) CheckDistanceFromEnemy();
        

    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position,enemyTargetDistance);
    }

    private void CheckAbilityInputs()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            PlayerCastManager.Instance.AttemptCast(0);
        }
    }

    private void CheckTargetInput()
    {
        if(Input.GetKeyDown(KeyCode.Tab)) FindClosestEnemy();
        
    }

    private void FindClosestEnemy()
    {
        Transform[] transforms;
        Collider2D hit;
        hit = Physics2D.OverlapCircle(transform.position,enemyTargetDistance);
        
        if(hit != null && hit.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            SetTarget(hit.transform);
        }

    }

    private void ResetTarget()
    {
        if(target!=null)
        {
            target.UnsetAsTargetedEnemy();
            target = null;
        }
    }

    private void SetTarget(Transform targetTransform)
    {
        ResetTarget();
        target = targetTransform.GetComponent<Enemy>();
        target.SetAsTargetedEnemy();
        Debug.Log("found enemy = "+target);
    }

    private void CheckDistanceFromEnemy()
    {
        float distance = (target.transform.position - transform.position).magnitude;
        Debug.Log("distance from target = "+distance);
        if( distance > enemyTargetDistance)
        {
            ResetTarget();
        }
    }

    Transform GetClosestEnemy(Transform[] enemies)
    {
        Transform bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        for (int i = 0; i < enemies.Length; i++)
        {
            Vector3 directionToTarget = enemies[i].position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if(dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = enemies[i];
            }
        }
 
        return bestTarget;
    }

    

    private void LookAtMouse()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.rotation = Quaternion.LookRotation(Vector3.forward, (Vector3) mousePos-transform.position);
    }


}