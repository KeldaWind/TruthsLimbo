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
    public PlayerPullability PlrPullability
    {
        get
        {
            return playerPullability;
        }
    }
    [SerializeField] TutorialManager playerTutorial;

    [SerializeField] DocumentReadability documentReadability;
    public DocumentReadability PlayerDocumentReadability
    {
        get
        {
            return documentReadability;
        }
    }


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
            {
                lensManager.Equip(!lensManager.Equiped);
                documentReadability.SwitchDocumentImage();
            }
        }

        CheckInteraction();
        //playerPullability.CheckForTakeOrRelease(inputManager, this, GameManager.gameManager.NormalCamera.gameObject.activeInHierarchy ? GameManager.gameManager.NormalCamera : GameManager.gameManager.LensCamera);
        playerPullability.UpdatePull(transform.position);

        playerMovements.CheckCrouch(inputManager.GetCrouch);

        if (lampManager.HasLamp)
        {
            EnigmaObject lampEnigmaObject = lampManager.CheckForLookedObject(GameManager.gameManager.NormalCamera.gameObject.activeInHierarchy ? GameManager.gameManager.NormalCamera : GameManager.gameManager.LensCamera);
            if (lampEnigmaObject != null && Input.GetMouseButtonDown(0))
            {
                lampManager.ActiveLamp(lampEnigmaObject);
                playerTutorial.ValidateLampUse();
            }

            if (Input.GetMouseButtonDown(1))
            {
                lampManager.DesactiveLamp();
                playerTutorial.ValidateLampRemove();
            }
        }

        bool onGround = playerMovements.GrvtManager.CheckForOnGround();

        if (inputManager.GetJump && onGround && !playerMovements.Crouched)
            playerMovements.Jump();

        playerTutorial.Update();
    }

    private void FixedUpdate()
    {
        playerBody.velocity = GetCurrentTotalForce();
        playerMovements.MovePlayerCameraAndObject();

        /*if (inputManager.GetJump)
        {
            Debug.Log("get jump");
            if(onGround)
                Debug.Log("onGround");

            if (!playerMovements.Crouched)
                Debug.Log("Uncrouched");
        }*/


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

        Vector3 moveDir = playerMovements.GetCurrentPlayerWalkMove(new Vector3(inputManager.GetLateralInput(), 0, inputManager.GetForwardInput()), inputManager.Running).normalized;
        if (moveDir != Vector3.zero)
        {
            Vector3 normalDir = playerMovements.GrvtManager.GetGroundNormalDirection();

            Vector3.OrthoNormalize(ref normalDir, ref moveDir);

            Debug.DrawRay(transform.position, moveDir * 10, Color.red);

            currentTotalForce += playerMovements.GetCurrentPlayerMoveWithRightDirection(moveDir, inputManager.Running);
        }

        currentTotalForce += playerMovements.GrvtManager.GetCurrentGravityForce();

        return currentTotalForce;
    }
    #endregion

    #region Interactions
    public void CheckInteraction()
    {
        Camera mainCamera = GameManager.gameManager.NormalCamera.gameObject.activeInHierarchy? GameManager.gameManager.NormalCamera : GameManager.gameManager.LensCamera;

        if (inputManager.GetInteractDown)
        {
            if (documentReadability.opened)
            {
                documentReadability.CloseDocument();
            }

            Ray ray = mainCamera.ScreenPointToRay(new Vector3(mainCamera.pixelWidth / 2, mainCamera.pixelHeight / 2, 0));
            RaycastHit hit = new RaycastHit();

            if (Physics.Raycast(ray, out hit, 3))
            {
                IInteracible interactibleObject = hit.collider.GetComponent<IInteracible>();
                if (interactibleObject != null)
                {
                    playerTutorial.ValidateInteraction();
                    interactibleObject.Interact(this);
                }
            }
        }

        if (inputManager.GetInteractUp)
        {
            playerPullability.ReleaseObject(this);
        }
    }
    #endregion
}
