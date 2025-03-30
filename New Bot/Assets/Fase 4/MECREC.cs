using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MECREC : MonoBehaviour
{
    public MECRECGerenciador gerente;
    public Transform AreaDeInteracao, Menu;

    
    private void OnEnable()
    {
        // Eventos para detecção de aproximação do player
        gerente.naAreaDeInteracao.AddListener(EntrouAreaDeInteracao);
        gerente.saiuAreaDeInteracao.AddListener(SaiuAreaDeInteracao);
        gerente.interagiu.AddListener(Interagiu);
        gerente.changeInContainer += AtualizaSequenciaPlayer;

    }
    private void OnDisable()
    {
        // Eventos para detecção de aproximação do player
        gerente.naAreaDeInteracao.RemoveListener(EntrouAreaDeInteracao);
        gerente.saiuAreaDeInteracao.RemoveListener(SaiuAreaDeInteracao);
        gerente.interagiu.RemoveListener(Interagiu);

        gerente.changeInContainer -= AtualizaSequenciaPlayer;
    }
    // Start is called before the first frame update
    void Start()
    {
        AreaDeInteracao = FindDeepChild(transform, "Botao de interacao");
        Menu = FindDeepChild(transform, "Menu Rec");
    }

    

    // Funções para 
    public void EntrouAreaDeInteracao()
    {
        
        AreaDeInteracao.gameObject.SetActive(true);
    }
    public void SaiuAreaDeInteracao()
    {
        
        AreaDeInteracao.gameObject.SetActive(false);
    }
    public void Interagiu()
    {
        AreaDeInteracao.gameObject.SetActive(false);
        Menu.gameObject.SetActive(true);
    }

    public void AtualizaSequenciaPlayer(int index, int valor)
    {
        gerente.sequenciaPlayer[index] = valor;
    }
    

    //Função para encontrar os filhos de MECREC que é um encapsulador.(Filhos são os outros gameObject dentro de MECREC)
    Transform FindDeepChild(Transform parent, string name)
    {
        foreach (Transform child in parent)
        {
            if (child.name == name)
                return child;

            Transform found = FindDeepChild(child, name);
            if (found != null)
                return found;
        }
        return null;
    }
}
