using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTrigger : MonoBehaviour {

    [SerializeField] Event relatedEvent;

    public virtual void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerHitbox>())
        {
            relatedEvent.TestTriggerEvent();
        }
    }
}
