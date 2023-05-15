using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Rigidbody2D rb;

    public float attackDamage;
    public float projectileForce;
    public float lifespan;
    public float startingPosition;
    public float knockback;

    void Start()
    {
        // Dirección del proyectil - Jugador -> Mouse
        transform.position = new Vector3(transform.position.x, transform.position.y + startingPosition, transform.position.z);
        Vector2 direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
        rb.AddForce(direction * projectileForce, ForceMode2D.Impulse);
        float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    void Update()
    {
        // Tiempo de vida
        lifespan -= Time.deltaTime;
        if (lifespan <= 0)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Si choca contra un enemigo o una pared se rompe
        if (collision.gameObject.tag == "Enemy")
        {
            Vector3 knockbackDirection = (collision.transform.position - transform.position).normalized * knockback;
            collision.gameObject.GetComponent<PlayerStats>().TakeDamage(attackDamage, knockbackDirection);
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }
    }
}
