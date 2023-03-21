using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardControl : MonoBehaviour
{
    PlayerStats playerStats;
    CardDrag cd;
    public bool isPlaced = false;
    public void setPlayer(PlayerStats p)
    {
        playerStats = p;
    }
    // Start is called before the first frame update
    bool isPlayerTurn = true; //initially cards are not 

    void tellCardTurn(bool isTurn)
    {
        isPlayerTurn = isTurn;
    }

    void Awake()
    {
        cd = this.gameObject.GetComponent<CardDrag>();
    }
    // Update is called once per frame
    public virtual void FixedUpdate()
    {

    }

    public bool isPlayable()
    {
       // Debug.Log(this.gameObject.GetComponent<CardSetup>().CurrCardCost+" PE "+playerStats.getEnergy());
        if (playerStats == null)
        {
            return false;
        }
        else if (playerStats.getEnergy() < this.gameObject.GetComponent<CardSetup>().CurrCardCost)
        {
            return false;
        }
        else if (isPlayerTurn == this.GetComponent<CardSetup>().PlayedOnlyOnTurn)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public void PlayedCard(int cost)
    {
        //Debug.Log("CARD PLAYED");
        playerStats.setEnergy(playerStats.getEnergy() - cost);
        playerStats.capEnergy();
        isPlaced = true;
    }
    public GameObject target;
    
    public void ResolveEffects()
    {
        //resolve Effects from CardData. (this is mostly for summons)
        //STEP 1: Resolve Effects that should happen ON ATTACK. Reguardless of creature health.
        //STEP 2: Check if creature lives.
        //Step 2B: Kill creature if it dies
        //STEP 3: Resolve Effects that should happen AFTER ATTCK. If creature lives.
    }

    public void ResolveActions(List<ActionTargetPair> actions, GameObject target)
    {
        Debug.Log("RESOLVING ACTIONS");
        List<GameObject> allTargets = new List<GameObject>();
        foreach(ActionTargetPair ATP in actions)
        {
            if (target == null) {
                Debug.Log("No selected target. (This is fine for cards with no targeting).");
            }
            
            if (isSingularTarget(ATP.target))
            {
                Debug.Log("Singular Target");
                allTargets = new List<GameObject>() { target }; //the only target is the target targeted by the card.
            }
            else
            {
                Debug.Log("Locking Multitarget.");
                allTargets = LockTargets(ATP.target);
            }

            for (int i = 0; i <= ATP.repeatCount; i++)
            {
                CardActions.ActionDictionary[ATP.action](allTargets, ATP.actionValue);
                //Do the action to the targets.
            }
        }
    }

    public List<GameObject> LockTargets(Target target)
    {
        switch ((int)target)
        {
            case (int)Target.Everything:
                return GetAllTargetable();
            case (int)Target.AllMinions:
                return GetAllMinions();
            case (int)Target.AllPlayerMinions:
                return GetAllAllyMinions();
            case (int)Target.AllEnemyMinions:
                return GetAllEnemyMinions();
            default:
                return new List<GameObject>();
        }
    }

    public List<GameObject> GetAllTargetable()
    {
        List<GameObject> alltargets = new List<GameObject>();
        alltargets.Add(GameRunner.Player0);
        alltargets.Add(GameRunner.Player1);
        foreach(GameObject minion in GetAllMinions())
        {
            alltargets.Add(minion);
        }
        return alltargets;
    }

    public List<GameObject> GetAllMinions()
    {
        List<GameObject> alltargets = new List<GameObject>();
        foreach(GameObject minion in GetAllAllyMinions())
        {
            alltargets.Add(minion);
        }
        foreach(GameObject minion in GetAllEnemyMinions())
        {
            alltargets.Add(minion);
        }
        return alltargets;
    }
    
    public List<GameObject> GetAllAllyMinions()
    {
        string player = playerStats.gameObject.tag;
        GameObject field = GameObject.FindWithTag("PlayerField");
        GameObject playerField = GameObject.FindWithTag(player);
        List<GameObject> Cards = new List<GameObject>();
        foreach (Transform child in playerField.transform.GetChild(0)) //first child should always be card slot holder.
        {
            if (child.gameObject.tag == "CardSlot" && child.childCount > 0 && child.gameObject.transform.GetChild(0).gameObject.tag == "Card")
            {
                Cards.Add(child.gameObject.transform.GetChild(0).gameObject);
            }
        }//now we have all card slots.
        return Cards;
    }

    public List<GameObject> GetAllEnemyMinions()
    {
        string player = playerStats.gameObject.tag;
        if(player == "Player0") //swap tags.
        {
            player = "Player1";
        }
        else
        {
            player = "Player0";
        }
        GameObject field = GameObject.FindWithTag("PlayerField");
        GameObject playerField = GameObject.FindWithTag(player);
        List<GameObject> Cards = new List<GameObject>();
        foreach (Transform child in playerField.transform.GetChild(0)) //first child should always be card slot holder.
        {
            if (child.gameObject.tag == "CardSlot" && child.childCount > 0 && child.gameObject.transform.GetChild(0).gameObject.tag == "Card")
            {
                Cards.Add(child.gameObject.transform.GetChild(0).gameObject);
            }
        }//now we have all card slots.
        return Cards;
    }
  
    public bool isSingularTarget(Target target)
    {
        switch ((int)target)
        {
            case (int)Target.AnyTarget:
                return true;
            case (int)Target.AnyPlayer:
                return true;
            case (int)Target.Player:
                return true;
            case (int)Target.EnemyPlayer:
                return true;
            case (int)Target.AnyMinion:
                return true;
            case (int)Target.PlayerMinion:
                return true;
            case (int)Target.EnemyMinion:
                return true;
            default:
                return false;
        }
    }
    
    public void ResolveAction(ActionTargetPair action)
    {
        //resolve Actions from CardData. (this is for spell-like effects.)
    }

    public void DoAttack(GameObject target)
    {
        target.GetComponent<CardSetup>().CurrCardHealth -= GetComponent<CardSetup>().CurrCardDamage;
        GetComponent<SummonDrag>().attacksLeft--;
        //Do attack effects.
    }

    public void CounterAttack(GameObject target)
    {
        target.GetComponent<CardSetup>().CurrCardHealth -= GetComponent<CardSetup>().CurrCardDamage;
    }

    public void DeathCheck()
    {
        return;
    }
}
