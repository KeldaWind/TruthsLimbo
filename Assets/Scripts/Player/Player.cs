using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Rigidbody playerBody;
    [SerializeField] InputManager inputManager;
    [SerializeField] PlayerCursorManager playerCursorManager;
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
    [SerializeField] PlayerSoundsManager soundsManager;

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
        soundsManager.SetUp();
    }
	
	void Update () {
        bool interactionPossible = false;
        bool canLampOn = false;
        bool canLampOff = false;

        if (lensManager.HasLens)
        {
            if (inputManager.GetLensEquip)
            {
                lensManager.Equip(!lensManager.Equiped);
                documentReadability.SwitchDocumentImage();
            }
        }

        interactionPossible = CheckInteraction();
        //playerPullability.CheckForTakeOrRelease(inputManager, this, GameManager.gameManager.NormalCamera.gameObject.activeInHierarchy ? GameManager.gameManager.NormalCamera : GameManager.gameManager.LensCamera);
        playerPullability.UpdatePull(transform.position);

        playerMovements.CheckCrouch(inputManager.GetCrouch);

        if (lampManager.HasLamp)
        {
            EnigmaObject lampEnigmaObject = lampManager.CheckForLookedObject(GameManager.gameManager.NormalCamera.gameObject.activeInHierarchy ? GameManager.gameManager.NormalCamera : GameManager.gameManager.LensCamera);
            canLampOn = lampEnigmaObject != null;

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

        canLampOff = lampManager.LampIsOn;


        playerCursorManager.UpdateCursorSprite(interactionPossible, canLampOn, canLampOff);
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

            soundsManager.UpdateStepSound(inputManager.Running);
        }

        currentTotalForce += playerMovements.GrvtManager.GetCurrentGravityForce();

        return currentTotalForce;
    }
    #endregion

    #region Interactions
    public bool CheckInteraction()
    {
        bool interactiveObjectExists = false;

        Camera mainCamera = GameManager.gameManager.NormalCamera.gameObject.activeInHierarchy ? GameManager.gameManager.NormalCamera : GameManager.gameManager.LensCamera;

        if (documentReadability.opened && inputManager.GetInteractDown)
            documentReadability.CloseDocument();

        Ray ray = mainCamera.ScreenPointToRay(new Vector3(mainCamera.pixelWidth / 2, mainCamera.pixelHeight / 2, 0));
        RaycastHit hit = new RaycastHit();

        if (Physics.Raycast(ray, out hit, 3))
        {
            IInteracible interactibleObject = hit.collider.GetComponent<IInteracible>();
            if (interactibleObject != null)
            {
                interactiveObjectExists = true;
                if (inputManager.GetInteractDown)
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

        return interactiveObjectExists;
    }
    #endregion
}
