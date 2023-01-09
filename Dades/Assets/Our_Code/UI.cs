using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UI : MonoBehaviour
{
    public Camera gameCam;
    public Camera visualCam;
    public GameObject pauseCanvas;
    public GameObject returnButton;

    public void Visualize()
    {
        Time.timeScale = 1;

        visualCam.enabled = true;
        gameCam.enabled = false;
        pauseCanvas.SetActive(false);
        returnButton.SetActive(true);
    }

    public void ReturnToPause()
    {
        Time.timeScale = 0;

        visualCam.enabled = false;
        gameCam.enabled = true;
        pauseCanvas.SetActive(true);
        returnButton.SetActive(false);
    }
}
