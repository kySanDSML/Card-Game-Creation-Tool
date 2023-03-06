using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardControl : MonoBehaviour
{
    PlayerStats playerStats;
    CardDrag cd;
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
    }
}
