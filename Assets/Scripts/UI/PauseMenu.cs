using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    GameObject pauseMenu;

    [SerializeField]
    UnityEvent OnPause;

    [SerializeField]
    UnityEvent OnResume;

    public bool isPaused { get; private set; }

    public bool canPause { get; set; }

    private void Start()
    {
        Time.timeScale = 1;
        canPause = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerInputHandler.instance.pauseMenu && canPause)
        {
            SwitchPauseStatus();
        }
    }

    public void SwitchPauseStatus()
    {
        if (!isPaused)
        {
            Pause();
        }
        else
        {
            Resume();
        }
    }

    public void Pause()
    {
        isPaused = true;
        Time.timeScale = 0;
        PlayerInputHandler.instance.PauseInput();
        pauseMenu.SetActive(isPaused);
        OnPause.Invoke();
    }

    public void Resume()
    {
        isPaused = false;
        Time.timeScale = 1;
        PlayerInputHandler.instance.ResumeInput();
        pauseMenu.SetActive(isPaused);
        OnResume.Invoke();
    }
}
