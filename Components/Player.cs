

using System.Runtime.CompilerServices;
using UnityEngine;

public class Player : Pawn
{
    public Vector2 mousePos;
    [SerializeField] private float enemyTargetDistance = 3f;
    public Enemy target;

    public delegate void OnNewTarget(Enemy target);
    public OnNewTarget onNewTarget;

    public delegate void OnClearTarget();
    public OnClearTarget onClearTarget;

    private void OnEnable() 
    {
        
    }

    private void OnDisable() 
    {
        
    }

    void Start()
    {
    }
    
    void Update()
    {

        CheckAbilityInputs();
        CheckTargetInput();

        if(target!=null) CheckDistanceFromEnemy();
        

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

    private void CheckTargetInput()
    {
        if(Input.GetKeyDown(KeyCode.Tab)) FindClosestEnemy();
        if(Input.GetKeyDown(KeyCode.Escape)) ClearTarget();
        
    }

    private void FindClosestEnemy()
    {
        Transform[] transforms;
        Collider2D hit;
        hit = Physics2D.OverlapCircle(transform.position,enemyTargetDistance);
        
        if(hit != null && hit.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            SetTarget(hit.transform);
            onNewTarget.Invoke(hit.GetComponent<Enemy>());
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