using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameState { START, PLAYER0_TURN, PLAYER1_TURN, PLAYER0_WIN, PLAYER1_WIN, DRAW}

public class GameRunner : MonoBehaviour
{
    public GameState state;
    [SerializeField]  GameObject GOPlayer0;
    [SerializeField]  GameObject GOPlayer1;
    [SerializeField] public static GameObject Player0;
    [SerializeField] public static GameObject Player1;
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

        Player0 = GOPlayer0;
        Player1 = GOPlayer1;
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
        GOPlayer0.BroadcastMessage("GameStart");
        GOPlayer0.GetComponent<TurnHandler>().OnTurnStart();
        GOPlayer1.BroadcastMessage("GameStart");
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
            GOPlayer1.BroadcastMessage("DeactiveHand", EndTurnButton);
            GOPlayer0.BroadcastMessage("ActiveHand", EndTurnButton);
            GOPlayer0.GetComponent<TurnHandler>().OnTurnStart();
        }
        else if(state == GameState.PLAYER1_TURN)
        {
            //player1's turn
            GOPlayer0.BroadcastMessage("DeactiveHand", EndTurnButton);
            GOPlayer1.BroadcastMessage("ActiveHand", EndTurnButton);
            GOPlayer1.GetComponent<TurnHandler>().OnTurnStart();
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
