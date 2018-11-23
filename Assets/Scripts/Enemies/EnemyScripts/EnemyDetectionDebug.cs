using UnityEngine;

/// <summary>
/// Permet de voir les ranges de detection du player dans la la fenetre Scene
/// </summary>
public class EnemyDetectionDebug : MonoBehaviour {

    public EnemyBehaviour enemy;
    public Color attackColor = Color.red;
    public Color detectionColor = Color.magenta;

    private void OnDrawGizmosSelected()
    {
        if (enemy == null)
        {
            Debug.LogWarning("EnemyBehaviour not assign on " + gameObject.name);
            return;
        }

        //Detection sphere
        Gizmos.color = detectionColor;
        Gizmos.DrawWireSphere(transform.position, enemy.detectionRadius);

        //attack sphere
        Gizmos.color = attackColor;
        Gizmos.DrawWireSphere(transform.position, enemy.attackRadius);
    }
}
