using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageEnemy : MonoBehaviour
{
    public GameObject projectile;
    public float attackSpeed;
    bool canAttack;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartAttacking());
    }

    // Update is called once per frame
    void Update()
    {
        if (canAttack)
        {
            StartCoroutine(Attack());
        }
    }
    IEnumerator StartAttacking()
    {
        yield return new WaitForSeconds(attackSpeed);
        canAttack = true;
    }
    IEnumerator Attack()
    {
        canAttack = false;
        Instantiate(projectile, transform.position, Quaternion.identity);
        Projectile fireball = gameObject.GetComponent<Projectile>();
        yield return new WaitForSeconds(attackSpeed);
        canAttack = true;
    }
}
