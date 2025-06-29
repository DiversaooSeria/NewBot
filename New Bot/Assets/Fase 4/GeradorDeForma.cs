using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GeradorDeForma : MonoBehaviour, IPointerClickHandler
{
    public MecanicaReconhecimentoPadrao mecrec;
   
    RectTransform rectTransform;
    //public event Action<String> requestForma;

    [SerializeField] public MecanicaController.Formas forma;

    public GameObject objForma;

    public void Awake()
    {
        mecrec = FindController(this.transform, "MECREC").GetComponent<MecanicaReconhecimentoPadrao>();
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

    void Update()
    {
        
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (objForma == null) return;

        GameObject novaForma = Instantiate( objForma , this.rectTransform.anchoredPosition, Quaternion.identity);
        
        novaForma.GetComponent<Itens>().parentBeforeDrag = transform;
        novaForma.transform.SetParent(transform, false);   

        RectTransform novaFormaRect = novaForma.GetComponent<RectTransform>();
        novaFormaRect.SetParent(this.transform.parent, false);
    }
    
    public void SetForma(GameObject obj)
    {
        objForma = obj;
    }

    public GameObject FindController(Transform current, string tag)
    {
        if (current == null) return null;

        if (current.CompareTag(tag))
        {
            return current.gameObject;
        }

        return FindController(current.parent, tag); 
    }
}
