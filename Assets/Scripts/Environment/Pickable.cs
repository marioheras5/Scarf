using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Pickable : MonoBehaviour
{
    bool canPick = false;
    public GameObject pickableGameObject;
    Collider2D playerCollider;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        renderer.sprite = pickableGameObject.GetComponent<SpriteRenderer>().sprite;
        if (canPick && Input.GetKeyDown(KeyCode.E))
        {
            Pick();
        }
    }
    void Pick()
    {
        // Detectamos el weaponholder del jugador
        WeaponHolder wh = playerCollider.gameObject.GetComponentInChildren<WeaponHolder>();
        
        // Si ya tiene un arma, la intercambiamos
        if (wh.currentWeapon != "")
        {
            GameObject aux = wh.weapons.First(x => x.name == wh.currentWeapon);
            wh.EquipWeapon(pickableGameObject.name);
            pickableGameObject = aux;
        }
        else
        {
            wh.EquipWeapon(pickableGameObject.name);
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.transform.tag == "Player")
        {
            playerCollider = collider;
            //AparecerTecla();
            canPick = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.transform.tag == "Player")
        {
            //DesaparecerTecla();
            canPick = false;
        }
    }
}
