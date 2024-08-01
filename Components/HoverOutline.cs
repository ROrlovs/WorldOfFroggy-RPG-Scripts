
using System;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class HoverOutline : MonoBehaviour
{

    public void ActivateOutline()
    {
        GetComponent<SpriteRenderer>().enabled = true;
    }

    public void DeactivateOutline()
    {
        GetComponent<SpriteRenderer>().enabled = false;
    }

}
