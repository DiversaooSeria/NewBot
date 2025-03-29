using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Itens : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    
    public Transform parentBeforeDrag; // O ultimo parente antes de ser puxado pelo player
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private RectTransform rectTransform;
    private Canvas canvas;
    public Vector2 posicaoInicial; // posição para ele voltar caso o Container esteja invalido
    public bool locked = false; // Trava do item para ele ficar "paralisado"
   

    public void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();        // Obtém o Canvas automaticamente
        posicaoInicial = Vector2.zero;
        
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (locked) return;
        if (canvas == null || canvasGroup == null)
        {
            Start();
        }
        transform.SetParent(canvas.transform, true); // Mantém a hierarquia correta
        canvasGroup.blocksRaycasts = false; // Permite que containers detectem o drop
        canvasGroup.alpha = 0.5f; // Deixa a imagem semi-transparente
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (locked) return;
        if (rectTransform == null || canvas == null  ) return;
        // Converte a posição do mouse para coordenadas locais do Canvas
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform, eventData.position, eventData.pressEventCamera, out localPoint
        );

        rectTransform.anchoredPosition = localPoint;

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if(locked) return;

        canvasGroup.blocksRaycasts = true; // Permite que o item seja detectado novamente
        canvasGroup.alpha = 1f;            // Restaura a opacidade

        // Tenta detectar um DropContainer válido
        GameObject dropTarget = GetDropTarget(eventData);
        
        if (dropTarget != null  )
        {
            transform.SetParent(dropTarget.transform, false); // Define o container como novo pai

            // Converte a posição do mouse para o espaço local do novo container
            Vector2 localPoint;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                dropTarget.transform as RectTransform, eventData.position, eventData.pressEventCamera, out localPoint
            );

            rectTransform.anchoredPosition = localPoint;
        }
        else
        {
            transform.transform.SetParent(parentBeforeDrag, false); // Volta ao local original
            rectTransform.anchoredPosition = posicaoInicial; // Garante que fique no lugar certo
        }
    }

    private GameObject GetDropTarget(PointerEventData eventData)
    {
        PointerEventData pointerData = new PointerEventData(EventSystem.current)
        {
            position = eventData.position
        };

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);

        foreach (RaycastResult result in results)
        {
            if ( result.gameObject.CompareTag("DropContainer") )
            {
                return result.gameObject;
            }
        }
        return null;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData != null && eventData.clickCount == 2 && !locked) // para deletar caso clique duas vezes
        {
            Destroy(this.gameObject);
        }

    }
}