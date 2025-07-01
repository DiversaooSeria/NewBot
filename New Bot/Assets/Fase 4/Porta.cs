using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Porta : MonoBehaviour
{
    public MecanicaReconhecimentoPadrao mecrec;

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
        GetComponent<Animator>().SetBool("open", true);
    }
}
