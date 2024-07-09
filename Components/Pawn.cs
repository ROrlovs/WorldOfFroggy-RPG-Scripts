using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Pawn : MonoBehaviour
{
    public string pawnName;
    public Rigidbody2D rb;
    public float health = 1f;
    public float speed = 1f;
    public float maxHealth = 1f;
    public float timeBeforeDestroy = 2f;

    public virtual void Move(Vector2 movement)
    {
        transform.position = Vector2.MoveTowards(transform.position, movement, speed * Time.deltaTime);
    }

    public virtual void TakeDamage(float amount)
    {
        Debug.Log($"{pawnName} took {amount} damage)");
        health -= amount;
        if(health>maxHealth) health = maxHealth;
        if(health<=0) Die();
    }

    public virtual void Die()
    {
        Destroy(this.gameObject,timeBeforeDestroy);
    }



}
