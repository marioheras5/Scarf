using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public GameObject player;
    public float health;
    GameObject text;

    public float invincibilityTime = 1f;
    bool invincible;
    int frameCounter = 0;

    // Start is called before the first frame update
    void Start()
    {
        text = Resources.Load<GameObject>("CombatTextGO");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (invincible)
        {
            frameCounter++;
            if (frameCounter == 2)
            {
                SpriteRenderer render = player.GetComponent<SpriteRenderer>();
                Color color = render.color;
                color.a = 0;
                render.color = color;
            } 
            else if (frameCounter == 4)
            {
                frameCounter = 0;
                SpriteRenderer render = player.GetComponent<SpriteRenderer>();
                Color color = render.color;
                color.a = 255;
                render.color = color;
            }
        } 
        else
        {
            SpriteRenderer render = player.GetComponent<SpriteRenderer>();
            Color color = render.color;
            color.a = 255;
            render.color = color;
        }
    }
    public void TakeDamage(float damage)
    {
        if (invincible) return;
        bool isPlayer = player.tag == "Player";
        health -= damage;
        if (!isPlayer) MostrarNumero(damage);
        if (health <= 0)
        {
            if (isPlayer)
            {
                Muerte();
                return;
            }
            Destroy(player);
        }

        StartCoroutine(Invincible());
    }
    void MostrarNumero(float damage)
    {
        text.GetComponentInChildren<TextMeshProUGUI>().SetText("-" + damage.ToString());
        Instantiate(text, Camera.main.WorldToScreenPoint(transform.position), Quaternion.identity, GameObject.Find("HUD").transform);
    }
    void Muerte()
    {
        Debug.Log("Muelto");
    }
    IEnumerator Invincible()
    {
        invincible = true;
        frameCounter = 0;
        yield return new WaitForSeconds(invincibilityTime);
        invincible = false;
    }
}
 