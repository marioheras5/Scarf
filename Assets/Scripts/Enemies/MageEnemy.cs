using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageEnemy : MonoBehaviour
{
    public GameObject projectile;
    public float attackSpeed;
    bool canAttack = true;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (canAttack)
        {
            StartCoroutine(Attack());
        }
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
