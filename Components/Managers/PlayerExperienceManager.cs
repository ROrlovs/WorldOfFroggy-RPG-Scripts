
using UnityEngine;
using Vit.Utilities.Singletons;

public class PlayerExperienceManager : Singleton<PlayerExperienceManager>
{
    [SerializeField]private int exp;
    private int level;
    private const float levelUpConstant = 0.3f;
    public float percentageToNextLevel;

    public delegate void OnLevelUp();
    public OnLevelUp onLevelUp;

    public int GetPlayerExperience()
    {

        return exp;
    }

    public int GetPlayerLevel()
    {
        return (int)level;
    }

    public float GetPercentToNextPlayerLevel()
    {
        return percentageToNextLevel;
    }

    public void AddExperience(int amount)
    {
        exp+=amount;
        EvaluateExperience();
    }

    public int ExpToLevel(float pExp)
    {
        int final;
        float pLevel;
        pLevel=levelUpConstant * Mathf.Sqrt(pExp);
        final = Mathf.FloorToInt(pLevel);
        Debug.Log(pExp+" to level is "+final);
        return final;
    }

    public float LevelToExp(int pLevel)
    {
        float final;
        float pExp;
        pExp = pLevel / levelUpConstant;
        final = Mathf.Pow(pExp,2);
        Debug.Log(pLevel+" to exp is "+final);
        return final;
    }



    private void EvaluateExperience()
    {
        int oldLevel = level;
        level = ExpToLevel(exp);
        float expOfNextLevel = LevelToExp(level+1);
        percentageToNextLevel = exp/expOfNextLevel;
        Debug.Log("percent to next level "+percentageToNextLevel);
        if(oldLevel<level)
        {
            LevelUp();
            Debug.Log($"Level up! new level is {level} experience is {exp}, exp of next level is {expOfNextLevel}");
        }
    }

    private void LevelUp()
    {
        onLevelUp.Invoke();
    }


}
