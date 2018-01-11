using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//code ref https://www.youtube.com/watch?v=xIevsYimJYc
public class pause : MonoBehaviour {
    public GameObject pauseUI;

    private bool enable = false;

    private void Start()
    {
        pauseUI.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            enable = !enable;
        }

        if (enable)
        {
            pauseUI.SetActive(true);
            Time.timeScale = 0;
        }

        if (!enable)
        {
            pauseUI.SetActive(false);
            Time.timeScale = 1;
        }
    }

    public void Resume()
    {
        enable = false;
    }

    public void Exit()
    {
        Application.Quit();
    }
}
