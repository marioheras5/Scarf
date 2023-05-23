using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Buttons : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public bool rightRotation;
    Vector3 startingPos;
    Vector3 startingScale;
    Quaternion startingRotation;
    void Start()
    {
        RectTransform rt = GetComponent<Image>().rectTransform;
        startingPos = rt.position;
        startingScale = rt.localScale;
        startingRotation = rt.rotation;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        RectTransform rt = GetComponent<Image>().rectTransform;
        rt.position = new Vector3(startingPos.x, startingPos.y + 10, startingPos.z);
        rt.localScale = new Vector3(startingScale.x + 1, startingScale.y + 1, startingScale.z);
        rt.rotation = Quaternion.Euler(0, 0, rightRotation ? 3 : -3);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        RectTransform rt = GetComponent<Image>().rectTransform;
        rt.position = startingPos;
        rt.localScale = startingScale;
        rt.rotation = startingRotation;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        RectTransform rt = GetComponent<Image>().rectTransform;
        rt.position = startingPos;
        rt.localScale = startingScale;
        rt.rotation = startingRotation;
    }
}
