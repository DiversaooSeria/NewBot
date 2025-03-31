using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropContainer : MonoBehaviour
{
    public MECREC mecrec;

    public int index;   // posicao/indice do container
    public bool isAnchored = false; // esta preenchido

    public void Awake()
    {
        mecrec = FindMecrec(this.transform).GetComponent<MECREC>();
    }
    public void OnDrop(GameObject item)
    {
        if ( isAnchored == true ) return;        //Se está prenchido nada acontece
             
        if ( item != null )
        {
            item.transform.SetParent(transform); // Define como filho do container
            item.transform.localPosition = Vector3.zero; // Centraliza dentro do container
            isAnchored = true;
            ChangeArchoned( mecrec.gerente.ConverteFormaInteiro(item.gameObject) );
        }
    }

    
    public void ChangeArchoned(int value = 4)
    {
        mecrec.gerente.DispararMudancaNoContainer(index, value);
    }


    public GameObject FindMecrec(Transform current)
    {
        if (current == null) return null;

        if (current.CompareTag("MECREC"))
        {
            return current.gameObject;
        }

        return FindMecrec(current.parent); // Chama recursivamente para o próximo pai
    }



}

