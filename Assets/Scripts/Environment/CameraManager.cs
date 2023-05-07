using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public GameObject follow;

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 pos = follow.transform.transform.position;
        Vector3 newPos = new Vector3(pos.x, pos.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, newPos, 0.5f);
    }
}
