using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GravityManager
{
    /// <summary>
    /// The direction of the gravity force.
    /// </summary>
    [SerializeField] Vector3 gravityDirection;
    [SerializeField] float gravityAcceleration;
    [SerializeField] float maximumGravitySpeed;
    [SerializeField] Transform[] gravityCheckers;
    float currentVerticalSpeed;


    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public Vector3 GetCurrentGravityForce()
    {
        Vector3 gravityForce = new Vector3();

        if (CheckForOnGround() && currentVerticalSpeed <= 0)
            currentVerticalSpeed = 0;
        else
        {
            currentVerticalSpeed -= gravityAcceleration * Time.deltaTime;

            currentVerticalSpeed = Mathf.Clamp(currentVerticalSpeed, -maximumGravitySpeed, maximumGravitySpeed);
        }
        gravityForce = gravityDirection * currentVerticalSpeed * 100 * Time.deltaTime;

        return gravityForce;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="characterCenter"></param>
    /// <param name="gravityCheckPositions"></param>
    /// <returns></returns>
    public bool CheckForOnGround()
    {
        bool onGround = false;

        foreach (Transform checker in gravityCheckers)
        {
            Ray ray = new Ray();
            ray.origin = checker.position;
            ray.direction = new Vector3(0, -1, 0);

            Debug.DrawRay(ray.origin, ray.direction * 0.3f, Color.red);

            RaycastHit[] hits = Physics.RaycastAll(ray, 0.3f);

            foreach (RaycastHit hit in hits)
            {
                if (!hit.collider.isTrigger)
                {
                    onGround = true;
                    if (currentVerticalSpeed < 0)
                        currentVerticalSpeed = 0;
                    break;
                }
            }

            if (onGround)
                break;
        }

        return onGround;
    }

    public Vector3 GetGroundNormalDirection()
    {
        Vector3 normalPosition = new Vector3();
        Vector3 normalDirection = new Vector3();
        int counter = 0;

        foreach (Transform checker in gravityCheckers)
        {
            Ray ray = new Ray();
            ray.origin = checker.position;
            ray.direction = new Vector3(0, -1, 0);

            RaycastHit[] hits = Physics.RaycastAll(ray, 0.3f);

            foreach (RaycastHit hit in hits)
            {
                if (!hit.collider.isTrigger)
                {
                    normalPosition += hit.point;
                    normalDirection += hit.normal;
                    counter++;
                    break;
                }
            }
        }
        if (counter == 0)
            return new Vector3(0, 1, 0);

        normalPosition = normalPosition / (float)counter;
        normalDirection = normalDirection / (float)counter;
        normalDirection.Normalize();

        Debug.DrawRay(normalPosition, normalDirection * 0.3f, Color.red);

        return normalDirection;
    }

    public void Jump(float jumpForce)
    {
        currentVerticalSpeed = jumpForce;
    }
}
