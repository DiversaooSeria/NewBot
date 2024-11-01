using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Direction : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    [SerializeField] public Canvas canvas;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;


    public int direction;

    private bool isAnchored;

    private GameObject clonedObject;

    private GameObject anchoredPlaceholder;

    private void Awake()
    {
        isAnchored = false;
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();

    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (isAnchored == false)
        {
            clonedObject = GameObject.Instantiate(this.gameObject);
            clonedObject.transform.SetParent(GameObject.Find("InventoryContent").transform);
            clonedObject.GetComponent<RectTransform>().position = rectTransform.position;
            clonedObject.GetComponent<RectTransform>().rotation = rectTransform.rotation;
            clonedObject.GetComponent<RectTransform>().localScale = rectTransform.localScale;
        }
        else
        {
            //GameObject.Find("InventoryContent").GetComponent<Fila>().Add(null, anchoredPlaceholder.GetComponent<DND_DropReceiver>().GetPosition());
        }

        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

        if (isAnchored == false)
            clonedObject.name = this.gameObject.name;

        Destroy(this.gameObject);
    }

    public void Anchor(GameObject ph)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

        isAnchored = true;
        anchoredPlaceholder = ph;
    }
}
