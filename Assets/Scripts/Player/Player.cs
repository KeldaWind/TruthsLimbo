using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] Rigidbody playerBody;
    [SerializeField] InputManager inputManager;
    [SerializeField] PlayerMovements playerMovements;
    [SerializeField] LensManager lensManager;
    [SerializeField] PlayerPullability playerPullability;

    public bool HasLens
    {
        get
        {
            return lensManager.HasLens;
        }
    }
    [SerializeField] LampManager lampManager;
    public bool HasLamp
    {
        get
        {
            return lampManager.HasLamp;
        }
    }

    void Start () {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        lampManager.CheckForLookedObject(GameManager.gameManager.NormalCamera.gameObject.activeInHierarchy ? GameManager.gameManager.NormalCamera : GameManager.gameManager.LensCamera);
    }
	
	void Update () {
        if (lensManager.HasLens)
        {
            if (inputManager.GetLensEquip)
                lensManager.Equip(!lensManager.Equiped);
        }

        playerPullability.CheckForTakeOrRelease(inputManager, this, GameManager.gameManager.NormalCamera.gameObject.activeInHierarchy ? GameManager.gameManager.NormalCamera : GameManager.gameManager.LensCamera);

        playerMovements.CheckCrouch(inputManager.GetCrouch);

        if (lampManager.HasLamp)
        {
            EnigmaObject lampEnigmaObject = lampManager.CheckForLookedObject(GameManager.gameManager.NormalCamera.gameObject.activeInHierarchy ? GameManager.gameManager.NormalCamera : GameManager.gameManager.LensCamera);
            if (lampEnigmaObject != null && Input.GetMouseButtonDown(0))
            {
                lampManager.ActiveLamp(lampEnigmaObject);
            }

            if (Input.GetMouseButtonDown(1))
            {
                lampManager.DesactiveLamp();
            }
        }
    }

    private void FixedUpdate()
    {
        playerBody.velocity = GetCurrentTotalForce();
        playerMovements.MovePlayerCameraAndObject();

        bool onGround = playerMovements.GrvtManager.CheckForOnGround();

        if(Input.GetKeyDown(KeyCode.Space))
        if (inputManager.GetJump && onGround && !playerMovements.Crouched)
            playerMovements.Jump();
    }

    public void GainLens()
    {
        lensManager.GainLens();
    }

    public void GainLamp()
    {
        lampManager.GainLamp();
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
