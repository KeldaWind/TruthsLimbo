using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpeningEventTrigger : EventTrigger {
    [SerializeField] DoorOpeningEvent relatedDoorOpeningEvent;

    public override void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerHitbox>() != null)
        {
            relatedDoorOpeningEvent.TestTriggerEvent();
        }
    }
}
