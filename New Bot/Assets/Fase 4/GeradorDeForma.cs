using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GeradorDeForma : MonoBehaviour, IPointerClickHandler
{
    public MECRECGerenciador gerente;
    RectTransform rectTransform;
    //public event Action<String> requestForma;

    [SerializeField] public MECRECGerenciador.Formas forma;

    public GameObject objForma;
    

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }

    void Start()
    {
        SetForma( gerente.EntregaForma( forma.ToString() ) );
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

}
