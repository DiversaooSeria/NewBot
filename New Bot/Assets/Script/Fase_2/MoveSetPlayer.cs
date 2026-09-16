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

    [SerializeField] private GameObject barco;
    public Sprite ImagemBarco;

    public GameObject ObjetosColidiveis;
    public bool PularObstaculos;



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
        botaoMap.transform.position = new Vector2(Player.transform.position.x + 3.5f, Player.transform.position.y+0.3f);
        if (botaoMap.activeInHierarchy)
        {
            botaoMap.SetActive(false);
            ObjetosColidiveis.GetComponent<TilemapCollider2D>().isTrigger = false;
        }
        else
        {
            botaoMap.SetActive(true);
            otherA.SetActive(false);
            otherB.SetActive(false);

            barco.GetComponent<SpriteRenderer>().sprite = ImagemBarco;
            if(PularObstaculos)
            {
                ObjetosColidiveis.GetComponent<TilemapCollider2D>().isTrigger = true;
            }
            else
            {
                ObjetosColidiveis.GetComponent<TilemapCollider2D>().isTrigger = false;
            }
            

        }
    }

}
