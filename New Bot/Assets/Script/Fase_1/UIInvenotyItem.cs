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
        public GameManager gameManager;
        public GameObject anchoredGameObj;
        public RectTransform rectTransform;
        public CanvasGroup canvasGroup;
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
            if (eventData.pointerDrag != null)
            {
                Directions draggedItem = eventData.pointerDrag.GetComponent<Directions>();
                if (draggedItem == null) return;

                draggedItem.dropWasSuccessful = true;

                if (anchoredGameObj == null)
                {
                    anchoredGameObj = Instantiate(eventData.pointerDrag, this.gameObject.transform, true);

                    anchoredGameObj.GetComponent<RectTransform>().position = rectTransform.position;
                    anchoredGameObj.GetComponent<RectTransform>().rotation = rectTransform.rotation;
                    anchoredGameObj.GetComponent<RectTransform>().localScale = rectTransform.localScale;
                    Directions newDirScript = anchoredGameObj.GetComponent<Directions>();
                    newDirScript.Anchor(this);

                    int directionValue = newDirScript.direction;

                    int index = this.Index - 1;
                    gameManager.Numeros.Insert(index, directionValue);
                    if (this.Index == inventoryPage.count)
                    {
                        inventoryPage.InitializeInventoryUI(1);
                    }
                }
                else
                {
                    Destroy(anchoredGameObj);

                    anchoredGameObj = Instantiate(eventData.pointerDrag, this.gameObject.transform, true);

                    anchoredGameObj.GetComponent<RectTransform>().position = rectTransform.position;
                    anchoredGameObj.GetComponent<RectTransform>().rotation = rectTransform.rotation;
                    anchoredGameObj.GetComponent<RectTransform>().localScale = rectTransform.localScale;

                    Directions newDirScript = anchoredGameObj.GetComponent<Directions>();
                    newDirScript.Anchor(this);

                    int directionValue = newDirScript.direction;
                    int index = this.Index - 1;
                    gameManager.Numeros[index] = directionValue;
                }
            }
        }
    }
}