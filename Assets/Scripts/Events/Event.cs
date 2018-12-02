using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Event {
    [SerializeField] EventCondition eventCondition;
    public virtual void TestTriggerEvent()
    {
        if (eventCondition.ConditionOk())
        {
            TriggerEvent();
        }

    }

    public virtual void TriggerEvent()
    {

    }

}

[System.Serializable]
public class EventCondition
{
    public bool needLens;
    public bool needLamp;

    public bool ConditionOk()
    {
        bool okay = true;
        if (needLens && !GameManager.gameManager.player.HasLens)
            return false;
        if (needLamp && !GameManager.gameManager.player.HasLamp)
            return false;

        return true;
    }
}
