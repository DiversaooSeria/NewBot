using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropContainer : MonoBehaviour, IDropHandler, ContainerInterface
{
    public MECRECGerenciador gerente;

    public int index;   // posicao/indice do container
    public bool isAnchored = false; // esta preenchido

    public void OnDrop(PointerEventData eventData)
    {
        if ( isAnchored == false ) return;        //Se está prenchido nada acontece

        GameObject droppedObject = eventData.pointerDrag; // Pega o objeto que está sendo arrastado
        if ( droppedObject != null )
        {
            droppedObject.transform.SetParent(transform); // Define como filho do container
            droppedObject.transform.localPosition = Vector3.zero; // Centraliza dentro do container
            isAnchored = true;
            //tocar evento de preenchimento do padrão realizado
        }
    }
}

