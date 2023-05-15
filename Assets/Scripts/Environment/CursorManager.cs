using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    [SerializeField] private Texture2D texture;

    public Camera cam;
    public Transform player;
    public float threshold;

    private Vector2 cursorHotspot;
    // Start is called before the first frame update
    void Start()
    {
        cursorHotspot = new Vector2(texture.width >> 1, texture.height >> 1);
        Cursor.SetCursor(texture, cursorHotspot, CursorMode.Auto);

    }

    void FixedUpdate()
    {
        Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 targetPos = (player.position + mousePos) / 2f;

        targetPos.x = Mathf.Clamp(targetPos.x, -threshold + player.position.x, threshold + player.position.x);
        targetPos.y = Mathf.Clamp(targetPos.y, -threshold + player.position.y, threshold + player.position.y);


        transform.position = targetPos;
    }
}
