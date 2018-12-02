using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitbox : MonoBehaviour {
    public Player relatedPlayer;

    public void Die()
    {
        GameManager.gameManager.checkpointsManager.Respawn(relatedPlayer);
    }
}
