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
    [SerializeField] Transform[] gravityCheckers;
    float currentVerticalSpeed;


    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public Vector3 GetCurrentGravityForce()
    {
        Vector3 gravityForce = new Vector3();

        CheckForOnGround();

        currentVerticalSpeed -= gravityAcceleration * Time.deltaTime;

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

            RaycastHit[] hits = Physics.RaycastAll(ray, 0.15f);

            foreach (RaycastHit hit in hits)
            {
                onGround = true;
                if(currentVerticalSpeed < 0)
                    currentVerticalSpeed = 0;
                break;
            }

            if (onGround)
                break;
        }

        return onGround;
    }

    public void Jump(float jumpForce)
    {
        currentVerticalSpeed = jumpForce;
        Debug.Log("Jump");
    }
}
