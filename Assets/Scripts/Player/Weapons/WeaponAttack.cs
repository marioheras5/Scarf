using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponAttack : MonoBehaviour
{
    public GameObject weapon;
    public GameObject projectile;
    public bool melee;
    public float attackSpeed;
    public float attackDamage;
    bool canAttack = true;
    float timer;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = weapon.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.speed = 1f / attackSpeed;
        if (melee)
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
        canAttack = true;
    }
    void AtacarRanged()
    {
        if (Input.GetMouseButtonUp(0))
        {
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
            animator.SetBool("Attack", false);
            timer = 0;
        }

    }
}
