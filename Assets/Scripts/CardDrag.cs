using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardDrag : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler 
{
    // Start is called before the first frame update
    private RectTransform rectTransform;
    private Vector3 initialPos;
    private CanvasGroup canvasGroup;
    private GameObject OGparent;
    public bool canDrag = true;
    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        OGparent = transform.parent.gameObject;
    }
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        initialPos = rectTransform.anchoredPosition;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        outside = true;
        if (GetComponent<CardControl>().isPlayable())
        {
            rectTransform.anchoredPosition += eventData.delta;
        }
    }
    public bool outside = true;
    public void OnEndDrag(PointerEventData eventData)
    {
      //  Debug.Log("OnEndDrag");
        if (outside)
        {
            if (GetComponent<CardControl>().isPlayable() && GetComponent<CardSetup>().CardData.GetType() == typeof(ScriptableCast))
            {
                GetComponent<CardDrag>().enabled = false;
                GetComponent<CardSetup>().Played();
                Destroy(this.gameObject);
            }
        }
        canvasGroup.blocksRaycasts = true;
        returnToHand();
    }

    public void OnPointerDown(PointerEventData eventData){
     //   Debug.Log("OnPointerDown");
    }

    public void returnToHand()
    {
        outside = true;
        transform.SetParent(OGparent.transform);
        rectTransform.anchoredPosition = initialPos;
    }
}
