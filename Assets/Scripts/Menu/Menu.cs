using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public GameObject title;
    public GameObject start;
    public GameObject exit;

    bool animate = true;
    Vector3 startingPoint;
    int pos = 0;


    // Start is called before the first frame update
    void Start()
    {
        Vector3 titlePos = startingPoint = title.GetComponent<Image>().rectTransform.position;
        title.GetComponent<Image>().rectTransform.position = new Vector3(titlePos.x, titlePos.y + 700, titlePos.z);
        Vector3 startPos = start.GetComponent<Image>().rectTransform.position;
        start.GetComponent<Image>().rectTransform.position = new Vector3(startPos.x, startPos.y - 700, startPos.z);
        Vector3 exitPos = exit.GetComponent<Image>().rectTransform.position;
        exit.GetComponent<Image>().rectTransform.position = new Vector3(exitPos.x, exitPos.y - 700, exitPos.z);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (animate)
        {
            Vector3 titlePos = title.GetComponent<Image>().rectTransform.position;
            if (titlePos.y <= startingPoint.y)
            {
                animate = false;
                Vector3 scale = start.GetComponent<Image>().rectTransform.localScale;
                start.GetComponent<Image>().rectTransform.localScale = new Vector3(scale.x + 0.3f, scale.y + 0.3f, scale.z);
                return;
            }
            title.GetComponent<Image>().rectTransform.position = Vector3.Lerp(titlePos, new Vector3(titlePos.x, titlePos.y - 700, titlePos.z), 0.003f);
            Vector3 startPos = start.GetComponent<Image>().rectTransform.position;
            start.GetComponent<Image>().rectTransform.position = Vector3.Lerp(startPos, new Vector3(startPos.x, startPos.y + 700, startPos.z), 0.003f);
            Vector3 exitPos = exit.GetComponent<Image>().rectTransform.position;
            exit.GetComponent<Image>().rectTransform.position = Vector3.Lerp(exitPos, new Vector3(exitPos.x, exitPos.y + 700, exitPos.z), 0.003f);
        }
        else
        {
            if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) && (pos != 0))
            {
                pos = 0;
                Vector3 scale = start.GetComponent<Image>().rectTransform.localScale;
                start.GetComponent<Image>().rectTransform.localScale = new Vector3(scale.x + 0.15f, scale.y + 0.15f, scale.z);

                Vector3 scaleExit = exit.GetComponent<Image>().rectTransform.localScale;
                exit.GetComponent<Image>().rectTransform.localScale = new Vector3(scaleExit.x - 0.15f, scaleExit.y - 0.15f, scale.z);
            } 
            else if ((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) && (pos != 1))
            {
                pos = 1;
                Vector3 scale = exit.GetComponent<Image>().rectTransform.localScale;
                exit.GetComponent<Image>().rectTransform.localScale = new Vector3(scale.x + 0.15f, scale.y + 0.15f, scale.z);

                Vector3 scaleExit = start.GetComponent<Image>().rectTransform.localScale;
                start.GetComponent<Image>().rectTransform.localScale = new Vector3(scaleExit.x - 0.15f, scaleExit.y - 0.15f, scale.z);
            }

            if (Input.GetKey(KeyCode.Return))
            {
                // START
                if (pos == 0)
                {
                    StartButton();
                    return;
                }
                else if (pos == 1)
                {
                    ExitButton();
                }
            }
        }
    }
    public void StartButton()
    {
        SceneManager.LoadScene("MainScene");
    }
    public void ExitButton()
    {
        Application.Quit();
    }
}
 