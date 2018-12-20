using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InputManager {
    [Header("Moving Basic Inputs")]
    [SerializeField] KeyCode forwardKey;
    [SerializeField] KeyCode backwardKey;
    [SerializeField] KeyCode rightKey;
    [SerializeField] KeyCode leftKey;

    public int GetForwardInput()
    {
        return Input.GetKey(forwardKey) && Input.GetKey(backwardKey) ? 0 : Input.GetKey(forwardKey) ? 1 : Input.GetKey(backwardKey) ? -1 : 0;
    }

    public int GetLateralInput()
    {
        return Input.GetKey(rightKey) && Input.GetKey(leftKey) ? 0 : Input.GetKey(rightKey) ? 1 : Input.GetKey(leftKey) ? -1 : 0;
    }

    [Space] [Header("Moving Secondary Inputs")]
    [SerializeField] KeyCode jumpKey;
    [SerializeField] KeyCode runKey;
    [SerializeField] KeyCode[] crouchKeys;

    public bool Running
    {
        get
        {
            return Input.GetKey(runKey);
        }
    }

    public bool GetJump
    {
        get
        {
            return Input.GetKeyDown(jumpKey);
        }
    }

    public bool GetCrouch
    {
        get
        {
            bool pressed = false;

            foreach(KeyCode key in crouchKeys)
            {
                if (Input.GetKey(key))
                {
                    pressed = true;
                    break;
                }
            }

            return pressed;
        }
    }

    [Space] [Header("Actions Inputs")]
    [SerializeField] KeyCode interactKey;
    [SerializeField] KeyCode lensKey;
    [SerializeField] KeyCode laserKey;

    public bool GetInteractDown{
        get
        {
            return Input.GetKeyDown(interactKey);
        }
    }

    public bool GetInteractUp
    {
        get
        {
            return Input.GetKeyUp(interactKey);
        }
    }

    public bool GetLensEquip
    {
        get
        {
            return Input.GetKeyDown(lensKey);
        }
    }
}
