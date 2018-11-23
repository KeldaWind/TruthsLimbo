using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour {

    public enum EnemyState
    {
        Inactive, //Quand invisible
        Static, //Quand il est est visible ou dans la réalité
        ChasePlayer //quand il poursuit le player
    }

    public EnemyState enemyState;

    [Header("Enemy scripts")]
    public EnemyMovement movement;
    public EnemyAttack attack;

    [Header("Detection values")]
    public LayerMask playerLayer;
    public float detectionRadius = 15;
    public float attackRadius = 2;
    bool playerDetected;



	void Start ()
    {
		
	}
	

	void Update ()
    {

	}

    /// <summary>
    /// application des behaviour
    /// </summary>
    void Behaviour()
    {
        switch (enemyState)
        {
            case EnemyState.Inactive:
                //DO NOTHING
                break;
            case EnemyState.Static:
                playerDetected = PlayerDetected();

                break;
            case EnemyState.ChasePlayer:
                playerDetected = PlayerDetected();
                movement.MoveToPlayer(); //move to player

                break;
        }
    }

    /// <summary>
    /// Permet de changer de state de behaviour
    /// </summary>
    /// <param name="_nextState"></param>
    public void ChangeBehaviour(EnemyState _nextState)
    {
        enemyState = _nextState;

        switch (enemyState)
        {
            case EnemyState.Inactive:
                movement.canMoveToPlayer = false;
                movement.StopMovement();
                break;
            case EnemyState.Static:
                movement.canMoveToPlayer = false;
                movement.StopMovement();
                break;
            case EnemyState.ChasePlayer:
                movement.canMoveToPlayer = true;
                break;
        }
    }

    /// <summary>
    /// Permet de savoir si le player est dans la range de detection
    /// </summary>
    /// <returns></returns>
    bool PlayerDetected()
    {
        bool detected = Physics.CheckSphere(transform.position, detectionRadius, playerLayer);
        
        return detected;
    }
}
