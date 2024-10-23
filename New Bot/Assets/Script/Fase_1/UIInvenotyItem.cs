using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.EventSystems;

namespace Inventory.UI
{

    public class UIInventoryItem : MonoBehaviour, IDropHandler
    {
        public GameObject anchoredGameObj;

        private RectTransform rectTransform;

        private CanvasGroup canvasGroup;
        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            canvasGroup = GetComponent<CanvasGroup>();
        }
        public void OnDrop(PointerEventData eventData)
        {
            Debug.Log("Drop");
            if (eventData.pointerDrag != null && anchoredGameObj == null)
            {
                anchoredGameObj = Instantiate(eventData.pointerDrag, this.gameObject.transform, true);
                anchoredGameObj.GetComponent<RectTransform>().position = rectTransform.position;
                anchoredGameObj.GetComponent<RectTransform>().rotation = rectTransform.rotation;
                anchoredGameObj.GetComponent<RectTransform>().localScale = rectTransform.localScale;
                anchoredGameObj.GetComponent<Direction>().Anchor(this.gameObject);

                // Adiciona � fila de a��es uma posi��o ap�s a posi��o atual na fila.
                //GameObject.Find("PainelAcoes").GetComponent<PainelAcoes>().Add(queuePosition + 1);

                // Adiciona � fila de arrasto o objeto ancorado e a posi��o atual na fila.
                //GameObject.Find("PainelAcoes").GetComponent<Fila>().Add(anchoredGameObj, queuePosition);
            }
        }
    }
}