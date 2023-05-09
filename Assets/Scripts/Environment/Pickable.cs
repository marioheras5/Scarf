using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Pickable : MonoBehaviour
{
    public GameObject pickableGameObject;

    bool canPick = false;
    Collider2D playerCollider;

    void Start()
    {
        // Render sprite actual
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        renderer.sprite = pickableGameObject.GetComponent<SpriteRenderer>().sprite;
    }
    void Update()
    {
        // Pickear objeto
        if (canPick && Input.GetKeyDown(KeyCode.E))
        {
            Pick();
        }
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.transform.tag == "Player")
        {
            playerCollider = collider;
            AparecerTecla(true);
            canPick = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.transform.tag == "Player")
        {
            AparecerTecla(false);
            canPick = false;
        }
    }
    void AparecerTecla(bool alpha)
    {
        SpriteRenderer sr = gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>();
        Color color = sr.color;
        color.a = alpha ? 1 : 0;
        sr.color = color;
    }
    private void Pick()
    {
        // Detectamos el weaponholder del jugador
        WeaponHolder wh = playerCollider.gameObject.transform.parent.gameObject.GetComponentInChildren<WeaponHolder>();
        
        // Si ya tiene un arma, la intercambiamos
        if (wh.currentWeapon != "")
        {
            GameObject aux = wh.weapons.First(x => x.name == wh.currentWeapon);
            if (aux.GetComponent<WeaponAttack>().canAttack)
            {
                wh.EquipWeapon(pickableGameObject.name);
                pickableGameObject = aux;
                // Render sprite actual
                SpriteRenderer renderer = GetComponent<SpriteRenderer>();
                renderer.sprite = pickableGameObject.GetComponent<SpriteRenderer>().sprite;
            }
        }
        else
        {
            wh.EquipWeapon(pickableGameObject.name);
            Destroy(gameObject);
        }
    }
}
