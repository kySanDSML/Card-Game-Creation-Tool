using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardDrag : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler , IPointerEnterHandler, IPointerExitHandler
{
    // Start is called before the first frame update
    private RectTransform rectTransform;
    private Vector3 cachedScale;
    private Vector3 initialPos;
    private CanvasGroup canvasGroup;
    private GameObject OGparent;
    public bool canDrag = true;
    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        OGparent = transform.parent.gameObject;
        cachedScale = rectTransform.transform.localScale;
    }
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition -= new Vector2(0, rectTransform.sizeDelta.y / 2 * 1.3f);
        rectTransform.transform.localScale = cachedScale;
        rectTransform.SetSiblingIndex(initialIndex);
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
    public bool hasTarget = false;
    public void OnEndDrag(PointerEventData eventData)
    {
      //  Debug.Log("OnEndDrag");
        if (outside)
        {
            if (GetComponent<CardControl>().isPlayable() && GetComponent<CardSetup>().CardData.GetType() == typeof(ScriptableCast))
            {
                if(!GetComponent<CardSetup>().needsTarget || hasTarget)
                {
                    GetComponent<CardDrag>().enabled = false;
                    GetComponent<CardSetup>().Played();
                    Destroy(this.gameObject);
                }
            }
        }
        canvasGroup.blocksRaycasts = true;
        returnToHand();
    }

    public void OnPointerDown(PointerEventData eventData){
     //   Debug.Log("OnPointerDown");
    }

    int initialIndex;
    public void OnPointerEnter(PointerEventData eventData)
    {
        initialPos = rectTransform.anchoredPosition;
        initialIndex = rectTransform.GetSiblingIndex();
        rectTransform.SetAsLastSibling();
        rectTransform.transform.localScale = cachedScale * 1.5f;
        rectTransform.anchoredPosition += new Vector2 (0, rectTransform.sizeDelta.y/2 * 1.3f);

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        rectTransform.transform.localScale = cachedScale;
        rectTransform.SetSiblingIndex(initialIndex);
        rectTransform.anchoredPosition = initialPos;
    }

    public void returnToHand()
    {
        outside = true;
        transform.SetParent(OGparent.transform);
        rectTransform.anchoredPosition = initialPos;
    }
}
