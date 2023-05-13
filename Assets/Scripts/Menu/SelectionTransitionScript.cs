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
            GameObject players = GameObject.Find("Players");
            GameObject p1 = players.transform.GetChild(0).gameObject;
            SceneInfoManager.players = new GameObject[] { p1 };
            SceneManager.LoadScene("MainScene", LoadSceneMode.Single);
        }
    }
}
public static class SceneInfoManager
{
    public static GameObject[] players;
}
