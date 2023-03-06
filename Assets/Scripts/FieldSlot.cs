using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FieldSlot : MonoBehaviour, IDropHandler
{
    int player = -1;
    // Start is called before the first frame update
    void Start()
    {
        if(transform.parent.transform.parent.tag == "Player0")
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

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("Dropped.");
        if (eventData.pointerDrag != null && eventData.pointerDrag.GetComponent<CardSetup>().getPlayer() == player && eventData.pointerDrag.GetComponent<CardControl>().isPlayable())
        {
            //eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
            if (eventData.pointerDrag.GetComponent<CardSetup>().CardData.GetType() == typeof(ScriptableSummon))
            {
                eventData.pointerDrag.GetComponent<CardDrag>().enabled = false;
                eventData.pointerDrag.GetComponent<CardSetup>().Played();
                eventData.pointerDrag.transform.SetParent(this.gameObject.transform);
                eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);
            }
        }
        else
        {
            eventData.pointerDrag.GetComponent<CardDrag>().returnToHand();
        }

        Debug.Log("Player of Dragged card: "+eventData.pointerDrag.GetComponent<CardSetup>().getPlayer());
    }
}
