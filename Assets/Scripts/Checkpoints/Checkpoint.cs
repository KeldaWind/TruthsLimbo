using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] Transform respawnTransform;
    [SerializeField] Vector3 respawnPosition;


    private void Start()
    {
        if(respawnTransform != null)
        {
            respawnPosition = respawnTransform.position;
        }
    }

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
