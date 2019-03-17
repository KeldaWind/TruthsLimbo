using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteracible
{
    [SerializeField] Rigidbody doorBody;
    [SerializeField] HingeJoint doorHingeJoint;
    [SerializeField] bool openable;
    [SerializeField] bool opened;
    [SerializeField] AudioSource openingSound;
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
                JointSpring newSpring = doorHingeJoint.spring;
                if (!firstOpening)
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

            if(openingSound != null)
            {
                openingSound.Play();
            }
        }
    }

    public void LockDoor(bool needLens)
    {
        //Debug.Log("Blocage" + (needLens ? " besoin lentille : " : ": ") + GameManager.gameManager.player.HasLens);

        if (needLens && !GameManager.gameManager.player.HasLens)
            return;

        openable = false;
        opened = false;
    }

    public void UnlockDoor(bool needLens)
    {
        //Debug.Log("Blocage" + (needLens ? " besoin lentille : " : ": ") + GameManager.gameManager.player.HasLens);

        if (needLens && !GameManager.gameManager.player.HasLens)
            return;

        openable = true;
        opened = true;
        firstOpening = false;
    }
}
