using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public List<int> Numeros = new List<int>();
    public PlayerPhase1 player;
    public GameObject animObject;
    public GameObject tutorialPanelObj;

    private void Start()
    {
        player = FindObjectOfType<PlayerPhase1>();
        string sceneName = SceneManager.GetActiveScene().name;

        if (tutorialPanelObj != null)
        {
            tutorialPanelObj.SetActive(sceneName == "Fase 1");
        }
        if (animObject != null)
        {
            animObject.SetActive(false);
        }

    }

    public void OnButtonClick()
    {
        player.StartCoroutine(Timer());
    }
    public void OnTutorialExitClick()
    {
        tutorialPanelObj.SetActive(false);
        animObject.SetActive(true); 
    }


    IEnumerator Timer()
    {
        for (int i = 0; i < Numeros.Count; i++)
        {
            int x = Numeros[i];
            player.MoveCharacter(x);

            yield return new WaitForSeconds(1);
        }
    }
}
