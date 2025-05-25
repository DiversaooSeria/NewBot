using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Esteira_Buttons : MonoBehaviour
{
    [SerializeField] private Button buttonRight;
    [SerializeField] private Button buttonLeft;
    [SerializeField] private Canvas canva;
    public GameObject esteira;
    // Start is called before the first frame update
    void Start()
    {
        buttonLeft.onClick.AddListener(ButtonLeft);
        buttonRight.onClick.AddListener(ButtonRight);
        canva.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void ButtonRight() 
    {
        if (esteira.transform.position.x < 10)
        {
            esteira.transform.position = new Vector2(esteira.transform.position.x + 10, esteira.transform.position.y);
        }
        
    }
    private void ButtonLeft()
    {
        if (esteira.transform.position.x > -10)
        {
            esteira.transform.position = new Vector2(esteira.transform.position.x - 10, esteira.transform.position.y);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            canva.enabled = false;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        canva.enabled = true;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            canva.enabled = false;
        }
    }
}
