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
    [SerializeField] KeyCode crouchKey;

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
            return Input.GetKey(jumpKey);
        }
    }

    public bool GetCrouch
    {
        get
        {
            return Input.GetKey(crouchKey);
        }
    }

    [Space] [Header("Actions Inputs")]
    [SerializeField] KeyCode interactKey;
    [SerializeField] KeyCode lensKey;
    [SerializeField] KeyCode laserKey;

    public bool GetLensEquip
    {
        get
        {
            return Input.GetKeyDown(lensKey);
        }
    }
}
