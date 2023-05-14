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
            GameObject[] p = new GameObject[players.transform.childCount];
            for (int i = 0; i < p.Length; i++)
            {
                p[i] = players.transform.GetChild(i).gameObject;
            }
            SceneInfoManager.players = p;
            SceneManager.LoadScene("MainScene", LoadSceneMode.Single);
        }
    }
}
public static class SceneInfoManager
{
    public static GameObject[] players;
}
