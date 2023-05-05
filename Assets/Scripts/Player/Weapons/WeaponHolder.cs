using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WeaponHolder : MonoBehaviour
{

    public string currentWeapon = "";
    public List<GameObject> weapons;
    GameObject weapon;
    // Start is called before the first frame update
    void Start()
    {
        DesactivarArmas();
        EquipWeapon("Baston");
    }
    // Update is called once per frame
    void Update()
    {
        // Posición del arma respecto al ratón
        Vector3 mousePos = Input.mousePosition;
        var playerScreenPoint = Camera.main.WorldToScreenPoint(transform.position);
        if (mousePos.x < playerScreenPoint.x)
        {
            Vector2 direction = Camera.main.ScreenToWorldPoint(mousePos) - transform.position;
            float angle = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = rotation;
        }
        else
        {
            Vector2 direction = Camera.main.ScreenToWorldPoint(mousePos) - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = rotation;
        }

        // Dropear arma con la Q
        if (currentWeapon != "" && Input.GetKeyDown(KeyCode.Q))
        {
            TirarArmaActual();
            DesactivarArmas();
        }

    }
    void TirarArmaActual()
    {

    }
    void DesactivarArmas()
    {
        currentWeapon = "";
        foreach (var weapon in weapons)
        {
            weapon.SetActive(false);
        }
    }
    public void EquipWeapon(string weaponName)
    {
        if (currentWeapon != "")
        {
            GameObject oldWeapon = weapons.First(x => x.name == currentWeapon);
            oldWeapon.SetActive(false);
            SpriteRenderer renderer = oldWeapon.GetComponent<SpriteRenderer>();
            Color color = renderer.color;
            color.a = 0f;
            renderer.color = color;
        }
        currentWeapon = weaponName;
        weapon = weapons.First(x => x.name == currentWeapon);
        weapon.SetActive(true);
        SpriteRenderer rendererNew = weapon.GetComponent<SpriteRenderer>();
        Color colorNew = rendererNew.color;
        colorNew.a = 255f;
        rendererNew.color = colorNew;
    }
}
