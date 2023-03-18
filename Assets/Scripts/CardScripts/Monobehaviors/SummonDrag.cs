using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SummonDrag : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerEnterHandler, IPointerExitHandler, IDropHandler
{
    /**
     * SUMMONS (CARDS PLACED ON FIELD):
     * 
     * OnBeginDrag/OnEndDrag/OnDrag - this is dragging to target.
     * OnPointerEnter/OnPointerExit - Tooltips.
     * OnDrop - Check if dropped card can target this creature.
     *  - If dropped card is a Castable , do all the actions of this card, on actions with choosable target, the creature is the target.
     *  - If dropped card is a Summonable, do damage to this creature based on the other creature's attack, then resolve any after-effects.
     *         - Note difference Between WHEN is attacked (happens) and AFTER this is attacked (must survive first)
     * **/
    public bool newSummon = true; //basically dictates if can attack this turn.
    public bool isTargetable = true; //can you hit this summon?
    public bool retaliate = true; //does damage back to attacker?
    private RectTransform rectTransform;
    private Vector3 initialPos;
    private CanvasGroup canvasGroup;
    [SerializeField] int player;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        if (transform.parent.transform.parent.transform.parent.tag == "Player0")
        {
            Debug.Log("Under player 0");
            player = 0;
        }
        else
        {
            Debug.Log("Under player 1");
            player = 1;
        }
    }
    #region Drag
    public void OnBeginDrag(PointerEventData eventData)
    {
        initialPos = rectTransform.anchoredPosition;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (GetComponent<CardControl>().isPlayable())
        {
            rectTransform.anchoredPosition += eventData.delta;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition = initialPos;
        canvasGroup.blocksRaycasts = true;
    }
    #endregion
    #region Drop
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null && eventData.pointerDrag.GetComponent<CardSetup>().CardData.GetType() == typeof(ScriptableSummon) &&  eventData.pointerDrag.GetComponent<CardSetup>().getPlayer() != player && isTargetable)
        {
            //Do Damage of the dropped minion to this minion.
            eventData.pointerDrag.GetComponent<CardControl>().DoAttack(this.gameObject); //Do attack and attack effects on target
            if (retaliate)
            {
                GetComponent<CardControl>().CounterAttack(eventData.pointerDrag.gameObject); //this is different than a normal attack since the target is not the agressor.
                //Do damage back to attacker.
            }

            //Evaluate Effects of Attack.
        }
        
        if(eventData.pointerDrag != null && eventData.pointerDrag.GetComponent<CardSetup>().CardData.GetType() == typeof(ScriptableCast))
        {
            if (validCastTarget(eventData.pointerDrag.GetComponent<CardSetup>()))
            {
                //Do Action on Summon.
                eventData.pointerDrag.GetComponent<CardDrag>().hasTarget = true;
            }
        }
    }

    bool validCastTarget(CardSetup offensiveCast)
    {
        //Debug.Log()
        return false;
    }
    #endregion
    #region PointerOn
    public void OnPointerDown(PointerEventData eventData)
    {
        //   Debug.Log("OnPointerDown");
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        //Enter Pointer
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        //Exit Pointer
    }
    #endregion
}
