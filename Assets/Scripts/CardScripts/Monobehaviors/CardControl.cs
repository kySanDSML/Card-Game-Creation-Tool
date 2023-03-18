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

    public void ResolveActions()
    {
        //resolve Actions from CardData. (this is for spells or spell-like effects.)
    }

    public void DoAttack(GameObject target)
    {
        target.GetComponent<CardSetup>().CurrCardHealth -= GetComponent<CardSetup>().CurrCardDamage;
        //Do attack effects.
    }

    public void CounterAttack(GameObject target)
    {
        target.GetComponent<CardSetup>().CurrCardHealth -= GetComponent<CardSetup>().CurrCardDamage;
    }
}
