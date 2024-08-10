using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignalTrigger : MonoBehaviour
{
    public Signal OnSignalFired;


    public void FireSignal()
    {
        OnSignalFired.Fire();
    }
}
