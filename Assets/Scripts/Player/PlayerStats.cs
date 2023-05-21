using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public GameObject deadSound;
    public float health;
    public GameObject healthBar;
    bool muerto = false;
    float maxHealth;
    GameObject text;

    public float invincibilityTime = 1f;
    bool invincible;
    int frameCounter = 0;

    // Start is called before the first frame update
    void Start()
    {
        text = Resources.Load<GameObject>("CombatTextGO");
        maxHealth = health;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (muerto) return;
        if (invincible)
        {
            frameCounter++;
            if (frameCounter == 2)
            {
                SpriteRenderer render = GetComponent<SpriteRenderer>();
                Color color = render.color;
                color.a = 0;
                render.color = color;
            }
            else if (frameCounter == 4)
            {
                frameCounter = 0;
                SpriteRenderer render = GetComponent<SpriteRenderer>();
                Color color = render.color;
                color.a = 255;
                render.color = color;
            }
        }
        else
        {
            SpriteRenderer render = GetComponent<SpriteRenderer>();
            Color color = render.color;
            color.a = 255;
            render.color = color;
        }
    }

    public void Heal(float amount)
    {
        health += amount;
        if (health > maxHealth) health = maxHealth;
        UpdateHealthBar();
    }
    public void TakeDamage(float damage, [Optional] Vector3 knockbackDirection)
    {
        if (muerto) return;
        if (invincible) return;

        bool isPlayer = tag == "Player";
        health -= damage;

        if (knockbackDirection != Vector3.zero)
        {
            GetComponent<Rigidbody2D>().velocity = knockbackDirection;
            GetComponent<WalkingEnemy>().knockbacking = true;
        }
        if (!isPlayer) MostrarNumero(damage);
        else
        {
            UpdateHealthBar();
            ShakeHealthBar();
        }

        if (health <= 0)
        {
            if (isPlayer)
            {
                StartCoroutine(Muerte());
                return;
            }
            Destroy(gameObject);
            GameObject.Find("GameManager").GetComponent<GameManager>().AddKill();
        }

        StartCoroutine(Invincible());
    }
    void UpdateHealthBar()
    {
        healthBar.GetComponent<Image>().fillAmount = 0.3f + (health * 0.7f / maxHealth);
    }
    void ShakeHealthBar()
    {
        healthBar.transform.parent.GetComponent<Animator>().SetTrigger("Shake");
    }
    void MostrarNumero(float damage)
    {
        text.GetComponentInChildren<TextMeshProUGUI>().SetText("-" + damage.ToString());
        Instantiate(text, Camera.main.WorldToScreenPoint(transform.position), Quaternion.identity, GameObject.Find("HUD").transform);
    }
    IEnumerator Muerte()
    {
        if (muerto) yield return null;
        muerto = true;
        Instantiate(deadSound);
        GameObject.Find("Dead").GetComponent<Animator>().SetTrigger("Dead");
        yield return new WaitForSeconds(9f);
        SceneManager.LoadScene("MenuScene", LoadSceneMode.Single);
        Destroy(GameObject.Find("SelectedPlayers"));
    }
    IEnumerator Invincible()
    {
        invincible = true;
        frameCounter = 0;
        yield return new WaitForSeconds(invincibilityTime);
        invincible = false;
    }
}
 