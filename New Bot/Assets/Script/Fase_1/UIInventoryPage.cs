using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Inventory.UI
{
    public class UIInventoryPage : MonoBehaviour
    {
        [SerializeField]
        public UIInventoryItem itemPrefab;

        [SerializeField]
        private RectTransform contentPanel;

        public List<UIInventoryItem> listOfUIItems = new List<UIInventoryItem>();

        private GameObject anchoredGameObj;

        private RectTransform rectTransform;

        public Sprite image;
        public int quantity;

        public int count;
        private void Awake()
        {
            InitializeInventoryUI(1);
        }

        public void InitializeInventoryUI(int inventorysize)
        {
            for (int i = 0; i < inventorysize; i++)
            {
                UIInventoryItem uiItem = Instantiate(itemPrefab, contentPanel);
                uiItem.transform.localScale = Vector3.one;
                uiItem.transform.localPosition = Vector3.zero;

                RectTransform rectTransform = uiItem.GetComponent<RectTransform>();
                rectTransform.anchoredPosition3D = Vector3.zero;
                rectTransform.localScale = Vector3.one;

                count++;
                uiItem.Index = count;

                listOfUIItems.Add(uiItem);

            }
        }
        public void RemoveItem(UIInventoryItem itemToRemove)
        {
            if (itemToRemove != null && listOfUIItems.Contains(itemToRemove))
            {
                listOfUIItems.Remove(itemToRemove);

                Destroy(itemToRemove.gameObject);

                count = listOfUIItems.Count;

                ReIndexItems();
            }
            else
            {
                Debug.LogWarning("Tentativa de remover um item que não está na lista.");
            }
        }

        private void ReIndexItems()
        {
            for (int i = 0; i < listOfUIItems.Count; i++)
            {
                if (listOfUIItems[i] != null)
                {
                    listOfUIItems[i].Index = i + 1;
                }
            }
        }
    }
}