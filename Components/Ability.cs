using UnityEngine;

[CreateAssetMenu(fileName = "Ability", menuName = "Ability/TestAbility")]
public class Ability : ScriptableObject
{

    public AbilitySO abilitySO;
    public string nameOfAbility;
    public float cooldown;
    public float castingTime;
    public float damageToDeal;
    public float manaCost;
    public float energyCost;
    public GameObject objectToInstantiate;
    public Pawn pawn;
    public Player player;
    public Vector2 abilityMousePos;
    public Enemy target;
    public bool usable=true;
    public bool onCooldown=false;
    public bool requiresTarget;
    public Sprite sprite;


    
    public virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        pawn = 
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>()!=null ? 
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>() : null;

/*         nameOfAbility = abilitySO.nameOfAbility;
        cooldown = abilitySO.cooldown;
        castingTime = abilitySO.castingTime;
        objectToInstantiate = abilitySO.gameObjToInstantiate;
        damageToDeal = abilitySO.damageToDeal;
        requiresTarget = abilitySO.requiresTarget; */
    }

    public virtual void Update()
    {

    }



    public virtual void Action(Enemy target)
    {
        if(requiresTarget && target==null)
        {
            Debug.Log("No enemy selected!");
        }
    }



}
