using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LoadManager : ManagerBehaviour
{
    public void LoadSceneByName(string name)
    {
        SceneManager.LoadSceneAsync(name);
    }

    public void LoadSceneByBuildIndex(int index)
    {
        SceneManager.LoadSceneAsync(index);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
