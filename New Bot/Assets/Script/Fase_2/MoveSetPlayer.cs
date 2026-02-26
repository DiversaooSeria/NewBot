using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class MoveSetPlayer : MonoBehaviour
{
    [SerializeField] private Button botao;
    [SerializeField] private GameObject botaoMap;
    private GameObject Player;
    [SerializeField] private GameObject otherA;
    [SerializeField] private GameObject otherB;

    public float addX = 14f;
    public float addY = 4f;
    


    private void Awake()
    {
        botao.onClick.AddListener(OnClickButton);
        Player = GameObject.Find("Player");
        
    }


    void Start()
    {
        
    }

    void Update()
    {
        if (DragDropPhase2.moving)
        {
            botaoMap.SetActive(false);
            otherA.SetActive(false);
            otherB.SetActive(false);
        }
    }

    private void OnClickButton()
    {
        botaoMap.transform.position = new Vector2(Player.transform.position.x + addX, Player.transform.position.y+addY);
        if (botaoMap.activeInHierarchy)
        {
            botaoMap.SetActive(false);
        }
        else
        {
            botaoMap.SetActive(true);
            otherA.SetActive(false);
            otherB.SetActive(false);

        }
    }

}
