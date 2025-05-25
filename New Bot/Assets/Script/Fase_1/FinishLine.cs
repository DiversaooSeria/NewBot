using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinishLine : MonoBehaviour
{
    public Text winText; 

    private void Start()
    {
        winText.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            winText.gameObject.SetActive(true);
            winText.text = "Você ganhou!";

        }
    }
}
