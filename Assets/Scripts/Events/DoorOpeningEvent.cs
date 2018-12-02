using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DoorOpeningEvent : Event {
    [SerializeField] Vector3 openingDegree;
    [SerializeField] Transform doorObject;

    public override void TriggerEvent()
    {
        Debug.Log("oui");
        doorObject.transform.rotation = Quaternion.Euler(openingDegree);
    }
}
