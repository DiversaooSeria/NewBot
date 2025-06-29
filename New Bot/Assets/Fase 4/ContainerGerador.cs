using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ContainerGerador : MonoBehaviour
{
    
    public void OnDrop(PointerEventData eventData)
    {
        GameObject droppedObject = eventData.pointerDrag; 
        if (droppedObject != null && droppedObject.GetComponent<Itens>() != null)
        {
            droppedObject.transform.SetParent(transform); 
            droppedObject.transform.localPosition = Vector3.zero; 

        }
    }
}
