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
    public EnemyAudio enemyAudio;

    [Header("Detection values")]
    public LayerMask playerLayer;
    public float detectionRadius = 15;
    public float attackRadius = 2;
    bool playerDetected;

    [Header("Behaviour Variables")]
    bool activeLamp;

    [Header("Colliders")]
    [SerializeField] Collider[] bodyColliders;
    [SerializeField] Collider attackCollider;

    //Required refereence
    private LensManager lensManager;

    public Vector3 iniPos;
    EnemyState iniState;

    PlayerHitbox playerHitbox;

    #endregion

    void Start ()
    {
        lensManager = GameManager.gameManager.lensManager; //get lesn manager
        iniPos = transform.position;
        iniState = enemyState;
        playerHitbox = PlayerHitbox.instance;
        playerHitbox.OnDeath += ResetPositon;
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

                if (lensManager.Equiped || activeLamp)
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
                if (!lensManager.Equiped && !activeLamp)
                {
                    ChangeBehaviour(EnemyState.Inactive);
                }

                break;
            case EnemyState.ChasePlayer:
                playerDetected = PlayerDetected();
                movement.MoveToPlayer(); //move to player

                //Check lens
                if (!lensManager.Equiped && !activeLamp)
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
                if (!lensManager.Equiped && !activeLamp)
                {
                    attack.StopAllCoroutines();
                    attack.isAttacking = false;
                    ChangeBehaviour(EnemyState.Inactive);
                }

                break;
        }

        if (GameManager.gameManager.LampActivated && !activeLamp)
            ChangeEnemyWorld(1);

        if (!GameManager.gameManager.LampActivated && activeLamp)
            ChangeEnemyWorld(0);
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
                visibility.SetInvisibleInNormalWorld();
                movement.canMoveToPlayer = false;
                movement.StopMovement();

                foreach (Collider bodyCollider in bodyColliders)
                    bodyCollider.enabled = false;
                attackCollider.enabled = false;

                break;
            case EnemyState.Static:
                //visibility.SetVisibleInNormalWorld();
                movement.canMoveToPlayer = false;
                movement.StopMovement();

                foreach (Collider bodyCollider in bodyColliders)
                    bodyCollider.enabled = true;
                attackCollider.enabled = true;
                break;
            case EnemyState.ChasePlayer:
                //visibility.SetVisibleInNormalWorld();
                movement.canMoveToPlayer = true;
                enemyAudio.PlayEnemySound(enemyAudio.playerSpotedSounds);

                foreach (Collider bodyCollider in bodyColliders)
                    bodyCollider.enabled = true;
                attackCollider.enabled = true;
                break;
            case EnemyState.Attack:
                //visibility.SetVisibleInNormalWorld();
                enemyAudio.PlayEnemySound(enemyAudio.attackSounds);
                //stop movement
                movement.canMoveToPlayer = false; 
                movement.StopMovement();

                //start attack
                attack.StartCoroutine(attack.Attack());

                foreach (Collider bodyCollider in bodyColliders)
                    bodyCollider.enabled = true;
                attackCollider.enabled = true;
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

    /// <summary>
    /// 0 pour remettre l'ennemi dans son monde d'origine, 1 pour le mettre dans le monde normal
    /// </summary>
    public void ChangeEnemyWorld(int i)
    {
        if (i == 0)
        {
            visibility.SetInvisibleInNormalWorld();
            activeLamp = false;
        }
        if (i == 1)
        {
            visibility.SetVisibleInNormalWorld();
            activeLamp = true;
        }
    }

    public void ResetPositon()
    {
        transform.position = iniPos;
        enemyState = iniState;
    }
}
