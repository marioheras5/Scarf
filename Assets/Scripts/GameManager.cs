using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] players;
    void Start()
    {
        InitializePlayers();
    }
    void InitializePlayers()
    {
        players = SceneInfoManager.players;

        GameObject parent = GameObject.Find("Players");
        for (int i = 0;i < players.Length; i++)
        {
            Instantiate(players[i], parent.transform);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
