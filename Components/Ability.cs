using UnityEngine;

public class Ability : MonoBehaviour
{

    public AbilitySO abilitySO;
    public string nameOfAbility;
    public float cooldown;
    public float castingTime;
    public float damageToDeal;
    public GameObject objectToInstantiate;
    public Pawn pawn;
    public Player player;
    public Vector2 abilityMousePos;
    public Enemy target;
    public bool usable=true;
    public bool onCooldown=false;


    
    public virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        pawn = 
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>()!=null ? 
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>() : null;

        nameOfAbility = abilitySO.nameOfAbility;
        cooldown = abilitySO.cooldown;
        castingTime = abilitySO.castingTime;
        objectToInstantiate = abilitySO.gameObjToInstantiate;
        damageToDeal = abilitySO.damageToDeal;
    }

    public void Update()
    {
        abilityMousePos = player.mousePos;
    }



    public virtual void Action(Enemy target){}



}
