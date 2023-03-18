using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HandController : MonoBehaviour, IDropHandler
{
    int DrawPerTurn = 1; //draw cards at turn draw phase.
    int DrawHandStart = 2; //draw cards at game start.
    [SerializeField] public List<ScriptableCard> Deck = new List<ScriptableCard>();
    [SerializeField] List<ScriptableCard> GameDeck = new List<ScriptableCard>();
    [SerializeField] List<GameObject> Hand = new List<GameObject>();
    [SerializeField] GameObject Card;
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
        for (int i = 0; i < DrawHandStart; i++)
        {
            TryDraw();
        }
    }

    void DrawStartOfTurn()
    {
        for (int i = 0; i < DrawPerTurn; i++)
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
        if (GameDeck.Count > 0)
        {
            StartCoroutine(AddCardToHand(GameDeck[0])); //adds card to hand.
            GameDeck.RemoveAt(0); //removes card from deck.
        }
        else
        {
            DoDeckOut();
        }
    }

    public virtual IEnumerator AddCardToHand(ScriptableCard cardScript)
    {
        yield return new WaitForSeconds(0.25f); //wait a second.
        float handWidth = this.gameObject.GetComponent<RectTransform>().sizeDelta.x;
        Debug.Log(handWidth);
        float newLocation = (Card.GetComponent<RectTransform>().sizeDelta.x + 20) * Hand.Count + Card.GetComponent<RectTransform>().sizeDelta.x * 0.5f;
        GameObject newCard = Instantiate(Card, this.gameObject.transform);
        newCard.transform.localPosition = new Vector3(handWidth / 2.0f - newLocation, 0, 0);
        newCard.GetComponent<CardSetup>().CardData = cardScript;
        newCard.GetComponent<CardSetup>().SetUp();
        newCard.GetComponent<CardControl>().setPlayer(transform.parent.GetComponent<PlayerStats>());
        Hand.Add(newCard);

        if (this.transform.parent != null && transform.parent.tag == "Player0")
        {
            newCard.GetComponent<CardSetup>().setPlayer(0);
        }
        else
        {
            newCard.GetComponent<CardSetup>().setPlayer(1);
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

    IEnumerator ActiveHand(GameObject ETB)
    {
        Debug.Log(gameObject.name + " is turn.");
        BroadcastMessage("tellCardTurn", true, SendMessageOptions.DontRequireReceiver); //all cards are told that it is their player's turn;
        DrawStartOfTurn();
        yield return new WaitForSeconds(2.0f);
        ETB.SetActive(true);
    }

    IEnumerator DeactiveHand(GameObject ETB)
    {
        BroadcastMessage("tellCardTurn", false, SendMessageOptions.DontRequireReceiver); //all cards are told that it is no longer their player's turn;
        yield return new WaitForSeconds(1.0f);
    }

    public void RemoveFromHand(GameObject card)
    {
        Hand.Remove(card);
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("Dropped on Hand.");
        if (eventData.pointerDrag.GetComponent<CardDrag>() != null)
        {
            eventData.pointerDrag.GetComponent<CardDrag>().outside = false;
        }
    }
}