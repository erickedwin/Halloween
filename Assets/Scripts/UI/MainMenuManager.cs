using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    private void Awake()
    {

#if !UNITY_EDITOR
        Debug.unityLogger.logEnabled = false;
#endif

    }
    void Start()
    {
        Time.timeScale = 1;
    }
    
}
