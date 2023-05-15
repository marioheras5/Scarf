using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Chest : MonoBehaviour
{
    public GameObject[] pool;
    public Sprite closedSprite;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Instantiate(pool[new System.Random().Next(0, pool.Length)],transform.position , Quaternion.Euler(0, 0, 270));
            CloseChest();
        }
    }
    void CloseChest()
    {
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Animator>().enabled = false;
        GetComponentInChildren<Light2D>().enabled = false;
        GetComponent<SpriteRenderer>().sprite = closedSprite;
    }
}
