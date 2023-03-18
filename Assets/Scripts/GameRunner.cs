using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameState { START, PLAYER0_TURN, PLAYER1_TURN, PLAYER0_WIN, PLAYER1_WIN, DRAW}

public class GameRunner : MonoBehaviour
{
    public GameState state;
    public GameObject Player0;
    public GameObject Player1;
    public GameObject EndTurnButton;
    public static GameRunner instance { get; private set; }
    // Start is called before the first frame update

    void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }
    void Start()
    {
        state = GameState.START;
        StartCoroutine(GameSetUp());
    }

    // Update is called once per frame
    IEnumerator GameSetUp()
    {
        //no setup for now.
        yield return new WaitForSeconds(1.5f);
        state = GameState.PLAYER0_TURN;
        Player0.BroadcastMessage("GameStart");
        Player0.GetComponent<TurnHandler>().OnTurnStart();
        Player1.BroadcastMessage("GameStart");
        PlayerTurns();
    }

    void FixedUpdate()
    {

    }
    
    IEnumerator PlayerTurns()
    {
        yield return new WaitForSeconds(2.0f);
        if(state == GameState.PLAYER0_TURN)
        {
            //player0's turn
            // Debug.Log("Player 0 turn");
            Player1.BroadcastMessage("DeactiveHand", EndTurnButton);
            Player0.BroadcastMessage("ActiveHand", EndTurnButton);
            Player0.GetComponent<TurnHandler>().OnTurnStart();
        }
        else if(state == GameState.PLAYER1_TURN)
        {
            //player1's turn
            Player0.BroadcastMessage("DeactiveHand", EndTurnButton);
            Player1.BroadcastMessage("ActiveHand", EndTurnButton);
            Player1.GetComponent<TurnHandler>().OnTurnStart();
           // Debug.Log("Player 1 turn");
        }
    }

    public void EndTurn()
    {
        EndTurnButton.SetActive(false);
        if (state == GameState.PLAYER0_TURN)
        {
            state = GameState.PLAYER1_TURN;
            StartCoroutine(PlayerTurns());
        }else if(state == GameState.PLAYER1_TURN)
        {
            state = GameState.PLAYER0_TURN;
            StartCoroutine(PlayerTurns());
        }
    }


}
