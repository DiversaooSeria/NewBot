using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteracaoPLayer : MonoBehaviour
{
    //public event Action PressE;

    public MECREC mecrec;

    private void Start()
    {
        mecrec = FindMecrec(this.transform).GetComponent<MECREC>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            
            mecrec.gerente.DispararNaAreaDeInteracao();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKey(KeyCode.E))
        {
            mecrec.gerente.DispararInteragiu();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //SaiuAreaDeInteracao.Invoke();
            mecrec.gerente.DispararSaiuAreaDeInteracao();
        }
    }

    public GameObject FindMecrec(Transform current)
    {
        if (current == null) return null;

        if (current.gameObject.CompareTag("MECREC"))
        {
            return current.gameObject;
        }

        return FindMecrec(current.parent); // Chama recursivamente para o próximo pai
    }

}
