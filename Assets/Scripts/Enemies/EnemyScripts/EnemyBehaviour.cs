using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Permet de gérer le Behaviour de l'ennemi
/// </summary>
public class EnemyBehaviour : MonoBehaviour {

    #region Fields

    public enum EnemyState
    {
        Inactive, //Quand invisible
        Static, //Quand il est est visible ou dans la réalité
        ChasePlayer, //quand il poursuit le player
        Attack ///Quand il attaque le player
    }

    public EnemyState enemyState;

    [Header("Enemy scripts")]
    public EnemyMovement movement;
    public EnemyAttack attack;
    public EnemyVisibility visibility;

    [Header("Detection values")]
    public LayerMask playerLayer;
    public float detectionRadius = 15;
    public float attackRadius = 2;
    bool playerDetected;

    //Required refereence
    private LensManager lensManager;

    #endregion

    void Start ()
    {
        lensManager = GameManager.gameManager.lensManager; //get lesn manager
	}

	void Update ()
    {
        Behaviour();
	}

    /// <summary>
    /// application des behaviour
    /// </summary>
    void Behaviour()
    {
        switch (enemyState)
        {
            case EnemyState.Inactive:

                if (lensManager.Equiped)
                {
                    ChangeBehaviour(EnemyState.Static);
                }

                break;
            case EnemyState.Static:
                playerDetected = PlayerDetected();

                //Si detection du player
                if (playerDetected)
                {
                    ChangeBehaviour(EnemyState.ChasePlayer);
                }

                //check lens
                if (!lensManager.Equiped)
                {
                    ChangeBehaviour(EnemyState.Inactive);
                }

                break;
            case EnemyState.ChasePlayer:
                playerDetected = PlayerDetected();
                movement.MoveToPlayer(); //move to player

                //Check lens
                if (!lensManager.Equiped)
                {
                    ChangeBehaviour(EnemyState.Inactive);
                }

                //Check attack
                bool attackPlayer = CanAttack();

                if (attackPlayer)
                {
                    ChangeBehaviour(EnemyState.Attack);
                }

                break;
            case EnemyState.Attack:

                if (!attack.isAttacking)
                {
                    ChangeBehaviour(EnemyState.Static);
                }

                //Check lens
                if (!lensManager.Equiped)
                {
                    attack.StopAllCoroutines();
                    attack.isAttacking = false;
                    ChangeBehaviour(EnemyState.Inactive);
                }

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
                visibility.SetInvisible();
                movement.canMoveToPlayer = false;
                movement.StopMovement();
                break;
            case EnemyState.Static:
                visibility.SetVisible();
                movement.canMoveToPlayer = false;
                movement.StopMovement();
                break;
            case EnemyState.ChasePlayer:
                visibility.SetVisible();
                movement.canMoveToPlayer = true;
                break;
            case EnemyState.Attack:
                visibility.SetVisible();

                //stop movement
                movement.canMoveToPlayer = false; 
                movement.StopMovement();

                //start attack
                attack.StartCoroutine(attack.Attack()); 
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

    /// <summary>
    /// Permet de savoir si le player est dans la range d'attack
    /// </summary>
    /// <returns></returns>
    bool CanAttack()
    {
        bool canAttack = Physics.CheckSphere(transform.position, attackRadius, playerLayer);

        return canAttack;
    }
}
