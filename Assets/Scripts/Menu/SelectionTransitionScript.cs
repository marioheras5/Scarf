using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectionTransitionScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            GameObject players = GameObject.Find("SelectedPlayers");
            DontDestroyOnLoad(players);
            SceneManager.LoadScene("MainScene", LoadSceneMode.Single);
        }
    }
}
