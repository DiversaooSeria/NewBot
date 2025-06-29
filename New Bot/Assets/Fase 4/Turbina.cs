using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turbina : MonoBehaviour
{
    public MecanicaReconhecimentoPadrao mecrec;
    
    private void OnEnable()
    {
        mecrec.gerente.finalizado.AddListener(Finalizado);
    }
    private void OnDisable()
    {
        mecrec.gerente.finalizado.RemoveListener(Finalizado);
    }

    
    private void Finalizado()
    {
        GetComponent<Animator>().SetBool("Ativado", true);
    }
}
