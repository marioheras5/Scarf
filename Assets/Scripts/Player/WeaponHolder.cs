using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WeaponHolder : MonoBehaviour
{

    public string currentWeapon = "";
    public List<GameObject> weapons;
    // Start is called before the first frame update
    void Start()
    {
        DesactivarArmas();
        EquipWeapon("Baston");
    }
    // Update is called once per frame
    void Update()
    {

    }
    void DesactivarArmas()
    {
        foreach (var weapon in weapons)
        {
            weapon.SetActive(false);
        }
    }
    public void EquipWeapon(string weaponName)
    {
        if (currentWeapon != "")
        {
            GameObject weapon = weapons.First(x => x.name == currentWeapon);
            weapon.SetActive(false);
            SpriteRenderer renderer = weapon.GetComponent<SpriteRenderer>();
            Color color = renderer.color;
            color.a = 0f;
            renderer.color = color;
        }
        currentWeapon = weaponName;
        GameObject weaponNew = weapons.First(x => x.name == currentWeapon);
        weaponNew.SetActive(true);
        SpriteRenderer rendererNew = weaponNew.GetComponent<SpriteRenderer>();
        Color colorNew = rendererNew.color;
        colorNew.a = 255f;
        rendererNew.color = colorNew;
    }
}
