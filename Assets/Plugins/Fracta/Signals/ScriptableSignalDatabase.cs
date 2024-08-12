using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ScriptableSignalDatabase : MonoBehaviour
{
    [SerializeField] private List<ScriptableSignal> signals;


    private void Start()
    {
        SceneManager.sceneLoaded += ResetAll;
    }

    private void ResetAll(Scene arg0, LoadSceneMode arg1)
    {
        signals.ForEach(x => x.ResetToDefault());
        SceneManager.sceneLoaded -= ResetAll;
    }
}
