using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turbina : MonoBehaviour
{
    public MECREC mecrec;
    
   
    
    private void OnEnable()
    {
        mecrec.gerente.finalizado.AddListener(finalizado);
    }
    private void OnDisable()
    {
        mecrec.gerente.finalizado.RemoveListener(finalizado);
    }

    
    private void finalizado()
    {
        GetComponent<Animator>().SetBool("Ativado", true);
    }
}
