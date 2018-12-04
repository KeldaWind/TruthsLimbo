using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteracible
{
    [SerializeField] Rigidbody doorBody;
    [SerializeField] HingeJoint doorHingeJoint;
    [SerializeField] bool openable;
    [SerializeField] bool opened;
    bool firstOpening = true;
    bool wideOpen;
    bool wideClosed;

    private void Update()
    {
        if (opened)
        {
            wideClosed = false;
            doorBody.freezeRotation = false;

            if (!wideOpen)
            {
                Debug.Log("opening");
                JointSpring newSpring = doorHingeJoint.spring;
                if(!firstOpening)
                    newSpring.targetPosition = 90;
                doorHingeJoint.spring = newSpring;

                if (Mathf.Abs(doorHingeJoint.angle - newSpring.targetPosition) < 3)
                    wideOpen = true;
            }
            doorBody.freezeRotation = false;
        }
        else
        {
            wideOpen = false;
            if (!wideClosed)
            {
                Debug.Log("closing");
                JointSpring newSpring = doorHingeJoint.spring;
                newSpring.targetPosition = 0;
                doorHingeJoint.spring = newSpring;

                if (Mathf.Abs(doorHingeJoint.angle - newSpring.targetPosition) < 1)
                    wideClosed = true;
            }
            else
                doorBody.freezeRotation = true;
        }
    }

    public void Interact(Player player)
    {
        if (openable)
        {
            opened = !opened;
            if (opened)
                firstOpening = false;
        }
    }
}
