using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DummyScript : MonoBehaviour
{
    GameObject text;
    void Start()
    {
        text = Resources.Load<GameObject>("CombatTextGO");
    }
}
