using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GeradorDeForma : MonoBehaviour, IPointerClickHandler
{
    public MECREC mecrec;
   
    RectTransform rectTransform;
    //public event Action<String> requestForma;

    [SerializeField] public MECRECGerenciador.Formas forma;

    public GameObject objForma;

    public void Awake()
    {
        mecrec = FindMecrec(this.transform).GetComponent<MECREC>();
    }
    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }

    void Start()
    {
        SetForma( mecrec.gerente.EntregaForma( forma.ToString() ) );
        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (objForma == null) return;

        // Criar a nova instância
        GameObject novaForma = Instantiate( objForma , this.rectTransform.anchoredPosition, Quaternion.identity);
        
        novaForma.GetComponent<Itens>().parentBeforeDrag = transform;
        novaForma.transform.SetParent(transform, false);   

        // Configurar a nova forma dentro do Canvas
        RectTransform novaFormaRect = novaForma.GetComponent<RectTransform>();
        novaFormaRect.SetParent(this.transform.parent, false);
    }
    
    public void SetForma(GameObject obj)
    {
        objForma = obj;
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
