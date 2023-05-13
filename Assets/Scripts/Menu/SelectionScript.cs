using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SelectionScript : MonoBehaviour
{
    public GameObject[] characters;
    public GameObject[] players;
    public GameObject character;
    public GameObject background;
    int index;
    bool selected;
    bool canChange = true;
    GameObject currentCharacter;

    void Start()
    {
        
    }
    void Update()
    {
        if (selected)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                characters[index].SetActive(true);
                background.SetActive(true);
                selected = false;
                if (currentCharacter.transform.parent.GetComponentInChildren<WeaponHolder>().currentWeapon != "")
                {
                    currentCharacter.transform.parent.GetComponentInChildren<WeaponHolder>().DesactivarArmas();
                    currentCharacter.transform.parent.GetComponentInChildren<WeaponHolder>().TirarArmaActual();
                }
                Destroy(currentCharacter);
            }
            else
            {
                return;
            }
        }
        if (!canChange) return;

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            index++;
            if (index >= characters.Length)
            {
                index = 0;
            }
            UpdateImage();
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            index--;
            if (index < 0)
            {
                index = characters.Length - 1;
            }
            UpdateImage();
        }

        if (Input.GetKey(KeyCode.Return))
        {
            characters[index].SetActive(false);
            background.SetActive(false);
            SelectCharacter();
            selected = true;
        }
    }
    void SelectCharacter()
    {
        currentCharacter = Instantiate(players[index], Vector3.zero, Quaternion.identity, character.transform);
        currentCharacter.SetActive(true);
    }
    void UpdateImage()
    {
        canChange = false;
        for (int i = 0; i < characters.Length; i++)
        {
            if (i == index)
            {
                characters[i].SetActive(true);
            }
            else
            {
                characters[i].SetActive(false);
            }
        }
        canChange = true;
    }
}
