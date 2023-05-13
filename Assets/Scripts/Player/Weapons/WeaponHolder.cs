using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
    public string currentWeapon = "";
    public List<GameObject> weapons;
    public Pickable pickable;
    public GameObject player;

    GameObject realPlayer;

    GameObject weapon;

    void Start()
    {
        DesactivarArmas();
        if (currentWeapon != "")
        {
            EquipWeapon(currentWeapon);
        }
    }

    void FixedUpdate()
    {
        // Seguir player
        if (player.GetComponentInChildren<PlayerMovement>() != null)
        {
            realPlayer = player.GetComponentInChildren<PlayerMovement>().gameObject;
        }

        if (realPlayer != null)
        {
            transform.position = realPlayer.transform.position;
        }
        else
        {
            transform.position = player.transform.position;
        }

        // Posición del arma respecto al ratón
        Vector3 mousePos = Input.mousePosition;
        var playerScreenPoint = Camera.main.WorldToScreenPoint(transform.position);
        if (mousePos.x < playerScreenPoint.x)
        {
            // Izquierda
            Vector2 direction = Camera.main.ScreenToWorldPoint(mousePos) - transform.position;
            float angle = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = rotation;
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else
        {
            // Derecha
            Vector2 direction = Camera.main.ScreenToWorldPoint(mousePos) - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = rotation;
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }

        // Dropear arma con la Q
        if (currentWeapon != "" && !IsAttacking() && Input.GetKey(KeyCode.Q))
        {
            TirarArmaActual();
            DesactivarArmas();
        }
        
    }
    bool IsAttacking()
    {
        return !weapon.GetComponent<WeaponAttack>().canAttack;
    }
    public void TirarArmaActual()
    {
        pickable.pickableGameObject = weapon;
        Vector3 pos = transform.position;
        pos.z = transform.position.z + 1;
        Instantiate(pickable, pos, Quaternion.Euler(0, 0, 270));
    }
    public void DesactivarArmas()
    {
        currentWeapon = "";
        foreach (var weapon in weapons)
        {
            weapon.SetActive(false);
        }
    }
    public void EquipWeapon(string weaponName)
    {
        // Si ya tenía un arma, se la quitamos
        if (currentWeapon != "")
        {
            GameObject oldWeapon = weapons.First(x => x.name == currentWeapon);
            oldWeapon.SetActive(false);
            SpriteRenderer renderer = oldWeapon.GetComponent<SpriteRenderer>();
            Color color = renderer.color;
            color.a = 0f;
            renderer.color = color;
        }

        // Equipamos el nuevo arma
        currentWeapon = weaponName;
        weapon = weapons.First(x => x.name == currentWeapon);
        weapon.SetActive(true);
        SpriteRenderer rendererNew = weapon.GetComponent<SpriteRenderer>();
        Color colorNew = rendererNew.color;
        colorNew.a = 255f;
        rendererNew.color = colorNew;
    }
}
