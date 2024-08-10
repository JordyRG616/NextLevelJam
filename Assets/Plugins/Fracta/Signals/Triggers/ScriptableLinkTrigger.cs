using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptableLinkTrigger : MonoBehaviour
{
    [SerializeField] private ScriptableSignal signal;
    

    public void Fire()
    {
        signal.Fire();
    }
}
