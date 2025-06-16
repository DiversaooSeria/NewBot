using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialManager : MonoBehaviour
{
    [Header("Referências")]
    public GameObject tutorialPanel;
    public TMP_Text tutorialText;

    [Header("Configurações do Tutorial")]
    [TextArea]
    public List<string> mensagensTutorial = new List<string>()
    {
        "Bem-vindo! Aos labirintos nos trilhos.",
        "Clique nas setas larajas para mover os trens",
        "Boa sorte nessa missão."
    };

    private int mensagemIndex = 0;
    private Player player;
    private float velocidadeOriginal;

    void Start()
    {
        GameObject playerObject = GameObject.Find("Player");
        if (playerObject != null)
        {
            player = playerObject.GetComponent<Player>();
            if (player != null)
            {
                velocidadeOriginal = player.moveSpeed;
            }
        }
        else
        {
            Debug.LogError("Player não encontrado!");
        }

        tutorialPanel.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (player != null)
            {
                player.moveSpeed = 0f;
            }

            mensagemIndex = 0;
            tutorialPanel.SetActive(true);
            tutorialText.text = mensagensTutorial[mensagemIndex];
        }
    }

    void Update()
    {
        if (tutorialPanel.activeSelf && Input.GetKeyDown(KeyCode.Space))
        {
            mensagemIndex++;

            if (mensagemIndex < mensagensTutorial.Count)
            {
                tutorialText.text = mensagensTutorial[mensagemIndex];
            }
            else
            {
                tutorialPanel.SetActive(false);
                if (player != null)
                {
                    player.moveSpeed = velocidadeOriginal;
                }

                Destroy(gameObject);
            }
        }
    }
}
