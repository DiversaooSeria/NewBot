using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "NovoGerenciador", menuName = "MECREC/Gerenciador")]
public class MecanicaController : ScriptableObject
{

    [SerializeField] public List<int> sequenciaCorreta = new List<int>();
    [SerializeField] public List<int> sequenciaInicial = new List<int>();
    [SerializeField] public List<int> sequenciaPlayer;

    public UnityEvent naAreaDeInteracao;
    public UnityEvent saiuAreaDeInteracao;
    public UnityEvent interagiu, finalizado;

    public event Action<int, int> changeInContainer;


    public List<GameObject> Itens = new List<GameObject>();
    public GameObject container;
    public Formas formas;

    
    public enum Formas
    {
        Triangulo = 1,
        Quadrado = 2,
        Circulo = 3,
        Vazio = 4,
    }
    public void DispararNaAreaDeInteracao() => naAreaDeInteracao?.Invoke();
    public void DispararSaiuAreaDeInteracao() => saiuAreaDeInteracao?.Invoke();
    public void DispararInteragiu() => interagiu?.Invoke();

    public void DispararMudancaNoContainer(int a, int b) => changeInContainer?.Invoke(a,b);

    

    public GameObject EntregaForma(String forma)
    {
        GameObject obj = null;
        if (Enum.TryParse(forma, true, out Formas formaEncontrada))
        {
            obj = Itens[(int)formaEncontrada - 1 ];            
        }
        return obj;
    }
    

    public int ConverteFormaInteiro(GameObject item)
    {
        return (int)item.GetComponent<Itens>().forma ;
    }

    public void CheckSequence()
    {
        for(int i = 0;i < sequenciaCorreta.Count;i++)
        {
            if (sequenciaCorreta[i] != sequenciaPlayer[i])
            {
                Debug.Log("Errou!");
                return;
            }
        }
        Debug.Log("Sucesso");
        finalizado?.Invoke();
    }
}
