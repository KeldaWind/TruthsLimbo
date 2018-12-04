using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerMovements {
# region Walk and Run
    [Header("Walk and Run")]
    [SerializeField] float walkSpeed;
    [SerializeField] float runSpeed;
    [SerializeField] float crouchSpeed;

    public Vector3 GetCurrentPlayerWalkMove(Vector3 inputDirection, bool running)
    {
        Vector3 currentPlayerWalkMove = new Vector3();

        currentPlayerWalkMove = Quaternion.Euler(new Vector3(0, currentCameraDirection.y, 0)) * inputDirection.normalized * (Crouched ? crouchSpeed : running ? runSpeed : walkSpeed) * 100 * Time.deltaTime;

        return currentPlayerWalkMove;
    }

    public Vector3 GetCurrentPlayerMoveWithRightDirection(Vector3 rightDirection, bool running)
    {
        return rightDirection * (Crouched ? crouchSpeed : running ? runSpeed : walkSpeed) * 100 * Time.deltaTime;
    }
    #endregion

    #region Look
    [Space] [Header("Look")]
    [SerializeField] float cameraSensibility;
    [SerializeField] Vector3 currentCameraDirection;
    [SerializeField] Transform player;
    [SerializeField] Transform cameras;

    public void MovePlayerCameraAndObject()
    {
        currentCameraDirection.x -= Input.GetAxisRaw("Mouse Y") * cameraSensibility;
        currentCameraDirection.x = Mathf.Clamp(currentCameraDirection.x, -90, 90);

        currentCameraDirection.y += Input.GetAxisRaw("Mouse X") * cameraSensibility;
        if (currentCameraDirection.y > 360)
            currentCameraDirection.y -= 360;
        if (currentCameraDirection.y < 0)
            currentCameraDirection.y += 360;

        player.localRotation = Quaternion.Euler(new Vector3(0, currentCameraDirection.y, 0));
        cameras.localRotation = Quaternion.Euler(new Vector3(currentCameraDirection.x, 0, 0));
    }
    #endregion

    [Space] [Header("Jump")]
    [SerializeField] GravityManager gravityManager;
    public GravityManager GrvtManager
    {
        get
        {
            return gravityManager;
        }
    }
    [SerializeField] float jumpForce;

    public void Jump()
    {
        gravityManager.Jump(jumpForce);
    }

    [Space] [Header("Crouch")]
    [SerializeField] Transform playerColliderPivot;
    [SerializeField] float crouchedPlayerHeightCoeff;
    [SerializeField] float crouchTransitionTime;
    [SerializeField] float uncrouchedCamPos;
    [SerializeField] float crouchedCamPos;
    [SerializeField] Transform forceCrouchCheckers;
    [SerializeField] Vector3 uncrouchedCrouchCheckersPos;
    [SerializeField] Vector3 crouchedCrouchCheckersPos;
    float currentCrouchTransitionTime;

    public void CheckCrouch(bool crouched)
    {
        if (crouched)
        {
            if (currentCrouchTransitionTime < crouchTransitionTime)
                currentCrouchTransitionTime += Time.deltaTime;
            else if (currentCrouchTransitionTime > crouchTransitionTime)
                currentCrouchTransitionTime = crouchTransitionTime; 
        }
        else
        {
            if (CanGetUp())
            {
                if (currentCrouchTransitionTime > 0)
                    currentCrouchTransitionTime -= Time.deltaTime;
                else if (currentCrouchTransitionTime < 0)
                    currentCrouchTransitionTime = 0;
            }
        }

        playerColliderPivot.transform.localScale = new Vector3(1, Mathf.Lerp(crouchedPlayerHeightCoeff, 1, 1 - CurrentCrouchProgression), 1);
        cameras.transform.localPosition = new Vector3(cameras.transform.localPosition.x, Mathf.Lerp(crouchedCamPos, uncrouchedCamPos, 1 - CurrentCrouchProgression), cameras.transform.localPosition.z);
        forceCrouchCheckers.localPosition = Vector3.Lerp(crouchedCrouchCheckersPos, uncrouchedCrouchCheckersPos, 1 - CurrentCrouchProgression);
    }

    public bool CanGetUp()
    {
        bool canGetUp = true;

        for (int i = 0; i < forceCrouchCheckers.childCount; i++)
        {
            Transform pos = forceCrouchCheckers.GetChild(i);

            Ray ray = new Ray();
            ray.origin = pos.position;
            ray.direction = new Vector3(0, 1, 0);

            Debug.DrawRay(ray.origin, ray.direction * 0.15f, Color.red);

            RaycastHit[] hits = Physics.RaycastAll(ray, 0.15f);

            foreach (RaycastHit hit in hits)
            {
                if (!hit.collider.isTrigger)
                {
                    canGetUp = false;

                    break;
                }
            }

            if (!canGetUp)
                break;
        }

        return canGetUp;
    }

    public float CurrentCrouchProgression
    {
        get
        {
            return currentCrouchTransitionTime / crouchTransitionTime;
        }
    }

    public bool Crouched
    {
        get
        {
            return CurrentCrouchProgression > 0.2f;
        }
    }
}
