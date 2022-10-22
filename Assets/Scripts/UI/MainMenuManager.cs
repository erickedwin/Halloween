using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    private void Awake()
    {
        Debug.unityLogger.logEnabled = false;
    }
    void Start()
    {
        Time.timeScale = 1;
    }
    
}
