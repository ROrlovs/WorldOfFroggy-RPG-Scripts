using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    private Rigidbody2D _rb;
    public float damageToDeal;
    public float speed=1f;
    public Enemy target;
    public Pawn sender;

    public virtual void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    public virtual void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position,target.transform.position,speed*Time.deltaTime);
    }

    public virtual void Action(Enemy target)
    {
    }

    public virtual void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("trigger");
        if(other.gameObject == target.gameObject)
        {
            Action(other.gameObject.GetComponent<Enemy>());
            Destroy(gameObject);
        }
        
    }
}
