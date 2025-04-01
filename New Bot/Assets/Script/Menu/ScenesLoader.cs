using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class ScenesLoader : MonoBehaviour
{
    public string lvlName;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            SceneManager.LoadScene(lvlName);

        }
    }
}
