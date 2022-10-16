using UnityEngine.SceneManagement;
using UnityEngine;
using System;
using System.Collections;
using Nocturne.GeneralTools;

public class LoadScenes : MonoBehaviour
{
    public void LoadSpecificScene(int index)
    {
        SceneManager.LoadSceneAsync(index);
    }

    public void LoadSpecificSceneDelayed(int index)
    {
        StartCoroutine(Load(index));
    }

    IEnumerator Load(int index)
    {
        yield return Helpers.GetWaitRealtime(0.5f);
        SceneManager.LoadSceneAsync(index);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadSceneAsync(0);
    }
}
