using UnityEngine;

[CreateAssetMenu(fileName ="AbilitySO", menuName ="Custom/SO")]
public class AbilitySO : ScriptableObject
{
    public string nameOfAbility;
    public int cooldown;
    public int castingTime;
    public GameObject gameObjToInstantiate;
    public Ability script;

    public void Test()
    {
        Debug.Log("hello test");
    }

}
