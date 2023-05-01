using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    // Posicion seleccionada menu
    int pos = 0;
    // 

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // Boton de start
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            if (pos == 0)
            {
                return;
            }
            pos = 0;
            // Boton start
            GameObject startObject = GameObject.FindWithTag("start");
            startObject.transform.localScale = new Vector3(5.3f, 5.3f, 5.3f);

            // Boton exit
            GameObject exitObject = GameObject.FindWithTag("exit");
            exitObject.transform.localScale = new Vector3(5f, 5f, 5f);

            // Espada
            GameObject espadaObject = GameObject.FindWithTag("espada");
            Vector3 currentPosition = espadaObject.transform.position;
            Vector3 newPosition = new Vector3(currentPosition.x, -1.9f, currentPosition.z);
            espadaObject.transform.position = newPosition;
        }
        // Boton de exit
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            if (pos == 1)
            {
                return;
            }
            pos = 1;

            // Boton start
            GameObject startObject = GameObject.FindWithTag("start");
            startObject.transform.localScale = new Vector3(5f, 5f, 5f);

            // Boton exit
            GameObject exitObject = GameObject.FindWithTag("exit");
            exitObject.transform.localScale = new Vector3(5.3f, 5.3f, 5.3f);

            // Espada
            GameObject espadaObject = GameObject.FindWithTag("espada");
            Vector3 currentPosition = espadaObject.transform.position;
            Vector3 newPosition = new Vector3(currentPosition.x, -3.1f, currentPosition.z);
            espadaObject.transform.position = newPosition;
        }

        if (Input.GetKey(KeyCode.Return))
        {
            // START
            if (pos == 0)
            {
                SceneManager.LoadScene("MainScene");
                return;
            }

            // EXIT
            if (pos == 1)
            {
                Application.Quit();
            }
        }
    }
}
