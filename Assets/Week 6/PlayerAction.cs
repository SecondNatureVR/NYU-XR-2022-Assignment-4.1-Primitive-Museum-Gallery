using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerAction : MonoBehaviour
{
    public UnityEvent actionTriggered;
    private void Start()
    {
        if (actionTriggered == null)
            actionTriggered = new UnityEvent();
    }

    public void OnFire()
    {
        actionTriggered.Invoke();
    }
}
