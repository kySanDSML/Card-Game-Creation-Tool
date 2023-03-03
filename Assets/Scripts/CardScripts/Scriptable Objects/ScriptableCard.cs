using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptableCard : ScriptableObject
{
    bool isPlayerTurn = true; //initially cards are not 

    void tellCardTurn(bool isTurn)
    {
        isPlayerTurn = isTurn;
    }
}
