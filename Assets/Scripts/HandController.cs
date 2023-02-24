using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour
{
    int DrawPerTurn = 0; //draw cards at turn draw phase.
    int DrawHandStart = 0; //draw cards at game start.
    List<ScriptableCard> GameDeck = new List<ScriptableCard>();
    [SerializeField] public List<ScriptableCard> Deck = new List<ScriptableCard>();

    List<ScriptableCard> Hand = new List<ScriptableCard>(); //
    void Awake()
    {

    }

    void GameStart()
    {
        GameDeck = new List<ScriptableCard>();
        foreach (ScriptableCard card in Deck)
        {
            GameDeck.Add(card); //puts the cards into the playable deck.
        }
        ShuffleDeck();
        DrawStartingCards(); //draws X cards at game start.
    }

    void DrawStartingCards()
    {
        for(int i = 0; i < DrawHandStart; i++)
        {
            TryDraw();
        }
    }

    void ShuffleDeck()
    {

    }

    void TryDraw()
    {
        //attempts to draw a card.
        //if the deck has no cards to draw
        //do empty deck effect. (Default, nothing).
        //else, draw card.
        if(GameDeck.Count > 0)
        {
            Hand.Add(GameDeck[0]); //adds card to hand.
            GameDeck.RemoveAt(0); //removes card from deck.
        }
        else
        {
            DoDeckOut();
        }
    }

    void DoDeckOut()
    {
        //list of functions. To execute on Deck Out 
        //custom Deck Out Effect.
    }
    void HandStart()
    {
        //any fancy hand set up?
    }

    void ActiveHand()
    {
        BroadcastMessage("tellCardTurn", true); //all cards are told that it is their player's turn;
    }

    void DeactiveHand()
    {
        BroadcastMessage("tellCardTurn", false); //all cards are told that it is no longer their player's turn;
    }
}
