using Inventory.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Directions : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] public Canvas canvas;
    [SerializeField] public DirectionData directionData;

    private RectTransform rectTransform;
    public CanvasGroup canvasGroup;

    public bool isAnchored;
    private GameManager gameManager;
    private GameObject clonedObject;

    [HideInInspector]
    public UIInventoryItem parentPlaceholder;
    [SerializeField] public UIInventoryPage inventoryPage;

    private Vector2 offset;
    public int direction;

    [HideInInspector]
    public bool dropWasSuccessful = false;

    private GameObject animObject;

    private void Awake()
    {
        direction = directionData.directionValue;
        isAnchored = false;
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        gameManager = FindObjectOfType<GameManager>();

        if (inventoryPage == null)
        {
            inventoryPage = FindObjectOfType<UIInventoryPage>();
        }

        animObject = GameObject.Find("Anim");
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        dropWasSuccessful = false;

        if (animObject != null)
        {
            animObject.SetActive(false);
        }

        if (!isAnchored)
        {
            clonedObject = Instantiate(this.gameObject);
            clonedObject.transform.SetParent(canvas.transform);
            clonedObject.name = this.gameObject.name;

            var clonedRect = clonedObject.GetComponent<RectTransform>();

            clonedRect.position = rectTransform.position;
            clonedRect.rotation = rectTransform.rotation;

            clonedRect.localScale = new Vector3(0.45f, 0.45f, 0.45f);

            var cloneCanvasGroup = clonedObject.GetComponent<CanvasGroup>();
            if (cloneCanvasGroup == null) cloneCanvasGroup = clonedObject.AddComponent<CanvasGroup>();
            cloneCanvasGroup.alpha = 0.6f;
            cloneCanvasGroup.blocksRaycasts = false;
        }
        else
        {
            int index = parentPlaceholder.Index - 1;
            gameManager.Numeros.RemoveAt(index);

            parentPlaceholder.anchoredGameObj = null;

            transform.SetParent(canvas.transform);

            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvas.transform as RectTransform,
                eventData.position,
                eventData.pressEventCamera,
                out var localPointerPos
            );
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvas.transform as RectTransform,
                rectTransform.position,
                eventData.pressEventCamera,
                out var localObjectPos
            );
            offset = localObjectPos - localPointerPos;

            canvasGroup.alpha = 0.6f;
            canvasGroup.blocksRaycasts = false;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out var localPoint
        );

        if (clonedObject != null)
        {
            clonedObject.GetComponent<RectTransform>().anchoredPosition = localPoint + offset;
        }
        else
        {
            rectTransform.anchoredPosition = localPoint + offset;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (isAnchored)
        {
            if (dropWasSuccessful)
            {
                Destroy(this.gameObject);
            }
            else
            {
                inventoryPage.RemoveItem(parentPlaceholder);
                Destroy(this.gameObject);
            }
        }
        else
        {
            if (clonedObject != null)
            {
                var cloneScript = clonedObject.GetComponent<Directions>();
                if (cloneScript == null || !cloneScript.dropWasSuccessful)
                {
                    Destroy(clonedObject);
                }
                else
                {
                    Destroy(clonedObject);
                }
                clonedObject = null;
            }
        }
    }

    public void Anchor(UIInventoryItem ph)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

        isAnchored = true;
        parentPlaceholder = ph;

        ph.anchoredGameObj = this.gameObject;

        transform.SetParent(ph.transform);
        rectTransform.anchoredPosition = Vector2.zero;
        rectTransform.localScale = Vector3.one;
    }
}