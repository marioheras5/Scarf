using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingEnemy : MonoBehaviour
{
    public GameObject followingPlayer;
    public float speed;
    public float damage;
    public bool knockbacking;

    public Animator animator;
    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        followingPlayer = TargetearPlayer();
        animator.SetBool("Speed", true);
    }

    // Update is called once per frame
    void Update()
    {
        if (followingPlayer == null || rb == null || animator == null) return;

        // Calculate the direction from the enemy to the player
        Vector3 direction = followingPlayer.transform.position - transform.position;
        direction.Normalize();
        if (direction.x < 0)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }

        if (knockbacking)
        {
            StartCoroutine(Knockbacking());
            return;
        }
        // Move the enemy towards the player
        rb.velocity = direction * speed;
    }
    IEnumerator Knockbacking()
    {
        yield return new WaitForSeconds(0.15f);
        knockbacking = false;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            PlayerStats playerStats = collision.gameObject.GetComponent<PlayerStats>();
            playerStats.TakeDamage(damage, Vector3.zero);
        }
    }
    public GameObject TargetearPlayer()
    {
        return GameObject.FindGameObjectWithTag("Player");
    }

}
