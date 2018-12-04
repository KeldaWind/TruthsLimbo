using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class EnemyMoveToTarget : EnemyMovement {

    [Header("Target Values")]
    public Transform targetPosition;
    bool isMoving;
    public float completedPrecision = 0.1f;
    public UnityEvent completedPathEvent;


	void Start ()
    {
		
	}
	

	void Update ()
    {
        CheckCompletedMovement();
	}

    void CheckCompletedMovement()
    {
        if (isMoving)
        {
            if (AlmostEqual(transform.position, targetPosition.position, completedPrecision))
            {
                completedPathEvent.Invoke();
                isMoving = false;
            }
        }
    }

    public void MoveToTarget()
    {
        meshAgent.SetDestination(targetPosition.position);
        isMoving = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawLine(transform.position, targetPosition.position);
    }

    public bool AlmostEqual(Vector3 v1, Vector3 v2, float precision)
    {
        bool equal = true;

        if (Mathf.Abs(v1.x - v2.x) > precision)
            equal = false;
        if (Mathf.Abs(v1.y - v2.y) > precision)
            equal = false;
        if (Mathf.Abs(v1.z - v2.z) > precision)
            equal = false;

        return equal;
    }
}
