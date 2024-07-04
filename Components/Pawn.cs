using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Pawn : MonoBehaviour
{

    public Rigidbody2D rb;

    public virtual void Move(Vector2 movement)
    {
        rb.MovePosition(movement);
    }



}
