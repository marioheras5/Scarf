using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponAttack : NetworkBehaviour
{
    public GameObject weapon;
    public GameObject projectile;
    public bool isMelee;
    public float attackSpeed;
    public float attackDamage;
    public float knockback;

    public bool canAttack = true;
    float timer;
    Animator animator;
    List<Collider2D> cantAttackList = new List<Collider2D>();

    void Start()
    {
        //if (!IsOwner) return;
        animator = weapon.GetComponent<Animator>();
    }

    void Update()
    {
        //if (!IsOwner) return;
        animator.speed = 1f / attackSpeed;
        if (isMelee)
        {
            if (Input.GetMouseButton(0) && canAttack)
            {
                StartCoroutine(AtacarMelee());
            }
        } 
        else
        {
            AtacarRanged();
        }
    }
    IEnumerator AtacarMelee()
    {
        canAttack = false;
        // Collider
        BoxCollider2D collider = weapon.GetComponent<BoxCollider2D>();
        collider.enabled = true;

        // Animación de ataque
        animator.SetTrigger("Attack");
        float len = animator.runtimeAnimatorController.animationClips.First().length - 0.1f;
        yield return new WaitForSeconds(len);
        collider.enabled = false;
        yield return new WaitForSeconds(attackSpeed - len);
        cantAttackList.Clear();
        canAttack = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Enemy" && !cantAttackList.Contains(collision))
        {
            Vector3 knockbackDirection = (collision.transform.position - transform.position).normalized * knockback;
            collision.gameObject.GetComponent<PlayerStats>().TakeDamage(attackDamage, knockbackDirection);
            cantAttackList.Add(collision);
        }
        if (collision.transform.tag == "Projectile")
        {
            GameObject go = collision.gameObject;
            EnemyProjectile ep = go.GetComponent<EnemyProjectile>();
            ep.friendlyFire = true;
            ep.lifespan = ep.lifespan + 2.5f;
            ep.rb.velocity = -ep.rb.velocity;
            float angle = Mathf.Atan2(ep.rb.velocity.y, ep.rb.velocity.x) * Mathf.Rad2Deg;
            ep.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
    void AtacarRanged()
    {
        // Cargamos el ataque durante attackSpeed segundos
        if (Input.GetMouseButtonUp(0))
        {
            // Si ya esta cargado
            canAttack = true;
            if (timer > attackSpeed)
            {
                animator.SetBool("Attack", false);
                animator.SetBool("Charged", false);
                Instantiate(projectile, transform.position, Quaternion.identity);
                timer = 0;
            }
        }
        else if (Input.GetMouseButton(0))
        {
            // Cargando
            canAttack = false;
            animator.SetBool("Attack", true);
            timer += Time.deltaTime;
            if (timer > attackSpeed)
            {
                animator.SetBool("Charged", true);
                timer = attackSpeed + 1;
            }
        }
        else
        {
            // No hay boton pulsado
            canAttack = true;
            animator.SetBool("Attack", false);
            timer = 0;
        }

    }
}
