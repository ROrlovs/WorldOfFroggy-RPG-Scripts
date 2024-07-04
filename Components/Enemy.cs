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



    private void Update() {
        if(targetSpriteRenderer.gameObject.activeSelf)
        {
            RotateTargetSprite();
        }
        
    }

    public void SetAsTargetedEnemy()
    {
        ToggleTargetSprite(true);
    }

    public void UnsetAsTargetedEnemy()
    {
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
