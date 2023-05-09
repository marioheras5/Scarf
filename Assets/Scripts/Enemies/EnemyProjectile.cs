using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
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
        
        Vector3 target = GameObject.FindGameObjectWithTag("Player").transform.position;
        Vector2 direction = (target - transform.position).normalized;
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
        // Si choca contra un player
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerStats>().TakeDamage(attackDamage, Vector3.zero);
            Destroy(gameObject);
        }
    }
}
