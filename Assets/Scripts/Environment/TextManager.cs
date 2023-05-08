using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextManager : MonoBehaviour
{
    float lifespan = 1f;
    TextMeshProUGUI text;
    void Start()
    {
        text = gameObject.GetComponentInChildren<TextMeshProUGUI>();
        Vector3 pos = text.rectTransform.anchoredPosition;
        text.rectTransform.anchoredPosition = new Vector3(pos.x, pos.y + 10f, pos.z);
    }

    void FixedUpdate()
    {
        lifespan -= Time.fixedDeltaTime;
        Vector3 pos = text.rectTransform.anchoredPosition;
        text.rectTransform.anchoredPosition = new Vector3(pos.x, pos.y + 2f, pos.z);
        
        Color color = text.color;
        color.a = lifespan;
        text.color = color;

        if (lifespan < 0f)
        {
            Destroy(gameObject);
        }
    }
}
