using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Inventory.UI;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using static MECRECGerenciador;

public class GeradorDePadrao : MonoBehaviour
{
    public GameObject itemPrefab;
    public RectTransform contentPanel;

    public List<GameObject> listOfUItems = new List<GameObject>();

    public int quantidade, quebra;
    public float x = -180, y = 124;

    public MECRECGerenciador gerente; // para requisições do ScriptableObject


    // Start is called before the first frame update
    void Start()
    {
        contentPanel = GetComponent<RectTransform>();
        itemPrefab = gerente.container;
        PreencheSequenciaPlayer();
        InitializeInventoryUI(quantidade);
        
    }

   
    public void InitializeInventoryUI(int inventorySize)
    {
        int contador = 0;
        for (int i = 1; i < inventorySize; i++)
        {
            GameObject uiItem = Instantiate(itemPrefab, contentPanel.gameObject.transform); // Define como filho do contentPanel
            RectTransform rectTransform = uiItem.GetComponent<RectTransform>();
            uiItem.GetComponent<DropContainer>().index = i - 1;

            // Ajusta a posição e a escala corretamente para UI
            rectTransform.anchoredPosition = new Vector2(x, y);
            rectTransform.localScale = Vector3.one;

            // Selecionando qual deve ser a forma apresentada
            if (Enum.IsDefined(typeof(Formas), gerente.sequenciaInicial[i - 1]) && gerente.sequenciaInicial[i - 1] != 4)
            {
                string nomeForma = Enum.GetName(typeof(Formas), gerente.sequenciaInicial[i - 1]);

                GameObject prefabForma = gerente.EntregaForma(nomeForma);
                if (prefabForma == null)
                {
                    Debug.LogWarning($"Forma '{nomeForma}' não encontrada em EntregaForma.");
                    return;
                }

                GameObject novaForma = Instantiate(prefabForma);
                uiItem.GetComponent<DropContainer>().OnDrop(novaForma);
                // Define o novo pai na hierarquia
                novaForma.transform.SetParent(uiItem.transform, false);

                // Configuração de UI para garantir posicionamento correto
                RectTransform novaFormaRect = novaForma.GetComponent<RectTransform>();
                if (novaFormaRect != null)
                {
                    novaFormaRect.anchoredPosition = Vector2.zero; // Centraliza dentro do uiItem
                    novaFormaRect.localScale = Vector3.one;       // Garante que a escala fique correta
                    novaForma.transform.SetAsLastSibling();       // Mantém a ordem correta na UI
                }
                else
                {
                    Debug.LogWarning("Nova forma não possui RectTransform.");
                }

                // Configuração do comportamento da forma
                Itens itemScript = novaForma.GetComponent<Itens>();
                if (itemScript != null)
                {
                    itemScript.parentBeforeDrag = uiItem.transform;
                    itemScript.locked = true; // O item não pode ser movido
                }
                else
                {
                    Debug.LogWarning("Nova forma não possui componente 'Itens'.");
                }

                // Marca o DropContainer como ocupado
                DropContainer dropContainer = uiItem.GetComponent<DropContainer>();
                if (dropContainer == null)
                {
                    Debug.LogWarning("uiItem não possui componente 'DropContainer'.");
                }
                else
                {
                    
                 
                }
            }



            listOfUItems.Add(uiItem);
            x += 180;
            contador++;
            if( i%quebra == 0 )
            {
                contador = 0;
                x = -180;
                y -= 124;
            }
        }
    }

    public void PreencheSequenciaPlayer()
    {
        gerente.sequenciaPlayer = new List<int>(new int[quantidade-1]); // preenche com valores nulos 
    }
    
}
