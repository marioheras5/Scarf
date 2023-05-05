using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float attackDamage;
    public float force;
    public GameObject projectile;
    public Rigidbody2D rb;
    public float lifespan;
    public float pos;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + pos, transform.position.z);
        Vector2 direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
        rb.AddForce(direction * force, ForceMode2D.Impulse);
        float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;

        // Rotate the projectile towards the velocity direction
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    // Update is called once per frame
    void Update()
    {
        lifespan -= Time.deltaTime;
        if (lifespan <= 0)
        {
            Destroy(projectile);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Si choca contra un enemigo o una pared se rompe

        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(projectile);
        }
    }
}
