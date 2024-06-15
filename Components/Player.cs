
using UnityEngine;

public class Player : Pawn
{
    private Vector2 _mousePos;

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
            PlayerCastManager.instance.Cast(0);
        }
    }

    private void LookAtMouse()
    {
        _mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.rotation = Quaternion.LookRotation(Vector3.forward, (Vector3) _mousePos-transform.position);
    }


}