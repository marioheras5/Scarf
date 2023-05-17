using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject[] players;
    public GameObject counter;
    int kills;

    void Start()
    {
        InitializePlayers();
    }
    void InitializePlayers()
    {
        GameObject players = GameObject.Find("Players");
        GameObject selections = GameObject.Find("SelectedPlayers");
        for (int i = 0; i < selections.transform.childCount; i++)
        {
            GameObject player = selections.transform.GetChild(i).gameObject;
            player.transform.parent = players.transform;
            player.transform.position = Vector3.zero;

            // Barra de vida
            GameObject healthBar = GameObject.Find("HUD").transform.GetChild(i).gameObject;
            healthBar.SetActive(true);
            GameObject empty = healthBar.transform.GetChild(0).gameObject;
            GameObject full = healthBar.transform.GetChild(1).gameObject;

            string charName = GetCharacterName(player);
            player.GetComponentInChildren<PlayerStats>().healthBar = full;
            empty.GetComponent<Image>().sprite = Resources.Load<Sprite>($"marco{charName}Empty");
            full.GetComponent<Image>().sprite = Resources.Load<Sprite>($"marco{charName}");

            // Cursor
            GameObject cursorManager = GameObject.Find("CursorManager");
            cursorManager.GetComponent<CursorManager>().player = player.GetComponentInChildren<PlayerStats>().transform;
        }
        GameObject[] playersTag = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < playersTag.Length; i++)
        {
            playersTag[i].transform.position = Vector3.zero;
        }
    }
    string GetCharacterName(GameObject player)
    {
        GameObject go = player.GetComponentInChildren<PlayerStats>().gameObject;
        if (go.name.Contains("Mage"))
        {
            return "Mago";
        }
        if (go.name.Contains("Dino"))
        {
            return "Dino";
        }
        if (go.name.Contains("Doc"))
        {
            return "Doc";
        }
        if (go.name.Contains("Knight"))
        {
            return "Knight";
        }
        return "Doc";
    }
    public void AddKill()
    {
        kills++;
        counter.GetComponent<TextMeshProUGUI>().text = kills.ToString();
    }
}
