using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject tutorialPanel;
    [SerializeField] private TMP_Text tutorialText;

    [Header("Tutorial Messages")]
    [TextArea]
    [SerializeField] private List<string> tutorialMessages = new();

    [Header("References")]
    [SerializeField] private Player player;

    private int currentMessageIndex;
    private bool tutorialStarted;

    private void Awake()
    {
        if (player == null)
        {
            player = FindFirstObjectByType<Player>();
        }
    }

    private void Start()
    {
        tutorialPanel.SetActive(false);

        // Caso a fase já comece com um tutorial
        if (SceneManager.GetActiveScene().name == "Fase1_Algo")
        {
            tutorialStarted = true;
            BeginTutorial();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (tutorialStarted)
            return;

        if (!other.CompareTag("Player"))
            return;

        tutorialStarted = true;
        BeginTutorial();
    }

    private void Update()
    {
        if (!tutorialPanel.activeSelf)
            return;

        if (!Input.GetKeyDown(KeyCode.Space))
            return;

        currentMessageIndex++;

        if (currentMessageIndex < tutorialMessages.Count)
        {
            tutorialText.text = tutorialMessages[currentMessageIndex];
        }
        else
        {
            EndTutorial();
        }
    }

    private void BeginTutorial()
    {
        Debug.Log("Starting Tutorial");

        currentMessageIndex = 0;

        player?.DisableMovement();

        tutorialPanel.SetActive(true);

        if (tutorialMessages.Count > 0)
        {
            tutorialText.text = tutorialMessages[currentMessageIndex];
        }
    }

    private void EndTutorial()
    {
        tutorialPanel.SetActive(false);

        player?.EnableMovement();

        Destroy(gameObject);
    }
}