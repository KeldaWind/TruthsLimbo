﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField] Rigidbody playerBody;
    [SerializeField] InputManager inputManager;
    [SerializeField] PlayerMovements playerMovements;
    [SerializeField] LensManager lensManager;
    [SerializeField] LampManager lampManager;

    void Start () {
        Cursor.visible = false;
	}
	
	void Update () {
        if (inputManager.GetLensEquip)
            lensManager.Equip(!lensManager.Equiped);

        playerMovements.CheckCrouch(inputManager.GetCrouch);

        EnigmaObject lampEnigmaObject = lampManager.CheckForLookedObject(GameManager.gameManager.NormalCamera.gameObject.activeInHierarchy ? GameManager.gameManager.NormalCamera : GameManager.gameManager.LensCamera);
        if(lampEnigmaObject != null && Input.GetMouseButtonDown(0))
        {
            lampManager.ActiveLamp(lampEnigmaObject);
        }

        if (Input.GetMouseButtonDown(1))
        {
            lampManager.DesactiveLamp();
        }
    }

    private void FixedUpdate()
    {
        playerBody.velocity = GetCurrentTotalForce();
        playerMovements.MovePlayerCameraAndObject();

        if (inputManager.GetJump && playerMovements.GrvtManager.CheckForOnGround())
            playerMovements.Jump();
    }

    #region Physic and Movements
    public Vector3 GetCurrentTotalForce()
    {
        Vector3 currentTotalForce = new Vector3();

        currentTotalForce += playerMovements.GetCurrentPlayerWalkMove(new Vector3(inputManager.GetLateralInput(), 0, inputManager.GetForwardInput()), inputManager.Running);

        currentTotalForce += playerMovements.GrvtManager.GetCurrentGravityForce();

        return currentTotalForce;
    }
    #endregion
}
