using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Itens : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    
    public Transform parentBeforeDrag; // O ultimo parente antes de ser puxado pelo player
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private RectTransform rectTransform;
    private Canvas canvas;
    public Vector2 posicaoInicial; // posi��o para ele voltar caso o Container esteja invalido
    public bool locked = false; // Trava do item para ele ficar "paralisado"

    [SerializeField] public MecanicaController.Formas forma;

    public void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();        // Obt�m o Canvas automaticamente
        posicaoInicial = Vector2.zero;
        
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (locked) return;
        if (canvas == null || canvasGroup == null)
        {
            Start();
        }
        if( this.transform.parent.CompareTag("DropContainer"))
        {
            this.transform.parent.GetComponent<DropContainer>().isAnchored = false;
            this.transform.parent.GetComponent<DropContainer>().ChangeArchoned(); // isAnchorned is false, value = 4: void
        }
        transform.SetParent(canvas.transform, true); // Mant�m a hierarquia correta
        canvasGroup.blocksRaycasts = false; // Permite que containers detectem o drop
        canvasGroup.alpha = 0.5f; // Deixa a imagem semi-transparente
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (locked) return;
        if (rectTransform == null || canvas == null  ) return;
        // Converte a posi��o do mouse para coordenadas locais do Canvas
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

        // Tenta detectar um DropContainer v�lido
        GameObject dropTarget = GetDropTarget(eventData);
        
        if ( dropTarget != null && !dropTarget.GetComponent<DropContainer>().isAnchored )
        {
            transform.SetParent(dropTarget.transform, false); // Define o container como novo pai

            // Converte a posi��o do mouse para o espa�o local do novo container
            Vector2 localPoint;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                dropTarget.transform as RectTransform, eventData.position, eventData.pressEventCamera, out localPoint
            );

            rectTransform.anchoredPosition = localPoint;
            dropTarget.GetComponent<DropContainer>().OnDrop(this.gameObject);
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
            Transform container = this.transform.parent; // Obt�m o pai do item na hierarquia

            if (container != null)
            {
                Debug.Log("Foi");
                DropContainer dropContainer = container.GetComponent<DropContainer>();
                if (dropContainer != null)
                {
                    dropContainer.isAnchored = false; // Marca o container como vazio
                }
            }
            Destroy(this.gameObject);

        }

    }
}