using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider))]
public class EventZone : MonoBehaviour {

    [Header("Event")]
    public UnityEvent enterEvent;
    public UnityEvent exitEvent;

    [Header("Parameters")]
    public bool infiniteEnterEvent, infiniteExitEvent;
    bool enterPlayed, exitPlayed;

    void EnterEvent()
    {
        if (!infiniteEnterEvent && enterPlayed) return;

        enterEvent.Invoke();
        enterPlayed = true;
    }

    void ExitEvent()
    {
        if (!infiniteExitEvent && exitPlayed) return;

        exitEvent.Invoke();
        exitPlayed = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<IPlayer>() != null)
        {
            EnterEvent();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<IPlayer>() != null)
        {
            ExitEvent();
        }
    }
}
