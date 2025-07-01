using UnityEngine;

public class MecanicaReconhecimentoPadrao : MonoBehaviour
{
    public MecanicaController gerente;
    public Transform areaDeInteracao, menu;

    private void Awake()
    {
        areaDeInteracao = FindDeepChild(transform, "Botao de interacao");
        menu = FindDeepChild(transform, "Menu Rec");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            BotaoSair();
        }
    }
    private void OnEnable()
    {
        gerente.naAreaDeInteracao.AddListener(EntrouAreaDeInteracao);
        gerente.saiuAreaDeInteracao.AddListener(SaiuAreaDeInteracao);
        gerente.interagiu.AddListener(Interagiu);
        gerente.changeInContainer += AtualizaSequenciaPlayer;

    }
    private void OnDisable()
    {
        gerente.naAreaDeInteracao.RemoveListener(EntrouAreaDeInteracao);
        gerente.saiuAreaDeInteracao.RemoveListener(SaiuAreaDeInteracao);
        gerente.interagiu.RemoveListener(Interagiu);

        gerente.changeInContainer -= AtualizaSequenciaPlayer;
    }

    public void EntrouAreaDeInteracao()
    {
        
        areaDeInteracao.gameObject.SetActive(true);
    }
    public void SaiuAreaDeInteracao()
    {
        
        areaDeInteracao.gameObject.SetActive(false);
    }
    public void Interagiu()
    {
        areaDeInteracao.gameObject.SetActive(false);
        menu.gameObject.SetActive(true);
    }

    public void AtualizaSequenciaPlayer(int index, int valor)
    {
        gerente.sequenciaPlayer[index] = valor;
    }
    
    public void BotaoSair()
    {
        menu.gameObject.SetActive(false);
    }

    public void BotaoOK()
    {
        gerente.CheckSequence();
    }

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
