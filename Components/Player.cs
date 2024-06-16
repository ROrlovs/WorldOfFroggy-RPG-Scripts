
using UnityEngine;

public class Player : Pawn
{
    public Vector2 mousePos;

    void Start()
    {
    }
    
    void Update()
    {

        CheckAbilityInputs();
        LookAtMouse();

    }

    private void CheckAbilityInputs()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            PlayerCastManager.Instance.AttemptCast(0);
        }
    }

    private void LookAtMouse()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.rotation = Quaternion.LookRotation(Vector3.forward, (Vector3) mousePos-transform.position);
    }


}