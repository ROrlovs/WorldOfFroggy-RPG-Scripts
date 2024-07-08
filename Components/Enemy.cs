using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : Pawn, ITargetable
{

    public bool isTargeted;

    public SpriteRenderer targetSpriteRenderer;



    void Start()
    {
        //Debug.Log(ReturnTargetTransform());
        ToggleTargetSprite(false);
    }

    public override void Move(Vector2 movement)
    {
        base.Move(movement);
    }

    public override void Die()
    {
        if(isTargeted)
        {
            Debug.Log("targeted");
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().ClearTarget();
        } 
        isTargeted = false;
        base.Die();
    }



    private void Update() {
        if(targetSpriteRenderer.gameObject.activeSelf)
        {
            RotateTargetSprite();
        }
        
    }

    public void SetAsTargetedEnemy()
    {
        isTargeted = true;
        ToggleTargetSprite(true);
    }

    public void UnsetAsTargetedEnemy()
    {
        isTargeted = false;
        ToggleTargetSprite(false);
    }

    public Transform ReturnTargetTransform()
    {
        return this.gameObject.transform;
    }


    private void ToggleTargetSprite(bool state)
    {
        targetSpriteRenderer.gameObject.SetActive(state);
    }

    public void RotateTargetSprite()
    {
        targetSpriteRenderer.transform.Rotate(new Vector3 (0,0,1));
    }
}
