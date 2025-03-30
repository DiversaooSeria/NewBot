using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropContainer : MonoBehaviour
{
    public MECRECGerenciador gerente;

    public int index;   // posicao/indice do container
    public bool isAnchored = false; // esta preenchido

    public void OnDrop(GameObject item)
    {
        if ( isAnchored == true ) return;        //Se está prenchido nada acontece
             
        if ( item != null )
        {
            item.transform.SetParent(transform); // Define como filho do container
            item.transform.localPosition = Vector3.zero; // Centraliza dentro do container
            isAnchored = true;
            ChangeArchoned( gerente.ConverteFormaInteiro(item.gameObject) );
        }
    }

    


    public void ChangeArchoned(int value = 4)
    {
        gerente.DispararMudancaNoContainer(index, value);
    }




}

