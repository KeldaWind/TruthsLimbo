using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {

    [Header("Attack values")]
    [SerializeField] float anticipationTime = 0.5f;
    [SerializeField] float attackDuration = 0.5f;
    [SerializeField] float recoverDuration = 1f;

    [HideInInspector] public bool isAttacking;

    /// <summary>
    /// Permet de lancer l'attaque
    /// </summary>
    /// <returns></returns>
    public IEnumerator Attack()
    {
        isAttacking = true;
        //anticipation
        yield return new WaitForSeconds(anticipationTime);
        //attack
        Debug.Log("Attack from " + gameObject.name);
        yield return new WaitForSeconds(attackDuration);
        //recover
        yield return new WaitForSeconds(recoverDuration);

        isAttacking = false;
    }
}
