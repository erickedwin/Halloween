using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InventoryMenu : MonoBehaviour
{
    [SerializeField]
    GameObject inventoryMenu;

    [SerializeField]
    UnityEvent OnShown;

    [SerializeField]
    UnityEvent OnResume;

    public bool isOpen { get; private set; }

    public bool canShowMenu { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        canShowMenu = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerInputHandler.instance.inventory && canShowMenu)
        {
            SwitchPauseStatus();
        }
    }

    public void SwitchPauseStatus()
    {
        if (!isOpen)
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
        isOpen = true;
        Time.timeScale = 0;
        PlayerInputHandler.instance.PauseInput();
        inventoryMenu.SetActive(isOpen);
        OnShown.Invoke();
    }

    public void Resume()
    {
        isOpen = false;
        Time.timeScale = 1;
        PlayerInputHandler.instance.ResumeInput();
        inventoryMenu.SetActive(isOpen);
        OnResume.Invoke();
    }
}
