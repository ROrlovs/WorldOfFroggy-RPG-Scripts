using UnityEngine;

public class Ability : MonoBehaviour
{

    public AbilitySO abilitySO;
    public string nameOfAbility;
    public int cooldown;
    public int castingTime;
    private GameObject objectToInstantiate;
    public Pawn _pawn;
    public Player _player;
    public Vector2 mousePos;
    public bool usable=true;
    public bool onCooldown=false;


    
    public virtual void Start()
    {
        _pawn = 
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>()!=null ? 
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>() : null;

        nameOfAbility = abilitySO.nameOfAbility;
        cooldown = abilitySO.cooldown;
        castingTime = abilitySO.castingTime;
        objectToInstantiate = abilitySO.gameObjToInstantiate;
    }

    public void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }



    public virtual void Action(){}



}
