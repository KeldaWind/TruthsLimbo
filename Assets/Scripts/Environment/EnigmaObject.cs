﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnigmaObject : MonoBehaviour {
    /// <summary>
    /// IsReal means that the block has a physical presence, but is invisible without the lens.
    /// If not real, it is visible without the lens, but does not have a physical presence.
    /// </summary>
    [SerializeField] bool isReal;
    public bool IsReal
    {
        get
        {
            return isReal;
        }
    }
    [SerializeField] Collider objectCollider;
    [SerializeField] Renderer objectRenderer;
    [SerializeField] Rigidbody objectBody;
    [SerializeField] NavMeshObstacle objectNavMeshObstacle;
    [SerializeField] bool inverter;
    

    private void Start()
    {
        CheckWorld();
    }

    public void MoveToTheOtherWorld()
    {
        isReal = !isReal;
        CheckWorld();
    }

    public void CheckWorld()
    {
        if (objectCollider == null)
            objectCollider = GetComponent<Collider>();

        if (objectCollider != null)
            objectCollider.isTrigger = !isReal;


        if (objectRenderer == null)
            objectRenderer = GetComponent<Renderer>();

        if (objectBody == null)
            objectBody = GetComponent<Rigidbody>();

        if (objectBody != null)
        {
            objectBody.useGravity = isReal;
            objectBody.velocity = Vector3.zero;
            objectBody.angularVelocity = Vector3.zero;
        }

        if (objectNavMeshObstacle == null)
            objectNavMeshObstacle = GetComponent<NavMeshObstacle>();

        if (objectNavMeshObstacle != null && !inverter)
            objectNavMeshObstacle.enabled = isReal;

        if (objectNavMeshObstacle != null && inverter)
            objectNavMeshObstacle.enabled = !isReal;

        gameObject.layer = isReal ? 9 : 10;
    }
}
