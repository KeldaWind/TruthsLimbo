using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {
    [SerializeField] Vector3 respawnPosition;
    public Vector3 GetRespawnPosition()
    {
        return respawnPosition;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerHitbox>() != null)
            GameManager.gameManager.checkpointsManager.ChangeCheckpoint(this);
    }

}
