using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    public GameObject tutorialPanel;
    public TMP_Text tutorialText;

    [Header("Tutorial Messages")]
    [TextArea]
    public List<string> mensagensTutorial = new List<string>()
    {   
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
            Debug.LogError("Player not found!");
        }

        tutorialPanel.SetActive(false);
        if (SceneManager.GetActiveScene().name == "Fase1_Algo")
        {
            StartTutorial();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (player != null)
            {
                player.moveSpeed = 0f;
            }

            Debug.Log("Collission Detected with Player");
            StartTutorial();
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

    void StartTutorial()
    {
        Debug.Log("Starting Tutorial");
        mensagemIndex = 0;
        tutorialPanel.SetActive(true);
        tutorialText.text = mensagensTutorial[mensagemIndex];
    }
}
