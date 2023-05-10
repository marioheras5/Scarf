using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Buttons : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public bool rightRotation;
    public void OnPointerEnter(PointerEventData eventData)
    {
        RectTransform rt = GetComponent<Image>().rectTransform;
        rt.position = new Vector3(rt.position.x, rt.position.y + 10, rt.position.z);
        rt.localScale = new Vector3(rt.localScale.x + 1, rt.localScale.y + 1, rt.localScale.z);
        rt.rotation = Quaternion.Euler(0, 0, rightRotation ? 3 : -3);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        RectTransform rt = GetComponent<Image>().rectTransform;
        rt.position = new Vector3(rt.position.x, rt.position.y - 10, rt.position.z);
        rt.localScale = new Vector3(rt.localScale.x - 1, rt.localScale.y - 1, rt.localScale.z);
        rt.rotation = Quaternion.Euler(0, 0, 0);
    }
}
