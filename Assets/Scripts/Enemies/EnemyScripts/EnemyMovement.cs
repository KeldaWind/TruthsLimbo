using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour {

    [Header("NavMesh")]
    public NavMeshAgent meshAgent;

    [Header("Movement")]
    public bool canMoveToPlayer;
    

    [Header("Debug")]
    public bool debugMode;
    public KeyCode moveInput;
    public Transform destinationDebug;

    //Required references
    private Player player;

    void Start ()
    {
        player = GameManager.gameManager.player; //Get player
	}

	void Update ()
    {
        MoveToPlayer();
        Debug();
	}

    void Debug()
    {
        if (!debugMode) return;

        if (Input.GetKeyDown(moveInput))
        {
            MovePosition(destinationDebug.position);
        }
    }

    /// <summary>
    /// Move the enemy to player's position
    /// </summary>
    public void MoveToPlayer()
    {
        if (!canMoveToPlayer) return;

        MovePosition(player.transform.position);
    }

    /// <summary>
    /// Permet de stopper le mouvement
    /// </summary>
    public void StopMovement()
    {
        meshAgent.ResetPath();
    }

    /// <summary>
    /// Move the enemy to a target destination
    /// </summary>
    /// <param name="_destination"></param>
    public void MovePosition(Vector3 _destination)
    {
        meshAgent.SetDestination(_destination);
    }
}
