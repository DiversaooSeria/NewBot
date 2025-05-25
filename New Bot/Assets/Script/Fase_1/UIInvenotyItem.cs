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
        private GameManager gameManager;

        public GameObject anchoredGameObj;

        private RectTransform rectTransform;

        private CanvasGroup canvasGroup;

        private UIInventoryPage inventoryPage;

        public int Index;
        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            canvasGroup = GetComponent<CanvasGroup>();
            gameManager = FindObjectOfType<GameManager>();
            inventoryPage = FindObjectOfType<UIInventoryPage>();
        }
        public void OnDrop(PointerEventData eventData)
        {
            if (eventData.pointerDrag != null && anchoredGameObj == null)
            {
                anchoredGameObj = Instantiate(eventData.pointerDrag, this.gameObject.transform, true);
                anchoredGameObj.GetComponent<RectTransform>().position = rectTransform.position;
                anchoredGameObj.GetComponent<RectTransform>().rotation = rectTransform.rotation;
                anchoredGameObj.GetComponent<RectTransform>().localScale = rectTransform.localScale;
                anchoredGameObj.GetComponent<Direction>().Anchor(this.gameObject);

                int directionValue = anchoredGameObj.GetComponent<Direction>().direction;

                inventoryPage.InitializeInventoryUI(1);

                gameManager.Numeros.Add(directionValue);
            }

            if (eventData.pointerDrag != null && anchoredGameObj != null)
            {
                Destroy(anchoredGameObj);
                anchoredGameObj = Instantiate(eventData.pointerDrag, this.gameObject.transform, true);
                anchoredGameObj.GetComponent<RectTransform>().position = rectTransform.position;
                anchoredGameObj.GetComponent<RectTransform>().rotation = rectTransform.rotation;
                anchoredGameObj.GetComponent<RectTransform>().localScale = rectTransform.localScale;
                anchoredGameObj.GetComponent<Direction>().Anchor(this.gameObject);

                int directionValue = anchoredGameObj.GetComponent<Direction>().direction;

                int index = this.Index - 1;
                gameManager.Numeros[index] = directionValue;
            }
        }
    }
}