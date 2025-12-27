using Inventory.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Direction : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] public Canvas canvas;
    [SerializeField] public DirectionData directionData;

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;

    public bool isAnchored;
    private GameManager gameManager;
    private GameObject clonedObject;
    private GameObject anchoredPlaceholder;
    private GameObject animObject;

    private Vector2 offset;

    public int direction;

    private void Awake()
    {
        direction = directionData.directionValue;
        isAnchored = false;
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        gameManager = FindObjectOfType<GameManager>();
        animObject = GameObject.Find("Anim");
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (animObject != null)
        {
            animObject.SetActive(false);
        }

        if (!isAnchored)
        {
            clonedObject = Instantiate(this.gameObject);
            clonedObject.transform.SetParent(GameObject.Find("InventoryContent").transform);
            clonedObject.GetComponent<RectTransform>().position = rectTransform.position;
            clonedObject.GetComponent<RectTransform>().rotation = rectTransform.rotation;
            clonedObject.GetComponent<RectTransform>().localScale = rectTransform.localScale;
            clonedObject.name = this.gameObject.name;

            var clonedRect = clonedObject.GetComponent<RectTransform>();
            clonedRect.position = rectTransform.position;
            clonedRect.rotation = rectTransform.rotation;
            clonedRect.localScale = rectTransform.localScale;

            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvas.transform as RectTransform,
                eventData.position,
                eventData.pressEventCamera,
                out var localPointerPos
            );

            offset = clonedRect.anchoredPosition - localPointerPos;
        }
        else
        {
            GameObject parentObject = transform.parent.gameObject;
            UIInventoryItem uIInventoryItem = parentObject.GetComponent<UIInventoryItem>();
            int index = uIInventoryItem.Index - 1;
            gameManager.Numeros.RemoveAt(index);
        }

        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out var localPoint
        );

        rectTransform.anchoredPosition = localPoint + offset;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

        if (!isAnchored)
        {
            clonedObject.name = this.gameObject.name;
        }

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
