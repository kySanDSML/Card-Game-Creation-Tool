using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardActions
{
    // Start is called before the first frame update
    /**ACTION FORMAT:
     *  Parameters: List<GameObject> Targets, int actionValue 
     *  Return Type: int totalValue (or 0)
     **/
    public static Dictionary<Action, System.Func<List<GameObject>, int, int>> ActionDictionary = new Dictionary<Action, System.Func<List<GameObject>, int, int>>() {
        {Action.Damage,  new System.Func<List<GameObject>, int, int>(DoDamage)},
        {Action.ReduceDamage,  new System.Func<List<GameObject>, int, int>(ReduceDamage)},
        {Action.IncreaseHealth,  new System.Func<List<GameObject>, int, int>(IncreaseHealth)},
        {Action.IncreaseDamage,  new System.Func<List<GameObject>, int, int>(IncreaseDamage)},
        {Action.RestoreHealth,  new System.Func<List<GameObject>, int, int>(RestoreHealth)},
        {Action.RestoreDamage,  new System.Func<List<GameObject>, int, int>(RestoreDamage)},
        {Action.FullHeal,  new System.Func<List<GameObject>, int, int>(FullHeal)},
        {Action.RestoreStats,  new System.Func<List<GameObject>, int, int>(ReturnToBaseStats)},
        {Action.ResetHealth,  new System.Func<List<GameObject>, int, int>(ResetHealth)},
        {Action.ResetDamage,  new System.Func<List<GameObject>, int, int>(ResetDamage)},
        {Action.SetHealth,  new System.Func<List<GameObject>, int, int>(SetHealth)},
        {Action.SetDamage,  new System.Func<List<GameObject>, int, int>(SetDamage)},
    };

    public static int DoDamage(List<GameObject> Targets, int ActionValue)
    {
        int damage = 0;
        foreach(GameObject target in Targets)
        {
            target.GetComponent<CardSetup>().CurrCardHealth -= ActionValue;
            target.GetComponent<CardControl>().DeathCheck();
            damage += ActionValue;
        }
        return damage;
    }

    public static int ReduceDamage(List<GameObject> Targets, int ActionValue)
    {
        int dmgReduced = 0;
        foreach(GameObject target in Targets)
        {
            CardSetup cs = target.GetComponent<CardSetup>();
            cs.CurrCardDamage = Mathf.Max(0, cs.CardDamage - ActionValue);
            dmgReduced += Mathf.Max(0, cs.CardDamage - ActionValue);
        }
        return dmgReduced;
    }

    public static int IncreaseHealth(List<GameObject> Targets, int ActionValue)
    {
        int healing = 0;
        foreach(GameObject target in Targets)
        {
            target.GetComponent<CardSetup>().CurrCardHealth += ActionValue;
            target.GetComponent<CardControl>().DeathCheck();
            healing += ActionValue;
        }
        return healing;
    }

    public static int IncreaseDamage(List<GameObject> Targets, int ActionValue)
    {
        int increasedDmg = 0;
        foreach(GameObject target in Targets){
            target.GetComponent<CardSetup>().CurrCardDamage += ActionValue;
            increasedDmg += ActionValue;
        }
        return increasedDmg;
    }

    public static int RestoreHealth(List<GameObject> Targets, int ActionValue)//Restore health UP TO max health
    {
        int healingDone = 0;
        foreach(GameObject target in Targets)
        {
            CardSetup cs = target.GetComponent<CardSetup>();
            if ((cs.CurrCardHealth + ActionValue) > cs.CardHealth) {
                cs.CurrCardHealth += Mathf.Max(0,cs.CardHealth - cs.CurrCardHealth);
                healingDone += Mathf.Max(0, cs.CardHealth - cs.CurrCardHealth);
            }//Max(0, #) prevents card from having it's health decreased if it's health has been increased past normal max.
            target.GetComponent<CardControl>().DeathCheck();
        }
        return healingDone;
    }

    public static int RestoreDamage(List<GameObject> Targets, int ActionValue)//Restore damage UP TO max damage
    {
        int dmgRestored = 0;
        foreach (GameObject target in Targets)
        {
            CardSetup cs = target.GetComponent<CardSetup>();
            if ((cs.CurrCardDamage + ActionValue) > cs.CardDamage)
            {
                cs.CurrCardDamage += Mathf.Max(0, cs.CardDamage - cs.CurrCardDamage);
                dmgRestored += Mathf.Max(0, cs.CardDamage - cs.CurrCardDamage);
                //Max(0, #) prevents card from having it's damage decreased if it's damage has been increased past normal max.
            }
            
        }
        return dmgRestored;
    }

    public static int FullHeal(List<GameObject> Targets, int ActionValue)
    {
        foreach (GameObject target in Targets)
        {
            CardSetup cs = target.GetComponent<CardSetup>();
            if (cs.CurrCardHealth < cs.CardHealth) //if health less than max health
            {
                cs.CurrCardHealth = cs.CardHealth; //restore to full.
            }
        }
        return 0;
    }
   
    public static int ReturnToBaseStats(List<GameObject> Targets, int ActionValue)
    {
        foreach(GameObject target in Targets)
        {
            CardSetup cs = target.GetComponent<CardSetup>();
            if(cs.CurrCardHealth > cs.CardHealth)
            {
                cs.CurrCardHealth = cs.CardHealth; //resets an overhealed summon back to normal health
            }
            if(cs.CurrCardDamage > cs.CardDamage)
            {
                cs.CurrCardHealth = cs.CardDamage;
            }
        }
        return 0;
    }

    public static int ResetHealth(List<GameObject> Targets, int ActionValue)
    {
        foreach (GameObject target in Targets)
        {
            CardSetup cs = target.GetComponent<CardSetup>();
            if (cs.CurrCardHealth > cs.CardHealth)
            {
                cs.CurrCardHealth = cs.CardHealth;
            }
        }
        return 0;
    }

    public static int ResetDamage(List<GameObject> Targets, int ActionValue) //resets damage back to O.G. value.
    {
        foreach (GameObject target in Targets)
        {
            CardSetup cs = target.GetComponent<CardSetup>();
            if (cs.CurrCardDamage != cs.CardDamage)
            {
                cs.CurrCardHealth = cs.CardDamage;
            }
        }
        return 0;
    }

    public static int SetHealth(List<GameObject> Targets, int ActionValue)
    {
        foreach(GameObject target in Targets)
        {
            CardSetup cs = target.GetComponent<CardSetup>();
            cs.CurrCardHealth = ActionValue;
            target.GetComponent<CardControl>().DeathCheck();

        }
        return 0;
    }

    public static int SetDamage(List<GameObject> Targets, int ActionValue)
    {
        foreach(GameObject target in Targets)
        {
            CardSetup cs = target.GetComponent<CardSetup>();
            cs.CurrCardDamage = ActionValue;
            if (cs.CurrCardDamage < 0)
            {
                cs.CurrCardDamage = 0; //prevents "negative" damage.
            }
        }
        return 0;
    }
}
